namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public enum CounterResetType : byte
    {
        Volume_Customer = 30,
        Volume_Total = 31,
        Temperature_Min = 50,
        Temperature_Max = 51,
        VolumetricFlow_Min = 52,
        VolumetricFlow_Max = 53,
        MassFlow_Min = 54,
        MassFlow_Max = 55,
        Velocity_Min = 56,
        Velocity_Max = 57,
        Volume_Min = 58,
        Volume_Max = 59,
        Pressure_Min = 60,
        Pressure_Max = 61,
        All = 255
    }
}