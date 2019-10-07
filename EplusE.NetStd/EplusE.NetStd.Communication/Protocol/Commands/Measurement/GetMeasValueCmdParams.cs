using EplusE.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetMeasValueCmdParams : EECmdParamBase
    {
        private readonly bool _noConversion = false;

        public GetMeasValueCmdParams(IEnumerable<KeyValuePair<byte, ValueVariant>> listMVCodeAndVariant, bool isDpIdx = false) :
                    base(0x0)
        {
            //... set bytes
        }

        internal override void ConvertData(IEECmdConverters protConverters)
        {
            if (_noConversion)
                return;

            // Translate MVCode to index if neccessary
            for (int i = 0; i < base.CmdData.Length; i += 2)
                base.CmdData[i] = protConverters.MVCodeToEE31MVIndex((MVCode)base.CmdData[i]);
        }
    }
}