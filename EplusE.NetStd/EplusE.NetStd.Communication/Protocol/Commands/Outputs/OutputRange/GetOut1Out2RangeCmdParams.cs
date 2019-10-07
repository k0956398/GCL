namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetOut1Out2RangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to get type and range for Out1 and Out2
        /// </summary>
        public GetOut1Out2RangeCmdParams() :
            base(0x0)
        {
        }
    }
}