namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetUserTextCmdParams : EECmdParamBase
    {
        public GetUserTextCmdParams() :
            base(0x0, 0, 0)
        {
        }
    }
}