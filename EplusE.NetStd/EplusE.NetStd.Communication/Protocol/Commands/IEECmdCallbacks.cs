using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Internal interface to call E+E protocol functions. Needed by commands.
    /// </summary>
    internal interface IEECmdCallbacks
    {
        CmdSpecialTreatment LookupOrCreateCmdSpecialTreatment(byte cmd);

        EECmdResultCode SendReceive(IEECommandParameter param, out byte[] response, byte[] rawDataToTx = null);

        void SendReceiveMulti(IEECommandParameter param, out IList<EE31CmdResponseData> responses);
    }
}