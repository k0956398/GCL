using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetHWCodeCmdResult : EECmdResultBase
    {
        public IEnumerable<ushort> HWCodes { get; private set; }

        public IEnumerable<byte> MajorVersions { get; private set; }

        public IEnumerable<byte> MinorVersions { get; private set; }

        public IEnumerable<byte> Revisions { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 2)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // Create command specific result from data
                var hwCodes = new List<ushort>();
                var major = new List<byte>();
                var minor = new List<byte>();
                var rev = new List<byte>();

                for (int i = 0; i < Data.Length; i++)
                {
                    hwCodes.Add(DataTypeConverter.ByteConverter.ToUInt16(Data, i, reverseByteOrder));
                    i += 2;

                    if (i + 2 < Data.Length)
                    {
                        // Optional information for this HW code
                        major.Add(Data[i]);
                        minor.Add(Data[i + 1]);
                        rev.Add(Data[i + 2]);
                    }
                    else
                        break;
                }

                HWCodes = hwCodes;
                MajorVersions = major;
                MinorVersions = minor;
                Revisions = rev;
            }
        }
    }
}