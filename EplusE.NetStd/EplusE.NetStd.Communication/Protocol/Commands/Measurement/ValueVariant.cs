namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Variant index of measurement value
    /// </summary>
    public enum ValueVariant : byte
    {
        /// <summary>
        /// Invalid.
        /// </summary>
        INVALID = 0,

        /// <summary>
        /// Measurement value.
        /// </summary>
        MeasVal = 1,

        /// <summary>
        /// Minimal value.
        /// </summary>
        MinValue = 2,

        /// <summary>
        /// Maximum value.
        /// </summary>
        MaxValue = 3,

        /// <summary>
        /// Average value.
        /// </summary>
        AvgValue = 4,

        /// <summary>
        /// Measurement value raw (no averaging, etc.).
        /// </summary>
        MeasValRaw = 5
    }
}