using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetPortSettingsCmdParams : EECmdParamBase
    {
        public SetPortSettingsCmdParams(ComPortSettings portSettings, byte? portNr = null, byte? mode = null) :
            base(0x0, 0)
        {
            //... set bytes
        }
    }
}