using EplusE.NetStd.Communication.Protocol;
using EplusE.NetStd.Communication.Protocol.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Manages background scanning of E+E hardware devices (currently BLE + UART). There only should
    /// be one instance of EEDeviceManager which can be obtained by EEDeviceManagerFactory.
    /// EEDeviceManager is used to get communication interface required to create actual device instances.
    /// </summary>
    internal class EEDeviceManager : IEEDeviceManager
    {
        private object __lock_SendCmdMultiResponse = new object();

        private IDisposable _bleScanner = null;

        private AutoResetEvent _evtAbortUARTScan = new AutoResetEvent(false);

        private IList<IEECommDevice> _foundDevices = new List<IEECommDevice>();

        private Task _uartScanner = null;

        private DateTime timestampLastTx = DateTime.UtcNow;

        public EEDeviceManager()
        {
        }

        /// <summary>
        /// Notification for each new found device.
        /// </summary>
        public event EventHandler<IEECommDevice> DeviceFound = null;

        /// <summary>
        /// Notification for exceptions during scan.
        /// </summary>
        public event EventHandler<Exception> ScanException = null;

        /// <summary>
        /// Gets list of all IEECommDevice devices currently found by background scan.
        /// </summary>
        public IEnumerable<IEECommDevice> FoundDevices { get { return _foundDevices; } }

        /// <summary>
        /// Starts scanning for devices in background. Attach to DeviceFound event to be notified
        /// about new devices. Scan exceptions might be received through ScanException event.
        /// ATTENTION: Device connections are closed prior to scanning.
        /// </summary>
        public void StartScan(IScanConfiguration config)
        {
            if (config is ScanConfigurationEmulation scanConfiguration)
            {
                lock (_foundDevices)
                {
                    int nr = 0;
                    foreach (EmulationSettings settings in scanConfiguration.EmulationDevicesSettings)
                    {
                        // Add emulated device
                        IEECommDevice emulatedDevice = new EECommDeviceEmulation(nr++, settings);
                        if (_foundDevices.FirstOrDefault(x => x.InterfaceType == InterfaceType.Emulation && x.InterfaceId == emulatedDevice.InterfaceId) == null)
                        {
                            _foundDevices.Add(emulatedDevice);

                            // Fire found device event
                            FireDeviceFound(emulatedDevice);
                        }
                    }
                }
            }
            if (config is ScanConfigurationBLE scanConfigurationBLE)
                StartScanBLE(scanConfigurationBLE);
            if (config is ScanConfigurationUART scanConfigurationUART)
                StartScanUART(scanConfigurationUART);
        }

        /// <summary>
        /// Stops scanning for devices.
        /// </summary>
        public void StopScan()
        {
            if (null != _bleScanner)
            {
                _bleScanner.Dispose();
                _bleScanner = null;
            }

            if (null != _uartScanner)
            {
                // Set event to abort scanner task
                _evtAbortUARTScan.Set();

                // Wait until scanner task completed
                _uartScanner.Wait();
                _uartScanner.Dispose();
                _uartScanner = null;
            }
        }

        public IEECommDevice TryGetDevice(IScanConfiguration config, ushort busAddr)
        {
            if (config is ScanConfigurationUART scanConfigurationUART)
            {
                if (busAddr == 0)
                    throw new ArgumentException("Device bus address must not be 0", "busAddr");

                IEECommDevice device = null;

                // Search in already existing devices before starting a new scan
                lock (_foundDevices)
                {
                    device = _foundDevices.FirstOrDefault(x => x.InterfaceType == InterfaceType.UART && x.InterfaceId == busAddr.ToString());
                }

                if (device != null)
                {
                    // Try to revitalize device
                    IEECommProtocol protEE31 = EECommProtocolFactory.GetEE31Protocol(device);
                    IEECommandResult result = null;

                    // Force silence
                    Thread.Sleep(50);

                    // For non-EE31 devices (Modbus, etc.): Send protocol switch command to make them speak EE31 (again)
                    ushort discoveryId = DiscoveryCmdParams.BuildNewDiscoveryId();
                    try
                    {
                        result = protEE31.ExecuteCommand(EE31Command.Discovery, new DiscoveryCmdParams(discoveryId, busAddr, 0, 100));
                    }
                    catch { }

                    try
                    {
                        result = protEE31.ExecuteCommand(EE31Command.Discovery, new DiscoveryCmdParams(discoveryId, busAddr, 0, 50));
                    }
                    catch { }

                    if (result != null && result.Code == EECmdResultCode.Success)
                        return device;
                }

                return device;
            }
            else if (config is ScanConfigurationEmulation scanConfigurationEmulation)
            {
                IEECommDevice emulatedDevice = new EECommDeviceEmulation(0, scanConfigurationEmulation.EmulationDevicesSettings.ElementAt(0));
                var existing = _foundDevices.FirstOrDefault(x => x.InterfaceType == InterfaceType.Emulation && x.InterfaceId == emulatedDevice.InterfaceId);
                if (existing == null)
                    existing = emulatedDevice;

                return existing;
            }

            throw new NotImplementedException("Currently it is only possible to get UART or emulated devices.");
        }

        private void FireDeviceFound(IEECommDevice device)
        {
            if (DeviceFound != null)
            {
                if (DeviceFound.Target is ISynchronizeInvoke sync && sync.InvokeRequired)
                    sync.Invoke(DeviceFound, new object[] { this, device });
                else
                    DeviceFound(this, device);
            }
        }

        private void FireScanException(Exception exc)
        {
            if (ScanException != null)
            {
                if (ScanException.Target is ISynchronizeInvoke sync && sync.InvokeRequired)
                    sync.Invoke(ScanException, new object[] { this, exc });
                else
                    ScanException(this, exc);
            }
        }

        /// <summary>
        /// Starts scanning for BLE devices in background. Attach to DeviceFound event to be notified
        /// about new devices. Scan exceptions might be received through ScanException event.
        /// ATTENTION: Device connections are closed prior to scanning.
        /// </summary>
        private void StartScanBLE(ScanConfigurationBLE config)
        {
            try
            {
                // Check if already scanning
                if (Plugin.BluetoothLE.CrossBleAdapter.Current.IsScanning || null != _bleScanner)
                    return;

                // ATTENTION: according to documentation (https://github.com/aritchie/bluetoothle),
                // if there is an open device connection it needs to be closed prior to scanning.
                IEECommDevice foundBLE = null;
                lock (_foundDevices)
                {
                    while ((foundBLE = _foundDevices.FirstOrDefault(x => (x is EECommDeviceBLE) || (x is EECommDeviceEmulation))) != null)
                    {
                        // Disconnect BLE device
                        foundBLE.Disconnect();
                        // Remove BLE devices from list of found devices
                        _foundDevices.Remove(foundBLE);
                    }
                }

                Plugin.BluetoothLE.ScanConfig scanConfig = new Plugin.BluetoothLE.ScanConfig
                {
                    ScanType = Plugin.BluetoothLE.BleScanType.Balanced
                };

                _bleScanner = Plugin.BluetoothLE.CrossBleAdapter.Current.Scan(scanConfig).Subscribe(scanResult =>
                {
                    if (null != scanResult && null != scanResult.Device)
                    {
                        // Check if E+E device through manufacturer data (Hardwarecode 0x1CD1)
                        ushort hwCode = 0;
                        if (scanResult.AdvertisementData.ManufacturerData != null && scanResult.AdvertisementData.ManufacturerData.Count() >= 4)
                            hwCode = (ushort)(scanResult.AdvertisementData.ManufacturerData[2] << 8 | scanResult.AdvertisementData.ManufacturerData[3]);

                        if (hwCode == 0x1CD1)
                        {
                            Plugin.BluetoothLE.IDevice bleDevice = scanResult.Device;

                            if (config.Uuid == null || config.Uuid == bleDevice.Uuid)
                            {
                                lock (_foundDevices)
                                {
                                    IEECommDevice device = _foundDevices.FirstOrDefault(x => x.InterfaceType == InterfaceType.BLE && x.InterfaceId == bleDevice.Uuid.ToString());
                                    if (null == device)
                                    {
                                        // Create new BLE device
                                        device = new EECommDeviceBLE(bleDevice, hwCode, config);

                                        _foundDevices.Add(device);

                                        // Fire found device event
                                        FireDeviceFound(device);
                                    }
                                }
                            }
                        }
                    }
                }, ex => { FireScanException(ex); });
            }
            catch (Exception exc)
            {
                FireScanException(exc);
            }
        }

        /// <summary>
        /// Starts scanning for UART devices on all available COM ports in backgound. Attach to
        /// DeviceFound event to be notified about new devices. Scan exceptions might be received
        /// through ScanException event.
        /// </summary>
        private async void StartScanUART(ScanConfigurationUART config)
        {
            try
            {
                if (_uartScanner != null)
                    return;

                _uartScanner = Task.Run(() =>
                {
                    try
                    {
                        var scanConfigs = new List<ScanConfigurationUART>();

                        // Either scan all available ports or the one given through configuration parameter.
                        if (string.IsNullOrEmpty(config.Port))
                        {
                            // Use HidSharp to enumerate COM ports
                            // (System.IO.Ports.SerialPort.GetPortNames() is not supported on UWP platform)
                            var serialDevices = HidSharp.DeviceList.Local.GetSerialDevices();

                            foreach (HidSharp.SerialDevice dev in serialDevices)
                            {
                                // Scan with default: 9600 Baud, 8N1, no handshake
                                // and a second config: 9600 Baud, 8E1, no handshake
                                scanConfigs.Add(new ScanConfigurationUART(dev.GetFileSystemName(), new ComPortSettings()));
                                scanConfigs.Add(new ScanConfigurationUART(dev.GetFileSystemName(), new ComPortSettings() { Parity = System.IO.Ports.Parity.Even }));
                            }
                        }
                        else
                            scanConfigs.Add(new ScanConfigurationUART(config));

                        // Run until event is set. Block for a small amount of time after each cycle.
                        while (!_evtAbortUARTScan.WaitOne(1000))
                        {
                            IEECommDevice uartDiscoveryInterface = null;
                            IEECommProtocol protEE31 = null;
                            try
                            {
                                foreach (ScanConfigurationUART tryConfig in scanConfigs)
                                {
                                    ushort discoveryId = DiscoveryCmdParams.BuildNewDiscoveryId();

                                    // Check if COM port is E+E Uni-Adapter Virtual COM Port
                                    bool isUniAdapter = false;
                                    HidSharp.DeviceList list = HidSharp.DeviceList.Local;
                                    if (list.TryGetSerialDevice(out HidSharp.SerialDevice serDev, tryConfig.Port))
                                    {
                                        string name = serDev.GetFriendlyName();
                                        isUniAdapter = -1 != name.IndexOf("E+E Uni-Adapter", StringComparison.OrdinalIgnoreCase);
                                    }

                                    try
                                    {
                                        tryConfig.UnauthorizedAccess = false;

                                        // Scan until no new devices found (3 times)
                                        int noNewDeviceFoundCnt = 0;
                                        while (noNewDeviceFoundCnt < 3)
                                        {
                                            if (uartDiscoveryInterface == null)
                                            {
                                                // Create communication interface (once) for discovery. Device discovery will receive
                                                // additional information (like: Model-Text, Bus-Address, ...)
                                                uartDiscoveryInterface = new EECommDeviceUART("", 0, tryConfig, isUniAdapter);

                                                // Get EE31 protocol instance to start discovery
                                                protEE31 = EECommProtocolFactory.GetEE31Protocol(uartDiscoveryInterface);
                                            }
                                            else
                                            {
                                                // Just change current configuration
                                                uartDiscoveryInterface.ScanConfigurationActive = tryConfig;
                                            }

                                            // Force silence
                                            Thread.Sleep(50);

                                            bool newDeviceFound = false;

                                            // Get EE31 protocol instance to start discovery
                                            IEECommandResult result = protEE31.ExecuteCommand(EE31Command.Discovery, new DiscoveryCmdParams(discoveryId));
                                            if (result.Code == EECmdResultCode.Success)
                                            {
                                                foreach (DiscoveryCmdResult.FoundDevice foundDevice in (result as DiscoveryCmdResult).Devices)
                                                {
                                                    IEECommDevice uartDeviceInterface = null;
                                                    try
                                                    {
                                                        // Disconnect because same COM port may be used again for found device below
                                                        uartDiscoveryInterface.Disconnect();

                                                        // Update device with detailed information (Model-Text, Bus-Address, ...)
                                                        uartDeviceInterface = new EECommDeviceUART(foundDevice.ModelText, foundDevice.BusAddr, tryConfig, isUniAdapter);

                                                        // Get EE31 protocol instance for new UART interface
                                                        IEECommProtocol protEE31Dev = EECommProtocolFactory.GetEE31Protocol(uartDeviceInterface);

                                                        lock (_foundDevices)
                                                        {
                                                            if (null == _foundDevices.FirstOrDefault(x => x.Equals(uartDeviceInterface)))
                                                            {
                                                                _foundDevices.Add(uartDeviceInterface);
                                                                // Fire found device event
                                                                FireDeviceFound(uartDeviceInterface);

                                                                newDeviceFound = true;
                                                            }
                                                        }

                                                        // Force silence
                                                        Thread.Sleep(50);

                                                        // Send Discovery ACK frame to mute found transmitter
                                                        protEE31Dev.ExecuteCommand(EE31Command.DiscoveryAck, new DiscoveryAckCmdParams(foundDevice.BusAddr, discoveryId));
                                                    }
                                                    finally
                                                    {
                                                        if (uartDeviceInterface != null)
                                                            uartDeviceInterface.Disconnect();
                                                    }
                                                }
                                            }

                                            if (!newDeviceFound)
                                                noNewDeviceFoundCnt++;
                                            else
                                                noNewDeviceFoundCnt = 0;
                                        }
                                    }
                                    catch (Exception exc)
                                    {
                                        // Check for UnauthorizedAccessException (port is in use by
                                        // another program)
                                        if (exc is UnauthorizedAccessException)
                                        {
                                            // Do not pass along this exception. Instead skip port and
                                            // try next one.
                                            config.UnauthorizedAccess = true;
                                        }
                                        else
                                            throw exc;
                                    }
                                }
                            }
                            finally
                            {
                                if (uartDiscoveryInterface != null)
                                    uartDiscoveryInterface.Disconnect();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        ScanException?.Invoke(this, exc);
                    }
                });

                await _uartScanner;
            }
            catch (Exception exc)
            {
                ScanException?.Invoke(this, exc);
            }
        }
    }
}