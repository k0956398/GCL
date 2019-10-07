using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOutputConfigCmdResultBase : EECmdResultBase
    {
        protected internal GetOutputConfigCmdResultBase(OutputMode mode)
        {
            OutputMode = mode;
            Inactive = (OutputMode == OutputMode.Inactive);
        }

        public bool? ErrorIndicationEnabled { get; protected set; }
        public double? ErrorIndicationValue { get; protected set; }
        public bool Inactive { get; private set; }
        public MVCode MVCode { get; protected set; } = MVCode.INVALID;
        public OutputMode OutputMode { get; private set; }
        public ValueVariant? Variant { get; protected set; }

        protected ValueVariant? OptionalVariant(byte[] data, ref int dataIdx)
        {
            // Return variant if found
            if ((dataIdx + 1) <= Data.Length)
                return (ValueVariant)Data[dataIdx++];

            return null;
        }
    }
}