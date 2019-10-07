namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// Display orientation (Normal, upside down, etc.).
    /// </summary>
    public enum DisplayOrientation
    {
        /// <summary>
        /// INVALID or unchanged orientation.
        /// </summary>
        INVALID = 0,

        /// <summary>
        /// Normal display orientation.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Upside down display orientation (rotated by 180°).
        /// </summary>
        UpsideDown = 2,

        /// <summary>
        /// Side display orientation (rotated by 270°).
        /// </summary>
        Side270 = 3,

        /// <summary>
        /// Side display orientation (rotated by 90°).
        /// </summary>
        Side90 = 4
    }
}