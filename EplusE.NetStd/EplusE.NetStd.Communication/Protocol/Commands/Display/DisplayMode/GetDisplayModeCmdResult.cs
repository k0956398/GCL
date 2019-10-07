using EplusE.Measurement;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDisplayModeCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Gets the assigned measurands to display.
        /// Null if assigned measurands not supported.
        /// </summary>
        public IEnumerable<MVCode> AssignedMeasurands { get; private set; }

        /// <summary>
        /// Gets the background light status (on or off).
        /// Null if backlight not supported.
        /// </summary>
        public bool? BacklightActive { get; private set; }

        /// <summary>
        /// Gets the display mode (One line, two lines, etc.).
        /// Null if display mode not supported.
        /// </summary>
        public DisplayMode? DisplayMode { get; private set; }

        /// <summary>
        /// Gets or sets the flag, if MVCode numbers have to be translated then reading from/writing to device.
        /// I.e. EE31 indices vs. Gen.Cfg. MVCode numbering.
        /// </summary>
        public bool MVCodesNumberTranslationNeeded { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 2)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            byte dispMode = Data[0];
            // Bit 7 = 0: measurands numbering is from EE31 indices list.
            // Bit 7 = 1: measurands numbering as in Gen.Cfg
            MVCodesNumberTranslationNeeded = (0x80 != (dispMode & 0x80));
            // Bit 6 = 0: automatic measuring range switch (on overflow) off
            // Bit 6 = 1: automatic measuring range switch (on overflow) on
            // settings.xxx = (0x40 == (dispMode & 0x40));
            // Bit 5 = 0: backlight off
            // Bit 5 = 1: backlight on
            BacklightActive = (0x20 == (dispMode & 0x20));
            // Bit 4 = 0: Startup logo off
            // Bit 4 = 1: Startup logo on
            // settings.xxx = (0x10 == (dispMode & 0x10));
            // Bit 3 = 0: Startup lamptest off
            // Bit 3 = 1: Startup lamptest on
            // settings.xxx = (0x08 == (dispMode & 0x08));
            // Bits 0..2: Display mode
            switch ((dispMode & 0x07))
            {
                case 0:
                    // Display off
                    DisplayMode = Protocol.DisplayMode.Off;
                    break;

                case 1:
                    // One line
                    DisplayMode = Protocol.DisplayMode.OneLine;
                    break;

                case 2:
                    // One line alternating
                    DisplayMode = Protocol.DisplayMode.OneLineAlternating;
                    break;

                case 3:
                    // Two lines
                    DisplayMode = Protocol.DisplayMode.TwoLines;
                    break;

                case 4:
                    // Three lines
                    DisplayMode = Protocol.DisplayMode.ThreeLines;
                    break;

                case 5:
                    // Four lines
                    // unused
                    break;

                case 6:
                    // unused
                    break;

                case 7:
                    // unused
                    break;
            }

            // Fetch following measurand codes
            var listAssignedMeasurands = new List<MVCode>();
            for (int idx = 1; idx < Data.Length; idx++)
            {
                MVCode mvc;
                if (MVCodesNumberTranslationNeeded)
                    mvc = cmdConv.MVIndexToMVCode(Data[idx]);
                else
                {
                    if (0xFF == Data[idx])
                        mvc = MVCode.INVALID;
                    else
                        mvc = (MVCode)Data[idx];
                }
                listAssignedMeasurands.Add(mvc);
            }

            AssignedMeasurands = listAssignedMeasurands;
        }
    }
}