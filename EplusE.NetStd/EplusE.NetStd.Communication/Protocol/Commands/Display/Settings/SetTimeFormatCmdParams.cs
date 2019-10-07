using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetTimeFormatCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Set date format
        /// </summary>
        /// <param name="format">The date format. 0 = hh:mm:ss, 1 = hh:mm.</param>
        public SetTimeFormatCmdParams(byte format) :
            base(0x0)
        {
            //... set bytes
        }
    }
}