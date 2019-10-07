using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Manages background scanning of E+E hardware devices (currently BLE + UART). There only should
    /// be one instance of EEDeviceManager which can be obtained by EEDeviceManagerFactory.
    /// EEDeviceManager is used to get communication interface required to create actual device instances.
    /// </summary>
    public interface IEEDeviceManager
    {
        /// <summary>
        /// Notification for each new found device.
        /// </summary>
        event EventHandler<IEECommDevice> DeviceFound;

        /// <summary>
        /// Notification for exceptions during scan.
        /// </summary>
        event EventHandler<Exception> ScanException;

        /// <summary>
        /// List of current IEECommDevices found by background scan.
        /// </summary>
        IEnumerable<IEECommDevice> FoundDevices { get; }

        /// <summary>
        /// Starts scanning for devices in background. Attach to DeviceFound event to be notified
        /// about new devices. Scan exceptions might be received through ScanException event.
        /// ATTENTION: Device connections are closed prior to scanning.
        /// </summary>
        void StartScan(IScanConfiguration config);

        /// <summary>
        /// Stops scanning for devices.
        /// </summary>
        void StopScan();

        /// <summary>
        /// Try to establish connection on given configuration.
        /// Does not run in background.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        IEECommDevice TryGetDevice(IScanConfiguration config, ushort busAddr);
    }
}