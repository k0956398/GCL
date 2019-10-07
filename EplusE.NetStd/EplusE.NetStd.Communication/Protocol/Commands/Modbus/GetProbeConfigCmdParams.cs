namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetProbeConfigCmdParams : EECmdParamBase
    {
        public GetProbeConfigCmdParams(byte probeIdx) :
            base(0x0)
        {
            //... set bytes
        }
    }
}