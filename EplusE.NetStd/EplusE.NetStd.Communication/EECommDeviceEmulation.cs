using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    internal class EECommDeviceEmulation : IEECommDevice
    {
        private EmulationSettings settings;
        private int v;

        public EECommDeviceEmulation(int v, EmulationSettings settings)
        {
            this.v = v;
            this.settings = settings;
        }

        public int BytesToRead => throw new System.NotImplementedException();

        public int BytesToWrite => throw new System.NotImplementedException();

        public bool Connected => throw new System.NotImplementedException();

        public string InterfaceId => throw new System.NotImplementedException();

        public InterfaceType InterfaceType => throw new System.NotImplementedException();

        public bool IsUniAdapter => throw new System.NotImplementedException();

        public string ModelText => throw new System.NotImplementedException();

        public IScanConfiguration ScanConfigurationActive { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IEnumerable<IScanConfiguration> ScanConfigurationsUsed => throw new System.NotImplementedException();

        public void Disconnect()
        {
            throw new System.NotImplementedException();
        }

        public void EnsureConnection()
        {
            throw new System.NotImplementedException();
        }

        public byte ReadByte()
        {
            throw new System.NotImplementedException();
        }

        public int ReadBytes(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        public void WriteBytes(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}