namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDisplayDetailsCmdParams : EECmdParamBase
    {
        public GetDisplayDetailsCmdParams() :
            base(0x0, null)
        {
        }
    }
}