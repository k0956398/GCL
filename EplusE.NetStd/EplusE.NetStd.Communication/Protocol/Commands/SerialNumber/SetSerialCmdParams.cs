using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetSerialCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Default constructor to get maximum serial number length
        /// </summary>
        /// <param name="busAddr"></param>
        public SetSerialCmdParams() :
            base(0x0, 0)
        {
        }

        public SetSerialCmdParams(string serial, bool factoryData) :
            base(0x0, 0)
        {
            //... set bytes
        }
    }
}