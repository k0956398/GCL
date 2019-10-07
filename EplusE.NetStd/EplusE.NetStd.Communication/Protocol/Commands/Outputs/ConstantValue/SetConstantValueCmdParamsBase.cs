using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetConstantValueCmdParamsBase : EECmdParamBase
    {
        protected internal SetConstantValueCmdParamsBase(byte portId, byte mode, byte mvCode, float value) :
            base(0x0)
        {
            if ((portId > 3 || portId < 1) && portId != 0xFF)
                throw new ArgumentOutOfRangeException("portId", "Only values 1, 2, 3 or 0xFF allowed");
            if (mode < 1 || mode > 3)
                throw new ArgumentOutOfRangeException("mode", "Only values 1, 2 or 3 allowed");

            //... set bytes
        }
    }
}