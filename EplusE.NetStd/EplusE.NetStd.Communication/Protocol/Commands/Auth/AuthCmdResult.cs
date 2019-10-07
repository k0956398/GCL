using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class AuthCmdResult : EECmdResultBase
    {
        public UInt32 ExpirationIdleSec { get; private set; }

        internal UInt32 Challenge { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            Challenge = 0;

            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                if (Data[0] == 0x0)
                {
                    // Authentication request (request challenge)
                    Challenge = DataTypeConverter.ByteConverter.ToUInt32(Data, 1, reverseByteOrder);
                }
                else if (Data[0] == 0x1)
                {
                    // Authentication response
                    ExpirationIdleSec = DataTypeConverter.ByteConverter.ToUInt32(Data, 1, reverseByteOrder);
                }
                else if (Data[0] == 0x2)
                {
                    // Changed password (no additional data)
                }
                else
                    Code = EECmdResultCode.InvalidResult;
            }
        }
    }
}