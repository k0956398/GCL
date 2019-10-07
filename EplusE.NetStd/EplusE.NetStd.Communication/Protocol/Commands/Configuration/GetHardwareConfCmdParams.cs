namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetHardwareConfCmdParams : EECmdParamBase
    {
        public GetHardwareConfCmdParams() :
            base(0x0)
        {
        }
    }
}