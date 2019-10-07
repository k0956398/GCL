using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetProbeConfigCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Bus address of probe
        /// </summary>
        public byte BusAddr { get; private set; }

        /// <summary>
        /// Expiration date of calibration
        /// </summary>
        public DateTime CalibExpiration { get; private set; }

        /// <summary>
        /// Is probe an E+E product
        /// </summary>
        public bool IsEEProbe { get; private set; }

        /// <summary>
        /// Custom probe name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Number of communication retries (0 = use global setting, >0 = overrides global setting)
        /// </summary>
        public byte NrOfRetries { get; private set; }

        /// <summary>
        /// Communication timeout in milliseconds (0 = use global setting, >0 = overrides global setting)
        /// </summary>
        public ushort TimeoutMs { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 26)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            BusAddr = Data[1];
            IsEEProbe = Data[2] != 0;
            Name = StringHelper.ExtractStringContent(Data, 3, 16);
            int day = Math.Max(1, (int)Data[19]);
            int month = Math.Max(1, (int)Data[20]);
            int year = Math.Max(1, (int)DataTypeConverter.ByteConverter.ToUInt16(Data, 21, reverseByteOrder));
            CalibExpiration = new DateTime(year, month, day);
            NrOfRetries = Data[23];
            TimeoutMs = DataTypeConverter.ByteConverter.ToUInt16(Data, 24, reverseByteOrder);
        }
    }
}