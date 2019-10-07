using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetCustomUnitsCmdParams : EECmdParamBase
    {
        public SetCustomUnitsCmdParams(byte unitIdx, string unit) :
            base(0x0)
        {
            //... set bytes
        }
    }
}