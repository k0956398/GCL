using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetTunnelCmdParams : EECmdParamBase
    {
        public GetTunnelCmdParams(byte subModuleNr, byte[] payload, ushort timeout, byte protocolType = 0, byte portType = 0) :
            base(0x0, 0)
        {
            // Additional retries and wait in case command returns BUSY
            TunnelCmdBusyRetries = 5;
            TunnelCmdBusyWaitMs = 200;

            // Length of config block (subModuleNr, protocolType, portType [, timeout])
            byte confLen = (byte)(portType == 0 ? 5 : 3);

            //... set bytes
        }

        internal int TunnelCmdBusyRetries { get; private set; }
        internal int TunnelCmdBusyWaitMs { get; private set; }
    }
}