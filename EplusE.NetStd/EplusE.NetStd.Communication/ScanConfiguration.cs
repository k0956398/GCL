namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Interface that encapsules properties required to scan for devices. Created through one of
    /// it's derived types (ScanConfigurationUART, ScanConfigurationBLE)
    /// </summary>
    public interface IScanConfiguration
    {
        InterfaceType Type { get; }

        IScanConfiguration MakeCopy();
    }
}