using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetStatusBarVisCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Set visibility of status bar
        /// </summary>
        /// <param name="visibility">The visibility. 0 = invisible, 1 = always visible, 2 = dynamic.</param>
        public SetStatusBarVisCmdParams(byte visibility) :
            base(0x0)
        {
            if (visibility > 2)
                throw new ArgumentOutOfRangeException("visibility", "Only values 0, 1 and 2 are allowed");

            //... set bytes
        }
    }
}