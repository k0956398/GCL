namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOut1Out2RangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to set type and range for Out1 and Out2
        /// </summary>
        public SetOut1Out2RangeCmdParams(bool out1Voltage, float out1RangeMin, float out1RangeMax,
            bool out2Voltage, float out2RangeMin, float out2RangeMax) :
            base(0x0)
        {
            //... set bytes
        }
    }
}