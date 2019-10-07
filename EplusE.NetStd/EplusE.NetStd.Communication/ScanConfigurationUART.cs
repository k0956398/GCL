using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Scan configuration for UART interface.
    /// </summary>
    public class ScanConfigurationUART : IScanConfiguration
    {
        /// <summary>
        /// Scan configuration to scan all available serial ports (ScanPort == null)
        /// </summary>
        public ScanConfigurationUART()
        {
            Port = null;

            // Default: 9600 Baud, 8N1, no handshake
            ComPortSettings = new ComPortSettings();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        public ScanConfigurationUART(ScanConfigurationUART other)
        {
            if (other != null)
            {
                Port = other.Port;
                ComPortSettings = new ComPortSettings(other.ComPortSettings);
            }
        }

        /// <summary>
        /// Scan configuration to scan given serial port.
        /// </summary>
        /// <param name="port">The serial port</param>
        /// <param name="settings">Serial port settings</param>
        public ScanConfigurationUART(string port, ComPortSettings settings)
        {
            Port = port;
            ComPortSettings = settings;
        }

        public ComPortSettings ComPortSettings { get; private set; }
        public string Port { get; private set; }
        public InterfaceType Type { get { return InterfaceType.UART; } }
        public bool UnauthorizedAccess { get; internal set; }

        public override bool Equals(object obj)
        {
            var uART = obj as ScanConfigurationUART;
            return uART != null &&
                   Type == uART.Type &&
                   Port == uART.Port &&
                   EqualityComparer<ComPortSettings>.Default.Equals(ComPortSettings, uART.ComPortSettings);
        }

        public override int GetHashCode()
        {
            var hashCode = 1776735863;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Port);
            hashCode = hashCode * -1521134295 + EqualityComparer<ComPortSettings>.Default.GetHashCode(ComPortSettings);
            return hashCode;
        }

        public IScanConfiguration MakeCopy()
        {
            return new ScanConfigurationUART(this);
        }
    }
}