namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Available EE31 protocol commands.
    /// </summary>
    public enum EE31Command : byte
    {
        /// <summary>
        /// EE31 command 0x61
        /// </summary>
        GetSerial,

        /// <summary>
        /// EE31 command 0x60, 0xF0
        /// </summary>
        SetSerial,

        /// <summary>
        /// EE31 command 0x64
        /// </summary>
        GetFWVersion,

        /// <summary>
        /// EE31 command 0x32
        /// </summary>
        GetUserText,

        /// <summary>
        /// EE31 command 0x3B
        /// </summary>
        SetUserText,

        /// <summary>
        /// EE31 command 0x2E
        /// </summary>
        Authenticate,

        /// <summary>
        /// EE31 command 0xBA
        /// </summary>
        GetDisplayMode,

        /// <summary>
        /// EE31 command 0xB9
        /// </summary>
        SetDisplayMode,

        /// <summary>
        /// EE31 command 0xDF
        /// </summary>
        GetDisplayDetails,

        /// <summary>
        /// EE31 command 0xDF
        /// </summary>
        SetDisplayDetails,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetDisplayWindowConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetDisplayWindowConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetDisplayWindowSlotConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetDisplayWindowSlotConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetProbeConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetProbeConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetDataPointConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetDataPointConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetCustomUnits,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetCustomUnits,

        /// <summary>
        /// EE31 command 0x30
        /// </summary>
        GetMeasurementValue,

        /// <summary>
        /// EE31 command 0xE3
        /// </summary>
        GetPortSettings,

        /// <summary>
        /// EE31 command 0xE2
        /// </summary>
        SetPortSettings,

        /// <summary>
        /// EE31 command 0xF2
        /// </summary>
        GetTunneledData,

        /// <summary>
        /// EE31 command 0x37
        /// </summary>
        GetHWCode,

        /// <summary>
        /// EE31 command 0xF1
        /// </summary>
        SwitchToBootloader,

        /// <summary>
        /// EE31 command 0xF1
        /// </summary>
        StartFWUpdate,

        /// <summary>
        /// EE31 command 0xF1
        /// </summary>
        ExecBootloader,

        /// <summary>
        /// EE31 command 0xF1
        /// </summary>
        UploadFWBlock,

        /// <summary>
        /// EE31 command 0xF1
        /// </summary>
        EndFWUpdate,

        /// <summary>
        /// EE31 command 0x52
        /// </summary>
        Discovery,

        /// <summary>
        /// EE31 command 0x3F
        /// </summary>
        DiscoveryAck,

        /// <summary>
        /// EE31 command 0x2D
        /// </summary>
        GetOutputConfig,

        /// <summary>
        /// EE31 command 0x2C
        /// </summary>
        SetOutputConfig,

        /// <summary>
        /// EE31 command 0x55
        /// </summary>
        GetOut1Out2Range,

        /// <summary>
        /// EE31 command 0x54
        /// </summary>
        SetOut1Out2Range,

        /// <summary>
        /// EE31 command 0xA1
        /// </summary>
        GetOutputRange,

        /// <summary>
        /// EE31 command 0xA0
        /// </summary>
        SetOutputRange,

        /// <summary>
        /// EE31 command 0x57
        /// </summary>
        GetOut1Out2MVAndRange,

        /// <summary>
        /// EE31 command 0x56
        /// </summary>
        SetOut1Out2MVAndRange,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetWirelessMaxIdleTime,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetWirelessMaxIdleTime,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetWirelessMACAddr,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetWirelessMACAddr,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetStatusBarVis,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetStatusBarVis,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetDateFormat,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetDateFormat,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetTimeFormat,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetTimeFormat,

        /// <summary>
        /// EE31 command 0x24
        /// </summary>
        GetRTC,

        /// <summary>
        /// EE31 command 0x24
        /// </summary>
        SetRTC,

        /// <summary>
        /// EE31 command 0x1C
        /// </summary>
        GetHardwareConf,

        /// <summary>
        /// EE31 command 0xE7
        /// </summary>
        GetConstantValue,

        /// <summary>
        /// EE31 command 0xE7
        /// </summary>
        SetConstantValue,

        /// <summary>
        /// EE31 command 0x38
        /// </summary>
        FactoryReset,

        /// <summary>
        /// EE31 command 0x39
        /// </summary>
        CounterReset,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        GetModbusSlaveConfig,

        /// <summary>
        /// EE31 command 0xEB
        /// </summary>
        SetModbusSlaveConfig,

        /// <summary>
        /// EE31 command 0xD5
        /// </summary>
        GetModeConf,

        /// <summary>
        /// EE31 command 0xD5
        /// </summary>
        SetModeConf
    }
}