using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetRTCCmdParams : EECmdParamBase
    {
        public SetRTCCmdParams(DateTimeOffset dateTimeOffset, byte daylightSavingMode) :
            base(0x0)
        {
            if (daylightSavingMode > 2)
                throw new ArgumentOutOfRangeException("daylightSavingMode", "Only values 0, 1 and 2 are allowed");

            //... set bytes
        }
    }
}