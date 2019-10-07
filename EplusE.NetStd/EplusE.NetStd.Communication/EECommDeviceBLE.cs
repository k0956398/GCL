using EplusE.NetStd.Communication.Protocol.Commands;
using Plugin.BluetoothLE;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Represents the communication layer for BLE interface. It has some basic properties to
    /// identify the connected E+E device and manages all read/write operations.
    /// </summary>
    internal class EECommDeviceBLE : EECommDevice, IDebugInfo
    {
        private IDevice _bleDevice;

        private byte _cmdTx = 0x0;

        private object _lockDebugInfo = new object();

        // use 0 for stress test, about 2 for production
        private int _maxAutoRetries = 0;

        private ConcurrentQueue<byte> _queueRx = new ConcurrentQueue<byte>();

        private List<long> _responseTimeMs = new List<long>(1000);

        // Some members needed to get debug infos
        private Stopwatch _stopWatch = new Stopwatch();

        private List<long> _writeTimeMs = new List<long>(1000);

        public EECommDeviceBLE(IDevice bleDevice, ushort hwCode, ScanConfigurationBLE scanConf) : base(scanConf)
        {
            _bleDevice = bleDevice;

            // Set model text based on hardware code (received through BLE advertisement manufacturer data)
            switch (hwCode)
            {
                case 0x1cd1:
                    ModelText = "Sigma10 (" + bleDevice.Name + ")";
                    break;

                default:
                    ModelText = bleDevice.Name;
                    break;
            }
        }

        public static Guid RxCharacteristicUuid { get; } = new Guid("0000c302-0000-1000-8000-00805f9b34fb");
        public static TimeSpan RxTimeout { get; set; } = TimeSpan.FromSeconds(1);
        public static Guid ServiceUuid { get; } = new Guid("0000a002-0000-1000-8000-00805f9b34fb");
        public static Guid TxCharacteristicUuid { get; } = new Guid("0000c301-0000-1000-8000-00805f9b34fb");
        public static TimeSpan TxTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public override int BytesToRead { get { return _queueRx.Count; } }

        public override int BytesToWrite { get { return 0; } }

        /// <summary>
        /// Gets the interface ID of device (UUID of BLE device or bus address of UART device)
        /// </summary>
        public override string InterfaceId { get { return _bleDevice?.Uuid.ToString(); } }

        /// <summary>
        /// Gets the communication interface type of device (currently BLE or UART)
        /// </summary>
        public override InterfaceType InterfaceType { get { return InterfaceType.BLE; } }

        /// <summary>
        /// E+E Uni Adapter based communication.
        /// </summary>
        public override bool IsUniAdapter { get { return false; } }

        /// <summary>
        /// Disconnects device.
        /// </summary>
        public override void Disconnect()
        {
            if (_bleDevice != null && _bleDevice.IsConnected())
                _bleDevice.CancelConnection();

            Connected = false;
        }

        /// <summary>
        /// Ensures connection status. Connects device if necessary.
        /// </summary>
        public override void EnsureConnection()
        {
            // Stop scan if device gets connected
            EEDeviceManagerFactory.DeviceManager.StopScan();

            if (_bleDevice != null && !_bleDevice.IsConnected())
            {
                bool success = Task.Run(async () =>
                {
                    _bleDevice.Connect(new ConnectionConfig { AutoConnect = true, AndroidConnectionPriority = ConnectionPriority.High });

                    // Change MTU size to at least 256 bytes otherwise protocol might not work
                    int mtu = await _bleDevice.RequestMtu(256);
                    if (mtu < 256)
                        Diagnostic.Msg(1, "EnsureConnection", "Unable to set MTU size to 256. Communication might not work correctly.");
                    else
                        Diagnostic.Msg(1, "EnsureConnection", "Set MTU size to " + mtu);

                    int loopCnt = 0;
                    while (loopCnt < 100 && !_bleDevice.IsConnected())
                    {
                        loopCnt++;
                        await Task.Delay(100);
                    }
                    if (!_bleDevice.IsConnected())
                    {
                        // Could not connect to device
                        return false;
                    }

                    await _bleDevice.DiscoverServices();

                    // Check rx Characteristic for notification or indication
                    IGattCharacteristic rxCharacteristic = await _bleDevice.GetCharacteristicsForService(ServiceUuid)
                        .FirstOrDefaultAsync(x => x.Uuid == RxCharacteristicUuid);

                    if (!rxCharacteristic.CanNotifyOrIndicate())
                        return false;

                    // Enable notification/indication on device
                    await rxCharacteristic.EnableNotifications(true);
                    rxCharacteristic.WhenNotificationReceived().Subscribe(result =>
                    {
                        _stopWatch.Stop();
                        lock (_lockDebugInfo)
                        {
                            if (_responseTimeMs.Count >= 1000)
                                _responseTimeMs.RemoveAt(0);
                            _responseTimeMs.Add(_stopWatch.ElapsedMilliseconds);
                        }
                        foreach (byte b in result.Data)
                            _queueRx.Enqueue(b);
                    });

                    return _bleDevice.IsConnected();
                }).Result;

                Connected = success;

                if (!success)
                {
                    // Could not connect to device
                    Diagnostic.Msg(1, "EnsureConnection", "Could not connect to device");
                    return;
                }
                else
                {
                    // Need to send DIAS to keep connection open
                    var ee31 = new Protocol.EE31Protocol(this);
                    IEECommandResult result = ee31.ExecuteCommand(EE31Command.Discovery,
                        new DiscoveryCmdParams(DiscoveryCmdParams.BuildNewDiscoveryId(), 0, 20, 500, 0xAF));

                    if (result.Code != EECmdResultCode.Success)
                        Diagnostic.Msg(1, "EnsureConnection", "Failed to send DIAS command (" + result.Code.ToString() + ")");
                }
            }

            _cmdTx = 0x0;
            while (!_queueRx.IsEmpty)
                _queueRx.TryDequeue(out byte dummy);
        }

        public override bool Equals(object obj)
        {
            var bLE = obj as EECommDeviceBLE;
            return bLE != null &&
                   InterfaceId == bLE.InterfaceId &&
                   InterfaceType == bLE.InterfaceType &&
                   IsUniAdapter == bLE.IsUniAdapter;
        }

        public override int GetHashCode()
        {
            var hashCode = -2091180254;
            hashCode = hashCode * -1521134295 + EqualityComparer<IDevice>.Default.GetHashCode(_bleDevice);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InterfaceId);
            hashCode = hashCode * -1521134295 + InterfaceType.GetHashCode();
            hashCode = hashCode * -1521134295 + IsUniAdapter.GetHashCode();
            return hashCode;
        }

        public void GetTimings(out long writeTimeMs, out long responseTimeMs)
        {
            writeTimeMs = 0;
            responseTimeMs = 0;
            lock (_lockDebugInfo)
            {
                writeTimeMs = _writeTimeMs.Count > 0 ? (long)_writeTimeMs.Average() : 0;
                responseTimeMs = _responseTimeMs.Count > 0 ? (long)_responseTimeMs.Average() : 0;
            }
        }

        /// <summary>
        /// Read byte through communication interface
        /// </summary>
        public override byte ReadByte()
        {
            // Get data from Rx queue if any available
            if (_queueRx.TryDequeue(out byte result))
                return result;
            else
                return 0;
        }

        /// <summary>
        /// Read bytes through communication interface
        /// </summary>
        public override int ReadBytes(byte[] buffer, int offset, int count)
        {
            int writeIdx = offset;
            int nrBytes = Math.Min(count, _queueRx.Count);

            while (nrBytes > 0)
            {
                if (_queueRx.TryDequeue(out byte result))
                {
                    buffer[writeIdx++] = result;
                    nrBytes--;
                }
            }

            return writeIdx - offset;
        }

        /// <summary>
        /// Write bytes through communication interface
        /// </summary>
        public override void WriteBytes(byte[] buffer, int offset, int count)
        {
            if (_bleDevice == null)
                throw new ArgumentNullException("_bleDevice");

            _cmdTx = buffer[2];

            int loopCnt = 0;
            while (loopCnt <= _maxAutoRetries)
            {
                loopCnt++;
                try
                {
                    IGattCharacteristic characteristic = _bleDevice.GetCharacteristicsForService(ServiceUuid)
                        .FirstOrDefaultAsync(x => x.Uuid == TxCharacteristicUuid).Wait();

                    if (null != characteristic)
                    {
                        Stopwatch watch = Stopwatch.StartNew();
                        characteristic.WriteWithoutResponse(buffer).Wait();
                        watch.Stop();
                        _stopWatch.Restart();

                        lock (_lockDebugInfo)
                        {
                            if (_writeTimeMs.Count >= 1000)
                                _writeTimeMs.RemoveAt(0);
                            _writeTimeMs.Add(watch.ElapsedMilliseconds);
                        }

                        break;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(75);
                }
            }
        }
    }
}