using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetTunnelCmdResult : EECmdResultBase
    {
        public byte[] Payload { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // Payload begins at second byte
                Payload = new byte[Data.Length - 1];
                Array.Copy(Data, 1, Payload, 0, Data.Length - 1);
            }
        }
    }
}