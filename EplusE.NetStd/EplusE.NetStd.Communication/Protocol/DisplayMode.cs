namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// Display modes (One line, two lines, etc.).
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// Display off.
        /// </summary>
        Off = 1,

        /// <summary>
        /// One line visible on display only.
        /// </summary>
        OneLine = 2,

        /// <summary>
        /// One line visible on display, measurands are shown alternating.
        /// </summary>
        OneLineAlternating = 3,

        /// <summary>
        /// Two lines visible on display.
        /// </summary>
        TwoLines = 4,

        /// <summary>
        /// Three lines visible on display.
        /// </summary>
        ThreeLines = 5

        ///// <summary>
        ///// Four lines visible on display.
        ///// </summary>
        //FourLines = 6
    }
}