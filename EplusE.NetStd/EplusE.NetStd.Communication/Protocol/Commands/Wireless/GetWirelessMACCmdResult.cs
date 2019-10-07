using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetWirelessMACCmdResult : EECmdResultBase
    {
        public byte[] MACAddr { get; private set; }

        /// <summary>
        /// Wireless interface MAC-ID
        /// </summary>
        public string MACAddrString { get { return BitConverter.ToString(MACAddr); } }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 7)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            MACAddr = new byte[6];
            Array.Copy(Data, 1, MACAddr, 0, 6);
        }
    }
}