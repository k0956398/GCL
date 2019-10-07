namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetConstantValueCmdParams_Off : SetConstantValueCmdParamsBase
    {
        /// <summary>
        /// Parameter constructor to turn off constant value output
        /// </summary>
        /// <param name="portId">1, 2, 3 or 0xFF</param>
        public SetConstantValueCmdParams_Off(byte portId) :
            base(portId, 1, 0, 0.0f)
        {
        }
    }
}