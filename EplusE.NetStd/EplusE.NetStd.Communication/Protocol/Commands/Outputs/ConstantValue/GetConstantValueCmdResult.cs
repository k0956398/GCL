using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetConstantValueCmdResult : EECmdResultBase
    {
        public bool Enabled { get; private set; }

        public MVCode MV { get; private set; }

        public bool UseOutputMV { get; private set; }

        public float Value { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 6)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            if (Data[1] == 0xFF)
            {
                // Measurement value of output should be used
                UseOutputMV = true;
                Enabled = true;
                MV = MVCode.INVALID;
            }
            else if (Data[1] > 0)
            {
                // Measurement value given
                MV = (MVCode)Data[1];
                UseOutputMV = false;
                Enabled = true;
            }
            else
            {
                // Disabled
                Enabled = false;
                UseOutputMV = false;
                MV = MVCode.INVALID;
            }

            Value = DataTypeConverter.ByteConverter.ToFloat(Data, 2, reverseByteOrder);
        }
    }
}