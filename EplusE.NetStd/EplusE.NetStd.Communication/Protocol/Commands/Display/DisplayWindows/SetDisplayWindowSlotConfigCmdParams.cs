using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDisplayWindowSlotConfigCmdParams : EECmdParamBase
    {
        public SetDisplayWindowSlotConfigCmdParams(byte slotNr, byte dpIdx, ValueVariant variant, byte precision, string dpName) :
            base(0x0)
        {
            //... set bytes
        }
    }
}