using EplusE.Measurement;
using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class CounterResetCmdParams : EECmdParamBase
    {
        public CounterResetCmdParams(IList<CounterResetType> listReset) :
            base(0x0)
        {
            //... set bytes
        }

        public CounterResetCmdParams(IList<MVCode> listMV) :
            base(0x0)
        {
            if (listMV == null || listMV.Count == 0)
                throw new ArgumentException("listReset", "No arguments given");

            //... set bytes
        }

    }
}