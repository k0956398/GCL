namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDateFormatCmdParams : EECmdParamBase
    {
        public GetDateFormatCmdParams() :
            base(0x0)
        {
            //... set bytes
        }
    }
}