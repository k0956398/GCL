using EplusE.Measurement;
using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputConfigCmdParamsBase : EECmdParamBase
    {
        protected internal SetOutputConfigCmdParamsBase(byte portId, bool isInput = false) :
            base(0x0)
        {
            if (portId > 9 || portId < 1)
                throw new ArgumentOutOfRangeException("portId", "Value must be between 1 and 9");
        }

        internal override void ConvertData(IEECmdConverters protConverters)
        {
            //... set bytes
        }
    }
}