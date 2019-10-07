using EplusE.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDisplayModeCmdParams : EECmdParamBase
    {
        private IEnumerable<MVCode> _assignedMeasurands;

        private bool _mvCodesNumberTranslationNeeded;

        public SetDisplayModeCmdParams(IEnumerable<MVCode> assignedMeasurands,
                                                       bool MVCodesNumberTranslationNeeded, bool? backlightActive, DisplayMode? displayMode) :
            base(0x0)
        {
            if (null == assignedMeasurands)
                throw new ArgumentException("Command 0xB9 needs assigned measurands!", "assignedMeasurands");

            //... set bytes
        }

        internal override void ConvertData(IEECmdConverters protConverters)
        {
            // Build following measurand codes
            int idx = 1;
            foreach (MVCode mvc in _assignedMeasurands)
            {
                byte byValue;
                if (_mvCodesNumberTranslationNeeded)
                    byValue = protConverters.MVCodeToEE31MVIndex(mvc);
                else
                {
                    if (MVCode.INVALID == mvc)
                        byValue = 0xFF;
                    else
                        byValue = (byte)mvc;
                }
                base.CmdData[idx++] = byValue;
            }
        }
    }
}