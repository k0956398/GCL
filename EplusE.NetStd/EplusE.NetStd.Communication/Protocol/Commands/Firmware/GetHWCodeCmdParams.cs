namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetHWCodeCmdParams : EECmdParamBase
    {
        public GetHWCodeCmdParams() :
            base(0x00, 0, 0, null)
        {
        }
    }
}