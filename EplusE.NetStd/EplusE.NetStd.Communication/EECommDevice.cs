using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Represents the communication layer. Created and managed by EEDeviceManager.
    /// </summary>
    internal abstract class EECommDevice : IEECommDevice
    {
        private IScanConfiguration _scanConfigurationActive;

        // Prevent direct creation of the class. It should be instantiated through one of its derived
        // classes (EECommDeviceBLE, EECommDeviceUART).
        protected EECommDevice(IScanConfiguration scanConf)
        {
            ScanConfigurationsUsed = new List<IScanConfiguration> { scanConf };
            _scanConfigurationActive = scanConf;
        }

        /// <summary>
        /// Number of bytes available to read on communication interface.
        /// </summary>
        public abstract int BytesToRead { get; }

        /// <summary>
        /// Number of bytes to be written on communication interface.
        /// </summary>
        public abstract int BytesToWrite { get; }

        /// <summary>
        /// Gets the connected status of the device.
        /// </summary>
        public bool Connected { get; internal protected set; }

        /// <summary>
        /// Gets the interface ID of device (UUID of BLE device or bus address of UART device)
        /// </summary>
        public abstract string InterfaceId { get; }

        /// <summary>
        /// Gets the communication interface type of device (currently BLE or UART)
        /// </summary>
        public abstract InterfaceType InterfaceType { get; }

        /// <summary>
        /// E+E Uni Adapter based communication.
        /// </summary>
        public abstract bool IsUniAdapter { get; }

        /// <summary>
        /// Gets model text of device behind this communication interface (e.g.: Sigma10, EE260,
        /// EE872, ...)
        /// </summary>
        public string ModelText { get; internal protected set; }

        /// <summary>
        /// Gets or sets current scan configuration of this communication interface.
        /// </summary>
        public IScanConfiguration ScanConfigurationActive
        {
            get { return _scanConfigurationActive; }
            set
            {
                if (value != _scanConfigurationActive)
                {
                    Disconnect();

                    // Set new configuration and ensure connection with this new settings applied
                    _scanConfigurationActive = value;

                    EnsureConnection();

                    // Add to used configurations list
                    if (!ScanConfigurationsUsed.Contains(_scanConfigurationActive))
                    {
                        List<IScanConfiguration> usedConfigs = new List<IScanConfiguration>(ScanConfigurationsUsed)
                        {
                            _scanConfigurationActive
                        };
                        ScanConfigurationsUsed = usedConfigs;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a list of used scan configurations on this communication interface.
        /// </summary>
        public IEnumerable<IScanConfiguration> ScanConfigurationsUsed { get; private set; }

        /// <summary>
        /// Disconnects device.
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Ensures connection status. Connects device if necessary.
        /// </summary>
        public abstract void EnsureConnection();

        /// <summary>
        /// Read byte through communication interface
        /// </summary>
        public abstract byte ReadByte();

        /// <summary>
        /// Read bytes through communication interface
        /// </summary>
        public abstract int ReadBytes(byte[] buffer, int offset, int count);

        /// <summary>
        /// Write bytes through communication interface
        /// </summary>
        public abstract void WriteBytes(byte[] buffer, int offset, int count);
    }
}