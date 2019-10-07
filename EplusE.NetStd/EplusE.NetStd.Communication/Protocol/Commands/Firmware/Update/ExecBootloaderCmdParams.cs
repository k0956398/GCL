using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class ExecBootloaderCmdParams : EECmdParamBase
    {
        public ExecBootloaderCmdParams(byte[] payload) :
            base(0x0, 0, 0)
        {
            //... set bytes
        }
    }
}