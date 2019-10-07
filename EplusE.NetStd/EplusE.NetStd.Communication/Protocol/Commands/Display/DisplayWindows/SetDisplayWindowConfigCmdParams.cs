using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDisplayWindowConfigCmdParams : EECmdParamBase
    {
        public SetDisplayWindowConfigCmdParams(byte wndNr, bool wndEnabled, string wndName) :
            base(0x0)
        {
            //... set bytes
        }
    }
}