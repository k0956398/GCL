namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetOut1Out2MVAndRangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to get measurement value and measurement range for Out1 and Out2
        /// </summary>
        public GetOut1Out2MVAndRangeCmdParams() :
            base(0x0)
        {
        }
    }
}