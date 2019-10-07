namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command Special Treatment class.
    /// </summary>
    internal class CmdSpecialTreatment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CmdSpecialTreatment"/> class.
        /// </summary>
        public CmdSpecialTreatment()
        {
            this.Unsupported = new SettingWithSource<bool>(false, SettingSource.Unknown);
            this.ReverseByteOrder = new SettingWithSource<bool>(false, SettingSource.Unknown);
            this.MaxParams = 3;
        }

        /// <summary>
        /// Gets or sets the maximum number of command parameters per call.
        /// </summary>
        /// <value>The maximum number of command parameters per call.</value>
        public int MaxParams { get; internal set; }

        /// <summary>
        /// Gets or sets the command parameters (i.e. float) have to be reversed flag.
        /// </summary>
        /// <value>The command parameters (i.e. float) have to be reversed flag.</value>
        public SettingWithSource<bool> ReverseByteOrder { get; internal set; }

        /// <summary>
        /// Gets or sets the unsupported command flag.
        /// </summary>
        /// <value>The unsupported command flag.</value>
        public SettingWithSource<bool> Unsupported { get; internal set; }
    }
}