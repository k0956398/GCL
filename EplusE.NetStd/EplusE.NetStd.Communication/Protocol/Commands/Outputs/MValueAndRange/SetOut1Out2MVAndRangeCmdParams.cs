using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOut1Out2MVAndRangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to set measurement value and measurement range for Out1 and Out2
        /// </summary>
        public SetOut1Out2MVAndRangeCmdParams(MVCode out1MVCode, float out1MVRangeMin, float out1MVRangeMax,
            MVCode out2MVCode, float out2MVRangeMin, float out2MVRangeMax) :
            base(0x0)
        {
            //... set bytes
        }

        internal override void ConvertData(IEECmdConverters protConverters)
        {
            // Translate MVCode to index if neccessary
            base.CmdData[0] = protConverters.MVCodeToEE31MVIndex((MVCode)base.CmdData[0]);
            base.CmdData[1] = protConverters.MVCodeToEE31MVIndex((MVCode)base.CmdData[1]);
        }
    }
}