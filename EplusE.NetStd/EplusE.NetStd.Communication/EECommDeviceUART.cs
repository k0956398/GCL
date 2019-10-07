using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Represents the communication layer for UART interface. It has some basic properties to
    /// identify the connected E+E device and manages all read/write operations.
    /// </summary>
    internal class EECommDeviceUART : EECommDevice
    {
        private readonly int _busAddr;

        private readonly bool _isUniAdapter;

        private readonly object _lock_serPort = new object();

        private System.IO.Ports.SerialPort _serPort = null;

        public EECommDeviceUART(string model, int busAddr, ScanConfigurationUART scanConf, bool uniAdapter) : base(scanConf)
        {
            _busAddr = busAddr;
            _isUniAdapter = uniAdapter;

            ModelText = model;
        }

        public override int BytesToRead
        {
            get
            {
                lock (_lock_serPort)
                {
                    if (null != _serPort)
                        return _serPort.BytesToRead;
                }

                return 0;
            }
        }

        public override int BytesToWrite
        {
            get
            {
                lock (_lock_serPort)
                {
                    if (null != _serPort)
                        return _serPort.BytesToWrite;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the interface ID of device (UUID of BLE device or bus address of UART device)
        /// </summary>
        public override string InterfaceId { get { return _busAddr.ToString(); } }

        /// <summary>
        /// Gets the communication interface type of device (currently BLE or UART)
        /// </summary>
        public override InterfaceType InterfaceType { get { return InterfaceType.UART; } }

        /// <summary>
        /// E+E Uni Adapter based communication.
        /// </summary>
        public override bool IsUniAdapter { get { return _isUniAdapter; } }

        /// <summary>
        /// Disconnects device.
        /// </summary>
        public override void Disconnect()
        {
            lock (_lock_serPort)
            {
                if (_serPort != null)
                {
                    if (_serPort.IsOpen)
                        _serPort.Close();
                    _serPort.Dispose();
                    _serPort = null;
                }

                Connected = false;
            }
        }

        /// <summary>
        /// Ensures connection status. Connects device if necessary.
        /// </summary>
        public override void EnsureConnection()
        {
            lock (_lock_serPort)
            {
                ScanConfigurationUART uartConf = (ScanConfigurationUART)ScanConfigurationActive;
                uartConf.UnauthorizedAccess = false;

                try
                {
                    if (_serPort == null)
                    {
                        _serPort = new System.IO.Ports.SerialPort()
                        {
                            PortName = uartConf.Port,
                            BaudRate = uartConf.ComPortSettings.Baudrate,
                            Parity = uartConf.ComPortSettings.Parity,
                            DataBits = uartConf.ComPortSettings.Databits,
                            StopBits = uartConf.ComPortSettings.Stopbits,
                            Handshake = uartConf.ComPortSettings.Handshake,
                            DtrEnable = true,
                            RtsEnable = true    // Request-to-send (RS232 adapter power supply)
                        };

                        System.Threading.Thread.Sleep(200);
                    }

                    if (!_serPort.IsOpen)
                    {
                        _serPort.Open();
                        Connected = true;
                    }
                }
                catch (Exception exc)
                {
                    // Check for UnauthorizedAccessException (port is in use by
                    // another program)
                    if (exc is UnauthorizedAccessException)
                    {
                        // Do not pass along this exception.
                        uartConf.UnauthorizedAccess = true;
                    }
                    else
                        throw exc;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var uART = obj as EECommDeviceUART;
            return uART != null &&
                   _busAddr == uART._busAddr &&
                   InterfaceId == uART.InterfaceId &&
                   InterfaceType == uART.InterfaceType &&
                   IsUniAdapter == uART.IsUniAdapter;
        }

        public override int GetHashCode()
        {
            var hashCode = -1614350696;
            hashCode = hashCode * -1521134295 + _busAddr.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InterfaceId);
            hashCode = hashCode * -1521134295 + InterfaceType.GetHashCode();
            hashCode = hashCode * -1521134295 + IsUniAdapter.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Read byte through communication interface
        /// </summary>
        public override byte ReadByte()
        {
            lock (_lock_serPort)
            {
                if (null != _serPort)
                    return (byte)_serPort.ReadByte();
            }

            throw new ArgumentNullException("_serPort");
        }

        /// <summary>
        /// Read bytes through communication interface
        /// </summary>
        public override int ReadBytes(byte[] buffer, int offset, int count)
        {
            lock (_lock_serPort)
            {
                if (null != _serPort)
                    return _serPort.Read(buffer, offset, count);
            }

            throw new ArgumentNullException("_serPort");
        }

        /// <summary>
        /// Write bytes through communication interface
        /// </summary>
        public override void WriteBytes(byte[] buffer, int offset, int count)
        {
            lock (_lock_serPort)
            {
                if (null != _serPort)
                {
                    _serPort.Write(buffer, offset, count);
                }
                else
                {
                    throw new ArgumentNullException("_serPort");
                }
            }
        }
    }
}