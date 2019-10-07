using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDateFormatCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Set date format
        /// </summary>
        /// <param name="format">The date format. 0 = dd.mm.yy, 1 = mm.dd.yy, 2 = yyyy-mm-dd.</param>
        public SetDateFormatCmdParams(byte format) :
            base(0x0)
        {
            //... set bytes
        }
    }
}