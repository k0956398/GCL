using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetConstantValueCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Parameter constructor to get constant value output settings
        /// </summary>
        /// <param name="portId">1, 2 or 3</param>
        public GetConstantValueCmdParams(byte portId) :
            base(0x0)
        {
            if (portId > 3 || portId < 1)
                throw new ArgumentOutOfRangeException("portId", "Only values 1, 2 or 3 allowed");

            //... set bytes
        }
    }
}