using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputRangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to set type and range for one given output
        /// </summary>
        public SetOutputRangeCmdParams(byte portId, bool setVoltage, float rangeMin, float RangeMax) :
            base(0x0)
        {
            if (portId > 3 || portId < 1)
                throw new ArgumentOutOfRangeException("portId", "Value must be between 1 and 3");

            //... set bytes
        }
    }
}