namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDisplayDetailsCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Gets the background light brightness (0..100%).
        /// 0% = dark (off), 100% = max. brightness.
        /// Null if backlight brightness not supported.
        /// </summary>
        public int? BacklightBrightness { get; private set; }

        /// <summary>
        /// Gets the display contrast setting (0..100%).
        /// 0% = min., 100% = max. contrast.
        /// Null if display contrast setting not supported.
        /// </summary>
        public int? Contrast { get; private set; }

        /// <summary>
        /// Gets the display orientation (Normal, upside down, etc.).
        /// Null if display mode not supported.
        /// </summary>
        public DisplayOrientation? DisplayOrientation { get; private set; }

        /// <summary>
        /// Gets the switch interval time [msec] for alternating display mode.
        /// Null if switch interval time not supported.
        /// </summary>
        public int? SwitchInterval { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length > 1 && 0xFF != Data[1])
            {
                // Switch intervall [msec/100]
                SwitchInterval = Data[1] * 100;
            }
            if (Data.Length > 2 && 0xFF != Data[2])
            {
                // Backlight brightness 0..100%
                BacklightBrightness = Data[2];
            }
            if (Data.Length > 3 && 0xFF != Data[3])
            {
                // Contrast 0..100%
                Contrast = Data[3];
            }
            if (Data.Length > 4 && 0xFF != Data[4])
            {
                // Display orientation (Normal, upside down,...)
                switch (Data[4])
                {
                    default:
                    case 0:
                        // Normal
                        DisplayOrientation = Protocol.DisplayOrientation.Normal;
                        break;

                    case 1:
                        // Upside down (180°)
                        DisplayOrientation = Protocol.DisplayOrientation.UpsideDown;
                        break;

                    case 2:
                        // Side (270°)
                        DisplayOrientation = Protocol.DisplayOrientation.Side270;
                        break;

                    case 3:
                        // Side (90°)
                        DisplayOrientation = Protocol.DisplayOrientation.Side90;
                        break;
                }
            }
        }
    }
}