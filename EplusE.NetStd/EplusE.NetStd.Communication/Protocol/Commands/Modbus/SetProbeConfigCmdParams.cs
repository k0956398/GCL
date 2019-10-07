using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetProbeConfigCmdParams : EECmdParamBase
    {
        public SetProbeConfigCmdParams(byte probeIdx, byte probeBusAddr, bool isEEProbe, string probeName,
                                       DateTime calibExpiration, byte nrOfRetries, ushort timeoutMs) :
            base(0x0)
        {
            //... set bytes
        }
    }
}