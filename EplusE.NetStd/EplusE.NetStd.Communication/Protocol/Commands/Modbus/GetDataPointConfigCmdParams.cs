namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDataPointConfigCmdParams : EECmdParamBase
    {
        public GetDataPointConfigCmdParams(byte dpIdx) :
            base(0x0)
        {
            //... set bytes
        }
    }
}