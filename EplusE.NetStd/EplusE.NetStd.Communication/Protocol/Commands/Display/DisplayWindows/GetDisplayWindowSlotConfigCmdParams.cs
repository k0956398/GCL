namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDisplayWindowSlotConfigCmdParams : EECmdParamBase
    {
        public GetDisplayWindowSlotConfigCmdParams(byte slotNr) :
            base(0x0)
        {
            //... set bytes
        }
    }
}