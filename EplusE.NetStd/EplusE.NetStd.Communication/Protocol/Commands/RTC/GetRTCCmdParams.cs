namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetRTCCmdParams : EECmdParamBase
    {
        public GetRTCCmdParams() :
            base(0x0)
        {
            //... set bytes
        }
    }
}