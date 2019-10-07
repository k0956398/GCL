namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDisplayModeCmdParams : EECmdParamBase
    {
        public GetDisplayModeCmdParams() :
            base(0x0)
        {
        }
    }
}