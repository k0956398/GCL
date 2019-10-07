namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Factory to handle EEDeviceManager instance.
    /// </summary>
    public static class EEDeviceManagerFactory
    {
        private static readonly object _lockInstance = new object();

        // Single instance of EEDeviceManager
        private static EEDeviceManager _instDeviceManager = null;

        /// <summary>
        /// Creates a new or returns an existing EEDeviceManager instance. This ensures only one
        /// instance is existing.
        /// </summary>
        public static IEEDeviceManager DeviceManager
        {
            get
            {
                lock (_lockInstance)
                {
                    if (_instDeviceManager == null)
                        _instDeviceManager = new EEDeviceManager();
                    return _instDeviceManager;
                }
            }
        }
    }
}