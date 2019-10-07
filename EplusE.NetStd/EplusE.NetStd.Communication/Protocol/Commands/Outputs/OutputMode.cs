namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public enum OutputMode : byte
    {
        Current = 0x00,
        Voltage = 0x01,
        Relais_Hysteresis = 0x10,
        Relais_Window = 0x11,
        Relais_ErrorIndication = 0x12,
        Pulse = 0x20,
        Inactive = 0xFF
    }
}