namespace EplusE.Measurement
{
    /// <summary>
    /// Measurement value data types (float, double, etc).
    /// </summary>
    public enum MVDataType : byte
    {
        /// <summary>
        /// Invalid data type.
        /// </summary>
        INVALID = 0,

        /// <summary>
        /// Float (4 bytes).
        /// </summary>
        Float = 1,

        /// <summary>
        /// Double (8 bytes).
        /// </summary>
        Double = 10
    }
}