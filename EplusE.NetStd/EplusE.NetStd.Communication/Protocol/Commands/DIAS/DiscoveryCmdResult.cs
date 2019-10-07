using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class DiscoveryCmdResult : EECmdResultBase
    {
        public IList<FoundDevice> Devices { get; } = new List<FoundDevice>();

        public class FoundDevice
        {
            public ushort BusAddr { get; internal set; }
            public ushort HwCode { get; internal set; }
            public string ModelText { get; internal set; }
            public byte NativeProtocol { get; internal set; }
        }
    }
}