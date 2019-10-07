using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Represents the communication layer. It has some basic properties to identify the connected
    /// E+E device and manages all read/write operations.
    /// </summary>
    public interface IEECommDevice
    {
        /// <summary>
        /// Number of bytes available to read on communication interface.
        /// </summary>
        int BytesToRead { get; }

        /// <summary>
        /// Number of bytes to be written on communication interface.
        /// </summary>
        int BytesToWrite { get; }

        /// <summary>
        /// Gets the connected status of the device.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Gets the interface ID of device (UUID of BLE device or bus address of UART device)
        /// </summary>
        string InterfaceId { get; }

        /// <summary>
        /// Gets the communication interface type of device.
        /// </summary>
        InterfaceType InterfaceType { get; }

        /// <summary>
        /// E+E Uni Adapter based communication (UART only; unused for BLE).
        /// </summary>
        bool IsUniAdapter { get; }

        /// <summary>
        /// Gets model text of device behind this communication interface (e.g.: Sigma10, EE260,
        /// EE872, ...)
        /// </summary>
        string ModelText { get; }

        /// <summary>
        /// Gets or sets current scan configuration of this communication interface.
        /// </summary>
        IScanConfiguration ScanConfigurationActive { get; set; }

        /// <summary>
        /// Gets a list of used scan configurations on this communication interface.
        /// </summary>
        IEnumerable<IScanConfiguration> ScanConfigurationsUsed { get; }

        /// <summary>
        /// Disconnects device.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Ensures connection status. Connects device if necessary.
        /// </summary>
        void EnsureConnection();

        /// <summary>
        /// Read byte through communication interface
        /// </summary>
        byte ReadByte();

        /// <summary>
        /// Read bytes through communication interface
        /// </summary>
        int ReadBytes(byte[] buffer, int offset, int count);

        /// <summary>
        /// Write bytes through communication interface
        /// </summary>
        void WriteBytes(byte[] buffer, int offset, int count);
    }
}