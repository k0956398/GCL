using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetOutputRangeCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to get type and range for one given output
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="optionalHybrid_U_I_Signal">to get type and range from hybrid port</param>
        public GetOutputRangeCmdParams(byte portId, byte? optionalHybrid_U_I_Signal = null) :
            base(0x0)
        {
            //... set bytes
        }
    }
}