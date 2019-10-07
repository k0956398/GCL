namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetConstantValueCmdParams_Output : SetConstantValueCmdParamsBase
    {
        /// <summary>
        /// Parameter constructor to set constant value of output measurement value
        /// </summary>
        /// <param name="portId">1, 2, 3 or 0xFF</param>
        /// <param name="constValue">Output value</param>
        /// <param name="mode">1 = use all adjustment data,
        /// 2 = use only factory adjustment,
        /// 3 = use unadjusted</param>
        public SetConstantValueCmdParams_Output(byte portId, float constValue, byte mode = 1) :
            base(portId, mode, 0xff, constValue)
        {
        }
    }
}