using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetFirmwareRevCmdResult : EECmdResultBase
    {
        public string Version { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 3)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // Create command specific result from data
                UInt32 major = Data[0];
                UInt32 minor = Data[1];
                UInt32 revision = 0;
                if (Data.Length >= 3)
                    revision = Data[2];

                if (0 != revision)
                    Version = string.Format("{0,2}.{1,2:00} Rev. {2,2}", major, minor, revision);
                else
                    Version = string.Format("{0,2}.{1,2:00}", major, minor);
            }
        }
    }
}