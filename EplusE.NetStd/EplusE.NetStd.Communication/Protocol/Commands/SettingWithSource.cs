namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Setting source information (Device config file, reported by device, etc.)
    /// </summary>
    internal enum SettingSource
    {
        /// <summary>
        /// Unknown source
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// Defined in device configuration XML file
        /// </summary>
        DeviceConfigFile = 2,

        /// <summary>
        /// Reported by device
        /// </summary>
        ReportedByDevice = 3,

        /// <summary>
        /// Set by application at runtime
        /// </summary>
        AppDecision = 4
    }

    /// <summary>
    /// Encapsulates a data component with corresponding SettingSource information.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    internal class SettingWithSource<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingWithSource{TData}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        public SettingWithSource(TData value, SettingSource source)
        {
            this.Value = value;
            this.Source = source;
        }

        /// <summary>
        /// Gets SettingSource information of data component.
        /// </summary>
        public SettingSource Source { get; set; }

        /// <summary>
        /// Gets data component.
        /// </summary>
        public TData Value { get; set; }
    }
}