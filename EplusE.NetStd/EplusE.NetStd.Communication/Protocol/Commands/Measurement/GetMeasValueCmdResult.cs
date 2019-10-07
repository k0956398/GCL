using EplusE.Measurement;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetMeasValueCmdResult : EECmdResultBase
    {
        public IList<KeyValuePair<MVCode, double>> MeasValues { get; } = new List<KeyValuePair<MVCode, double>>();

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < cmdParams.CmdData.Length + 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // First response byte is 0, data begins at 1
                int resultIdx = 1;

                // Get number of requested values from sent command data
                int nrValues = cmdParams.CmdData.Length / 2;

                for (int i = 0; i < nrValues; i++)
                {
                    // Get MVCode from sent command data
                    MVCode code = cmdConv.MVIndexToMVCode(cmdParams.CmdData[i * 2]);

                    // Get data type (try to get from MVCode if invalid)
                    MVDataType dataType = (MVDataType)Data[resultIdx++];

                    // Get value for data type
                    double value = double.NaN;
                    switch (dataType)
                    {
                        case MVDataType.Float:
                            if ((resultIdx + 4) <= Data.Length)
                                value = DataTypeConverter.ByteConverter.ToFloat(Data, resultIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                            resultIdx += 4;
                            break;

                        case MVDataType.Double:
                            if ((resultIdx + 8) <= Data.Length)
                                value = DataTypeConverter.ByteConverter.ToDouble(Data, resultIdx, reverseByteOrder);
                            resultIdx += 8;
                            break;
                    }

                    MeasValues.Add(new KeyValuePair<MVCode, double>(code, value));
                }
            }
        }
    }
}