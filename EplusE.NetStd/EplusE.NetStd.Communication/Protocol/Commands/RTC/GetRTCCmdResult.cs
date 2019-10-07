using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetRTCCmdResult : EECmdResultBase
    {
        public float BattVoltage { get; private set; }

        public byte DaylightSavingMode { get; private set; }

        public bool DaylightSavingTime { get; private set; }

        public DateTimeOffset RTC { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 19)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            // First two bytes are 0!
            BattVoltage = DataTypeConverter.ByteConverter.ToFloat(Data, 2, reverseByteOrder);
            byte hour = Data[6];
            byte minute = Data[7];
            byte second = Data[8];
            byte day = Data[9];
            byte month = Data[10];
            ushort year = DataTypeConverter.ByteConverter.ToUInt16(Data, 11, reverseByteOrder);
            int offsetUTC = DataTypeConverter.ByteConverter.ToInt32(Data, 13, reverseByteOrder);
            DaylightSavingMode = Data[17];
            DaylightSavingTime = Data[18] != 0;

            // Convert data to DateTimeOffset object
            RTC = new DateTimeOffset(year, month, day, hour, minute, second, new TimeSpan(0, 0, offsetUTC));
        }
    }
}