using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetUserTextCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Default constructor to get maximum user text length
        /// </summary>
        /// <param name="busAddr"></param>
        public SetUserTextCmdParams() :
            base(0x0)
        {
        }

        public SetUserTextCmdParams(string text) :
            base(0x0)
        {
            //... set bytes
        }
    }
}