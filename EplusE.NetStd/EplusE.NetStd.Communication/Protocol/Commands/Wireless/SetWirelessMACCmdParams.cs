using System;
using System.Linq;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetWirelessMACCmdParams : EECmdParamBase
    {
        public SetWirelessMACCmdParams(byte[] macAddr) :
            base(0x0)
        {
            SetCmdData(macAddr);
        }

        public SetWirelessMACCmdParams(string macAddrString) :
            base(0x0)
        {
            byte[] macAddr = macAddrString.Split(new char[] { ':', '-' }).Select(x => Convert.ToByte(x, 16)).ToArray();
            SetCmdData(macAddr);
        }

        private void SetCmdData(byte[] macAddr)
        {
            if (macAddr.Length != 6)
                throw new ArgumentException("Invalid MAC address (6 bytes required)", "macAddr");
            //... set bytes
        }
    }
}