using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class SetDisplayDetailsCmdResult : EECmdResultBase
    {
        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received, response data valid
            if (Data.Length >= 3)
            {
                // 0x00 fixed (Byte), then Waiting time [1/100 msec] (WORD)
                UInt16 wait = DataTypeConverter.ByteConverter.ToUInt16(Data, 1, reverseByteOrder);
                if (0 != wait)
                    System.Threading.Thread.Sleep(wait * 100);
            }
        }
    }
}