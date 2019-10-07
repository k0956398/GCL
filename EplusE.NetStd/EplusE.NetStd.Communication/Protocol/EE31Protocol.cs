using EplusE.Measurement;
using EplusE.NetStd.Communication.Protocol.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// The E+E Elektronik EE31 protocol implementation. Requires IEECommDevice to handle the communication layer.
    /// </summary>
    internal class EE31Protocol : IEECommProtocol, IEECmdCallbacks, IEECmdConverters
    {
        private readonly object __lock_SendCmdMultiResponse = new object();

        /// <summary>
        /// Implemented EE31 protocol commands. Mapping between command enum and command, parameter, result classes.
        /// Not every command needs it's own command and result class. In that case the base classes might be used.
        /// </summary>
        private readonly Dictionary<EE31Command, IEECommCommand> _listCommands = new Dictionary<EE31Command, IEECommCommand>()
            {
                { EE31Command.GetSerial, new GetSerialCmd() },
                { EE31Command.SetSerial, new EECmdBase<SetSerialCmdParams, SetSerialCmdResult>() },
                { EE31Command.GetFWVersion, new EECmdBase<GetFirmwareRevCmdParams, GetFirmwareRevCmdResult>() },
                { EE31Command.GetUserText, new EECmdBase<GetUserTextCmdParams, GetUserTextCmdResult>() },
                { EE31Command.SetUserText, new EECmdBase<SetUserTextCmdParams, SetUserTextCmdResult>() },
                { EE31Command.Authenticate, new AuthCmd() },
                { EE31Command.GetDisplayMode, new EECmdBase<GetDisplayModeCmdParams, GetDisplayModeCmdResult>() },
                { EE31Command.SetDisplayMode, new EECmdBase<SetDisplayModeCmdParams, EECmdResultBase>() },
                { EE31Command.GetDisplayDetails, new EECmdBase<GetDisplayDetailsCmdParams, GetDisplayDetailsCmdResult>() },
                { EE31Command.SetDisplayDetails, new EECmdBase<SetDisplayDetailsCmdParams, SetDisplayDetailsCmdResult>() },
                { EE31Command.GetDisplayWindowConfig, new EECmdBase<GetDisplayWindowConfigCmdParams, GetDisplayWindowConfigCmdResult>() },
                { EE31Command.SetDisplayWindowConfig, new EECmdBase<SetDisplayWindowConfigCmdParams, EECmdResultBase>() },
                { EE31Command.GetDisplayWindowSlotConfig, new EECmdBase<GetDisplayWindowSlotConfigCmdParams, GetDisplayWindowSlotConfigCmdResult>() },
                { EE31Command.SetDisplayWindowSlotConfig, new EECmdBase<SetDisplayWindowSlotConfigCmdParams, EECmdResultBase>() },
                { EE31Command.GetProbeConfig, new EECmdBase<GetProbeConfigCmdParams, GetProbeConfigCmdResult>() },
                { EE31Command.SetProbeConfig, new EECmdBase<SetProbeConfigCmdParams, EECmdResultBase>() },
                { EE31Command.GetDataPointConfig, new EECmdBase<GetDataPointConfigCmdParams, GetDataPointConfigCmdResult>() },
                { EE31Command.SetDataPointConfig, new EECmdBase<SetDataPointConfigCmdParams, EECmdResultBase>() },
                { EE31Command.GetCustomUnits, new EECmdBase<GetCustomUnitsCmdParams, GetCustomUnitsCmdResult>() },
                { EE31Command.SetCustomUnits, new EECmdBase<SetCustomUnitsCmdParams, EECmdResultBase>() },
                { EE31Command.GetMeasurementValue, new EECmdBase<GetMeasValueCmdParams, GetMeasValueCmdResult>() },
                { EE31Command.GetPortSettings, new EECmdBase<GetPortSettingsCmdParams, GetPortSettingsCmdResult>() },
                { EE31Command.SetPortSettings, new EECmdBase<SetPortSettingsCmdParams, SetPortSettingsCmdResult>() },
                { EE31Command.GetTunneledData, new GetTunnelCmd() },
                { EE31Command.GetHWCode, new EECmdBase<GetHWCodeCmdParams, GetHWCodeCmdResult>() },
                { EE31Command.SwitchToBootloader, new EECmdBase<SwitchBootloaderCmdParams, SwitchBootloaderCmdResult>() },
                { EE31Command.ExecBootloader, new EECmdBase<ExecBootloaderCmdParams, EECmdResultBase>() },
                { EE31Command.StartFWUpdate, new EECmdBase<StartFWUpdateCmdParams, StartFWUpdateCmdResult>() },
                { EE31Command.UploadFWBlock, new UploadBlockCmd() },
                { EE31Command.EndFWUpdate, new EECmdBase<EndFWUpdateCmdParams, EndFWUpdateCmdResult>() },
                { EE31Command.Discovery, new DiscoveryCmd() },
                { EE31Command.DiscoveryAck, new DiscoveryAckCmd() },
                { EE31Command.GetOutputConfig, new GetOutputConfigCmd() },
                { EE31Command.SetOutputConfig, new EECmdBase<SetOutputConfigCmdParamsBase, EECmdResultBase>() },
                { EE31Command.GetOut1Out2Range, new EECmdBase<GetOut1Out2RangeCmdParams, GetOut1Out2RangeCmdResult>() },
                { EE31Command.SetOut1Out2Range, new EECmdBase<SetOut1Out2RangeCmdParams, EECmdResultBase>() },
                { EE31Command.GetOutputRange, new EECmdBase<GetOutputRangeCmdParams, GetOutputRangeCmdResult>() },
                { EE31Command.SetOutputRange, new EECmdBase<SetOutputRangeCmdParams, EECmdResultBase>() },
                { EE31Command.GetOut1Out2MVAndRange, new EECmdBase<GetOut1Out2MVAndRangeCmdParams, GetOut1Out2MVAndRangeRangeCmdResult>() },
                { EE31Command.SetOut1Out2MVAndRange, new EECmdBase<SetOut1Out2MVAndRangeCmdParams, EECmdResultBase>() },
                { EE31Command.GetWirelessMaxIdleTime, new EECmdBase<GetWirelessMaxIdleTimeCmdParams, GetWirelessMaxIdleTimeCmdResult>() },
                { EE31Command.SetWirelessMaxIdleTime, new EECmdBase<SetWirelessMaxIdleTimeCmdParams, EECmdResultBase>() },
                { EE31Command.GetWirelessMACAddr, new EECmdBase<GetWirelessMACCmdParams, GetWirelessMACCmdResult>() },
                { EE31Command.SetWirelessMACAddr, new EECmdBase<SetWirelessMACCmdParams, EECmdResultBase>() },
                { EE31Command.GetStatusBarVis, new EECmdBase<GetStatusBarVisCmdParams, GetStatusBarVisCmdResult>() },
                { EE31Command.SetStatusBarVis, new EECmdBase<SetStatusBarVisCmdParams, EECmdResultBase>() },
                { EE31Command.GetDateFormat, new EECmdBase<GetDateFormatCmdParams, GetDateFormatCmdResult>() },
                { EE31Command.SetDateFormat, new EECmdBase<SetDateFormatCmdParams, EECmdResultBase>() },
                { EE31Command.GetTimeFormat, new EECmdBase<GetTimeFormatCmdParams, GetTimeFormatCmdResult>() },
                { EE31Command.SetTimeFormat, new EECmdBase<SetTimeFormatCmdParams, EECmdResultBase>() },
                { EE31Command.GetRTC, new EECmdBase<GetRTCCmdParams, GetRTCCmdResult>() },
                { EE31Command.SetRTC, new EECmdBase<SetRTCCmdParams, EECmdResultBase>() },
                { EE31Command.GetHardwareConf, new EECmdBase<GetHardwareConfCmdParams, GetHardwareConfCmdResult>() },
                { EE31Command.GetConstantValue, new EECmdBase<GetConstantValueCmdParams, GetConstantValueCmdResult>() },
                { EE31Command.SetConstantValue, new EECmdBase<SetConstantValueCmdParamsBase, EECmdResultBase>() },
                { EE31Command.FactoryReset, new EECmdBase<FactoryResetCmdParams, EECmdResultBase>() },
                { EE31Command.CounterReset, new EECmdBase<CounterResetCmdParams, EECmdResultBase>() },
                { EE31Command.GetModbusSlaveConfig, new EECmdBase<GetModbusSlaveConfigCmdParams, GetModbusSlaveConfigCmdResult>() },
                { EE31Command.SetModbusSlaveConfig, new EECmdBase<SetModbusSlaveConfigCmdParams, EECmdResultBase>() },
                { EE31Command.GetModeConf, new EECmdBase<GetModeConfCmdParams, GetModeConfCmdResult>() },
                { EE31Command.SetModeConf, new EECmdBase<SetModeConfCmdParams, EECmdResultBase>() }
            };

        private DateTime timestampLastTx = DateTime.UtcNow;

        /// <summary>
        /// Constructor. The protocol must have an underlying communication layer.
        /// </summary>
        /// <param name="communicationInterface">The communication layer</param>
        internal EE31Protocol(IEECommDevice communicationInterface)
        {
            CommunicationInterface = communicationInterface;

            // Default protocol configuration
            CmdTimeoutAutoRetries = 1;
            CmdTimeoutMSec = 2000;
            IncludeNetworkAddress = true;
            MaxPayload = 255;
            SIUS = SIUSUnit.SI;
            CfgMVCodeTranslations = null;
            CmdSpecialTreatments = new Dictionary<byte, CmdSpecialTreatment>();
            UseMVCodeInsteadOfEE31Index = true;
        }

        /// <summary>
        /// Gets or sets the MVCode translations for specific EE31 commands (i.e. 0x67).
        /// </summary>
        /// <value>The MVCode translations for specific EE31 commands (i.e. 0x67).</value>
        public IDictionary<byte, IList<MVCodeTranslationData>> CfgMVCodeTranslations { get; set; }

        /// <summary>
        /// EE31 protocol commands known as not supported by device or whose parameters (floats)
        /// have to be reversed on byte level (wrong byte order).
        /// </summary>
        public IDictionary<byte, CmdSpecialTreatment> CmdSpecialTreatments { get; set; }

        /// <summary>
        /// Number of cmd retries automatically done in case of time out (no response received)
        /// </summary>
        public UInt32 CmdTimeoutAutoRetries { get; set; }

        /// <summary>
        /// Timeout value [msec] for commands
        /// </summary>
        public UInt32 CmdTimeoutMSec { get; set; }

        /// <summary>
        /// Two known dialects: With or without 16 bit network address prefix. Most products use the
        /// dialect with 16 bit network address prefix.
        /// </summary>
        public bool IncludeNetworkAddress { get; set; }

        /// <summary>
        /// Gets or sets the maximum payload capacity.
        /// </summary>
        public UInt32 MaxPayload { get; set; }

        /// <summary>
        /// Global measurement unit system (SI or US) of device (if any, may be undefined!)
        /// (Typically factory defined using order code E01)
        /// </summary>
        public SIUSUnit SIUS { get; set; }

        /// <summary>
        /// Target transmitter address for all commands
        /// </summary>
        public ushort TransmitterBusAddr { get; set; }

        /// <summary>
        /// Use MVCode enum values instead of EE31 index numbers to identify measurands (with
        /// unit)? Only newer devices like EE210 support this.
        /// </summary>
        public bool UseMVCodeInsteadOfEE31Index { get; set; }

        internal IEECommDevice CommunicationInterface { get; private set; }

        /// <summary>
        /// Gets or sets the measurement value data types (float, double, etc).
        /// </summary>
        /// <value>
        /// The measurement value data types (float, double, etc).
        /// </value>
        protected internal IDictionary<MVCode, MVDataType> MVDataTypes { get; set; }

        /// <summary>
        /// Free all resources and remove protocol from factory
        /// </summary>
        public void Dispose()
        {
            EECommProtocolFactory.RemoveEE31Protocol(this);
        }

        /// <summary>
        /// Execute protocol command
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <param name="parameter">Interface to command parameters</param>
        /// <returns>Interface to executed command result</returns>
        public IEECommandResult ExecuteCommand(EE31Command cmd, IEECommandParameter parameter)
        {
            if (!_listCommands.ContainsKey(cmd))
                return new EECmdResultBase() { Code = EECmdResultCode.UnknownCmd };
            if (parameter == null)
                return new EECmdResultBase() { Code = EECmdResultCode.InvalidParamClass };

            // Set command basics and special treatments needed to handle commands properly
            var command = _listCommands[cmd];
            command.ProtocolCallbacks = this;
            command.ProtocolConverters = this;

            // Some command parameters need special conversion
            (parameter as EECmdParamBase).ConvertData(this);

            return command.Execute(parameter);
        }

        public CmdSpecialTreatment LookupOrCreateCmdSpecialTreatment(byte cmd)
        {
            CmdSpecialTreatment cst = null;
            if (CmdSpecialTreatments.ContainsKey(cmd))
                cst = CmdSpecialTreatments[cmd];
            else
            {
                cst = new CmdSpecialTreatment();
                CmdSpecialTreatments[cmd] = cst;
            }
            return cst;
        }

        /// <summary>
        /// Converts the MVCode to EE31 standardized measval index (Cmds 0x33/0x34).
        /// </summary>
        /// <param name="mvCode">The mv code.</param>
        /// <returns></returns>
        public byte MVCodeToEE31MVIndex(MVCode mvCode)
        {
            // Use MVCode enum values instead of EE31 index numbers to identify measurands (with
            // unit). Only newer devices like EE210 support this.
            if (UseMVCodeInsteadOfEE31Index)
                return (byte)mvCode;

            foreach (byte cmd in new byte[] { 0x33, 0x67 })
            {
                if (null != CfgMVCodeTranslations && CfgMVCodeTranslations.ContainsKey(cmd) && CfgMVCodeTranslations[cmd].Count > 0)
                {
                    // If unit override exists, try to find there
                    MVCodeTranslationData mvcTD = CfgMVCodeTranslations[cmd].FirstOrDefault(a => a.MVCode == mvCode || a.MVCodeUS == mvCode);
                    if (null != mvcTD)
                        return (byte)mvcTD.EE31Index;
                }
            }

            switch (mvCode)
            {
                case MVCode.TEMP__DEG_C:    // Temp. [°C]
                case MVCode.TEMP__DEG_F:    // Temp. [°F]
                case MVCode.TEMP__DEG_K:    // Temp. [K]
                    return 0;

                case MVCode.RH__PCT:        // RH [%rH]
                    return 1;

                case MVCode.e__MBAR:        // Water vapor partial pressure e [mbar]
                case MVCode.e__PSI:         // Water vapor partial pressure e [psi]
                    return 2;

                case MVCode.Td__DEG_C:      // Dew point temperature Td [°C]
                case MVCode.Td__DEG_F:      // Dew point temperature Td [°F]
                case MVCode.Td__DEG_K:      // Dew point temperature Td [K]
                    return 3;

                case MVCode.Tw__DEG_C:      // Wet bulb temperature Tw [°C]
                case MVCode.Tw__DEG_F:      // Wet bulb temperature Tw [°F]
                case MVCode.Tw__DEG_K:      // Wet bulb temperature Tw [K]
                    return 4;

                case MVCode.dv__G_M3:       // Absolute humidity dv [g/m3]
                case MVCode.dv__GR_FT3:     // Absolute humidity dv [gr/ft3]
                    return 5;

                case MVCode.r__G_KG:        // Mixing ratio r [g/kg]
                case MVCode.r__GR_LB:       // Mixing ratio r [gr/lb]
                    return 6;

                case MVCode.h__KJ_KG:       // Enthalpy h [kJ/kg]
                case MVCode.h__FT_LBF_LB:   // Enthalpy h [ft lbf/lb]
                case MVCode.h__BTU_LB:      // Enthalpy h [BTU/lb]
                    return 7;

                case MVCode.TdTf__DEG_C:    // Td/Tf [°C]
                case MVCode.TdTf__DEG_F:    // Td/Tf [°F]
                case MVCode.TdTf__DEG_K:    // Td/Tf [K]
                    return 8;

                case MVCode.CO2_RAW__PPM:   // CO2 raw [ppm]
                    return 10;

                case MVCode.CO2_MEAN__PPM:  // CO2 mean value (Median 11) [ppm]
                    return 11;

                case MVCode.V__M_PER_SEC:   // (Air) Velocity [m/s]
                case MVCode.V__FT_PER_MIN:  // (Air) Velocity [ft/min]
                    return 12;

                case MVCode.Aw__1:          // Aw [1]
                    return 13;

                case MVCode.X__PPM:         // Water content X [ppm]
                    return 14;

                case MVCode.Wv__PPM:        // Volume concentration (Humidity!) [ppm]
                    return 27;

                case MVCode.PctS__PCT:      // %S [%]
                    return 28;

                case MVCode.O2_MEAN__PCT:   // O2 mean value [%]
                    return 40;

                    //case AirPres__MBAR:       // Air pressure [mbar]
            }
            return 0xFF;
        }

        /// <summary>
        /// Converts the EE31 standardized measval index (Cmds 0x33/0x34) to MVCode.
        /// </summary>
        /// <param name="ee31MVIdx"></param>
        /// <returns></returns>
        public MVCode MVIndexToMVCode(int ee31MVIdx)
        {
            if (UseMVCodeInsteadOfEE31Index)
            {
                // Use MVCode enum values instead of EE31 index numbers to identify measurands
                // (with unit). Only newer devices like EE210 support this.
                try
                {
                    MVCode mvCode = (MVCode)ee31MVIdx;
                    if (Enum.IsDefined(typeof(MVCode), mvCode))
                    {
                        return mvCode;
                    }
                }
                catch (Exception ex)
                {
                    Diagnostic.Msg(1, "MVIndexToMVCode", "Exception: " + ex.Message);
                }
                return MVCode.INVALID;
            }

            bool bIsUnitSystemUS = SIUS == SIUSUnit.US;

            foreach (byte cmd in new byte[] { 0x33, 0x67 })
            {
                if (null != CfgMVCodeTranslations && CfgMVCodeTranslations.ContainsKey(cmd) && CfgMVCodeTranslations[cmd].Count > 0)
                {
                    // If unit override exists, try to find there
                    MVCodeTranslationData mvcTD = CfgMVCodeTranslations[cmd].FirstOrDefault(a => a.EE31Index == ee31MVIdx);
                    if (null != mvcTD)
                    {
                        if (bIsUnitSystemUS)
                            return mvcTD.MVCodeUS;
                        return mvcTD.MVCode;
                    }
                }
            }

            switch (ee31MVIdx)
            {
                case 0:     // Temperature T [°C; °F]
                    if (bIsUnitSystemUS)
                        return MVCode.TEMP__DEG_F;
                    return MVCode.TEMP__DEG_C;

                case 1:     // Relative Humidity RH [%rH]
                    return MVCode.RH__PCT;

                case 2:     // Water vapor partial pressure e [mbar; psi]
                    if (bIsUnitSystemUS)
                        return MVCode.e__PSI;
                    return MVCode.e__MBAR;

                case 3:     // Dew point Td [°C; °F]
                    if (bIsUnitSystemUS)
                        return MVCode.Td__DEG_F;
                    return MVCode.Td__DEG_C;

                case 4:     // Wet bulb Tw [°C; °F]
                    if (bIsUnitSystemUS)
                        return MVCode.Tw__DEG_F;
                    return MVCode.Tw__DEG_C;

                case 5:     // Water vapor concentration dv [g/m3; gr/ft3]
                    if (bIsUnitSystemUS)
                        return MVCode.dv__GR_FT3;
                    return MVCode.dv__G_M3;

                case 6:     // Mixing ratio r [g/kg; gr/lb]
                    if (bIsUnitSystemUS)
                        return MVCode.r__GR_LB;
                    return MVCode.r__G_KG;

                case 7:     // Enthalpy h [kJ/kg; ft lbf/lb]
                    if (bIsUnitSystemUS)
                        return MVCode.h__FT_LBF_LB;
                    return MVCode.h__KJ_KG;

                case 8:     // Dew point Td or Frost point Tf [°C; °F]
                    if (bIsUnitSystemUS)
                        return MVCode.TdTf__DEG_F;
                    return MVCode.TdTf__DEG_C;

                case 10:    // CO2 raw [ppm]
                    return MVCode.CO2_RAW__PPM;

                case 11:    // CO2 mean [ppm]
                    return MVCode.CO2_MEAN__PPM;

                case 12:    // (Air) Velocity [m/s; ft/min]
                    if (bIsUnitSystemUS)
                        return MVCode.V__FT_PER_MIN;
                    return MVCode.V__M_PER_SEC;

                case 13:    // Aw [1]
                    return MVCode.Aw__1;

                case 14:    // Water content X [ppm]
                    return MVCode.X__PPM;

                case 27:    // Volume concentration (Humidity!) [ppm]
                    return MVCode.Wv__PPM;

                case 28:    // %S [%]
                    return MVCode.PctS__PCT;

                case 40:    // O2 mean value [%]
                    return MVCode.O2_MEAN__PCT;
            }
            return MVCode.INVALID;
        }

        /// <summary>
        /// Sends a command to the device and receives a response.
        /// </summary>
        /// <param name="param">The command parameter data.</param>
        /// <param name="response">@ACK: Payload data (without ACK), @NAK: Error code (1 Byte)</param>
        /// <param name="rawDataToTx">Raw data bytes to transmit immediately after sending the command frame.</param>
        /// <returns></returns>
        public EECmdResultCode SendReceive(IEECommandParameter param, out byte[] response, byte[] rawDataToTx = null)
        {
            response = null;
            int rxBusAddr = 0;

            try
            {
                SendCmdMultiResponse(CommunicationInterface, param.Cmd,
                    param.CmdData,
                    param.CmdTries ?? CmdTimeoutAutoRetries,
                    param.CmdTimeoutMs ?? CmdTimeoutMSec,
                    param.CmdBusAddr ?? TransmitterBusAddr,
                    false,
                    out IList<EE31CmdResponseData> responses,
                    rawDataToTx);

                if (responses.Count > 0)
                {
                    response = responses[0].Response;
                    rxBusAddr = responses[0].RxBusAddr;

                    // Evaluate result
                    if (responses[0].Result == EE31Result.ACK)
                        return EECmdResultCode.Success;
                    else if (responses[0].Result == EE31Result.NAK &&
                        response.Length > 0 && response[0] == 0xF9)
                        return EECmdResultCode.Busy;
                    else if (responses[0].Result == EE31Result.NAK &&
                        response.Length > 0 && response[0] == 0xFC)
                        return EECmdResultCode.InvalidParameter;
                    else if (responses[0].Result == EE31Result.NAK &&
                        response.Length > 0 && response[0] == 0xFE)
                        return EECmdResultCode.UnknownCmd;
                    else
                        return EECmdResultCode.Failed;
                }
            }
            catch (System.IO.IOException) { }  // IOException happens i.e. when USB hardware is disconnected
            catch (Exception ex)
            {
                Diagnostic.Msg(1, string.Format("SendReceive ({0:X2})", param.Cmd), "Exception: " + ex.Message);
            }

            return EECmdResultCode.Failed;
        }

        /// <summary>
        /// Sends a command to the device and receives multiple responses.
        /// </summary>
        /// <param name="param">The command parameter data.</param>
        /// <param name="response">@ACK: Payload data (without ACK), @NAK: Error code (1 Byte)</param>
        /// <returns></returns>
        public void SendReceiveMulti(IEECommandParameter param, out IList<EE31CmdResponseData> responses)
        {
            responses = null;
            try
            {
                SendCmdMultiResponse(CommunicationInterface,
                    param.Cmd,
                    param.CmdData,
                    param.CmdTries ?? CmdTimeoutAutoRetries,
                    param.CmdTimeoutMs ?? CmdTimeoutMSec,
                    param.CmdBusAddr ?? TransmitterBusAddr,
                    true,
                    out responses);
            }
            catch (Exception ex)
            {
                // i.e. TimeoutException may occur, no problem
                Diagnostic.Msg(1, string.Format("SendReceiveMulti ({0:X2})", param.Cmd), "Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Sends a command to the device, expects multiple responses (i.e. broadcast message).
        /// </summary>
        /// <param name="commInterface">The communication interface</param>
        /// <param name="cmd">The command.</param>
        /// <param name="param">The command parameters.</param>
        /// <param name="cmdTimeoutRetries">Number of automatic retries in case of timeout.</param>
        /// <param name="cmdTimeoutMs">Command timeout in milliseconds.</param>
        /// <param name="busAddr">The target bus addr.</param>
        /// <param name="multiResponsesExpected">The multi responses expected.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="rawDataToTx">Raw data bytes to transmit immediately after sending the command frame.</param>
        private void SendCmdMultiResponse(IEECommDevice commInterface, byte cmd, byte[] param, uint cmdTimeoutRetries, uint cmdTimeoutMs,
                                          int busAddr, bool multiResponsesExpected, out IList<EE31CmdResponseData> responses, byte[] rawDataToTx = null)
        {
            lock (__lock_SendCmdMultiResponse)
            {
                responses = new List<EE31CmdResponseData>();

                commInterface.EnsureConnection();

                IList<byte> txBuffer = new List<byte>();

                if (IncludeNetworkAddress || 0 != busAddr)
                {
                    // 16 bit network address
                    UInt16 addr = (UInt16)busAddr;
                    byte[] flatBytes = BitConverter.GetBytes(addr);
                    if (2 != flatBytes.Length)
                        throw new ArgumentException("UInt16 must have 2 bytes!");
                    txBuffer.Add(flatBytes[0]);
                    txBuffer.Add(flatBytes[1]);
                }

                // Command byte
                txBuffer.Add(cmd);

                // Byte count of parameters
                if (null != param)
                    txBuffer.Add((byte)(param.Length));
                else
                    txBuffer.Add(0);

                if (null != param)
                {
                    // Parameter bytes
                    foreach (byte data in param)
                        txBuffer.Add(data);
                }

                // Checksum: (BusAddr + Cmd + Param Length + Data 1..Data n) MOD 0x100
                byte checksum = 0;
                foreach (byte by in txBuffer)
                    checksum += by;
                txBuffer.Add(checksum);

                // --- Send command ---
                string bufAsHexChars;
                while (true)
                {
                    while (DateTime.UtcNow.Subtract(timestampLastTx).TotalMilliseconds < 50)
                    {
                        // Force silence
                        Diagnostic.Msg(2, "SendCmdMultiResponse", "Forcing silence before sending cmd...");
                        Thread.Sleep(20);
                    }
                    commInterface.WriteBytes(txBuffer.ToArray(), 0, txBuffer.Count);

                    if (null != rawDataToTx && 0 != rawDataToTx.Length)
                    {
                        // Send raw data bytes to transmit immediately after sending the command frame
                        commInterface.WriteBytes(rawDataToTx, 0, rawDataToTx.Length);
                    }

                    // --- Ensure tx buffer is empty (all sent) ---
                    DateTime timeout = DateTime.UtcNow.AddMilliseconds(1000);
                    while (DateTime.UtcNow <= timeout && commInterface.BytesToWrite > 0)
                    {
                        Thread.Sleep(5);
                    }
                    timestampLastTx = DateTime.UtcNow;

                    // -------------------- DIAGNOSTICS --------------------
                    if (Diagnostic.OutputLevel > 2)
                    {
                        bufAsHexChars = "";
                        foreach (byte by in txBuffer)
                        {
                            bufAsHexChars += string.Format("{0:X2} ", by);
                        }

                        Diagnostic.Msg(3, "SendCmdMultiResponse",
                            "Cmd " + string.Format("{0:X2}", cmd) + " frame sent: " + bufAsHexChars);
                    }
                    // -------------------- DIAGNOSTICS --------------------

                    byte[] response;
                    int rxBusAddr;
                    byte[] foundFrame;
                    Queue<byte> rxQueue = new Queue<byte>();
                    // Timeout in x msecs
                    timeout = DateTime.UtcNow.AddMilliseconds(cmdTimeoutMs);
                    DateTime lastRxByte = DateTime.UtcNow;
                    bool checkOnceAfterByteWasReceivedAndSilencePassed = false;
                    while (DateTime.UtcNow <= timeout)
                    {
                        try
                        {
                            if (commInterface.BytesToRead > 0 ||
                                ((DateTime.UtcNow - lastRxByte).TotalMilliseconds > 250 && checkOnceAfterByteWasReceivedAndSilencePassed))
                            {
                                bool useSlidingWindow = false;

                                if (commInterface.BytesToRead > 0)
                                {
                                    byte rxByte = commInterface.ReadByte();
                                    rxQueue.Enqueue(rxByte);

                                    lastRxByte = DateTime.UtcNow;
                                    checkOnceAfterByteWasReceivedAndSilencePassed = true;

                                    // -------------------- DIAGNOSTICS --------------------
                                    if (Diagnostic.OutputLevel > 7)
                                    {
                                        bufAsHexChars = "";
                                        foreach (byte by in rxQueue)
                                        {
                                            bufAsHexChars += string.Format("{0:X2} ", by);
                                        }

                                        Diagnostic.Msg(8, "SendCmdMultiResponse",
                                            "RxByte: " + string.Format("{0:X2}", rxByte) +
                                            " => " + bufAsHexChars);
                                    }
                                    // -------------------- DIAGNOSTICS --------------------
                                }
                                else
                                {
                                    // This is the one pass after some period of silence after the
                                    // last received byte
                                    checkOnceAfterByteWasReceivedAndSilencePassed = false;

                                    Diagnostic.Msg(1, "SendCmdMultiResponse", "SendCmdMultiResponse silence passed, try to find packet within possible garbage...");

                                    // Try to find packet within rxQueue using sliding window
                                    useSlidingWindow = true;
                                }

                                EE31Result result = TryDecodeEE31Frame(rxQueue.ToArray(), new byte[] { cmd }, true, useSlidingWindow, out byte rxCmd, out response, out rxBusAddr, out foundFrame);
                                if (EE31Result.Invalid != result)
                                {
                                    // Valid response, add to result/responses/rxBusAddrs lists
                                    EE31CmdResponseData responseData = new EE31CmdResponseData() { Result = result, Response = response, RxBusAddr = rxBusAddr, FoundFrame = foundFrame };
                                    responses.Add(responseData);
                                    rxQueue.Clear();

                                    if (!multiResponsesExpected)
                                    {
                                        // finished!
                                        Diagnostic.Msg(2, "SendCmdMultiResponse", "TryDecodeEE31Frame was successful, finished!");
                                        return;
                                    }

                                    Diagnostic.Msg(2, "SendCmdMultiResponse", "TryDecodeEE31Frame was successful, waiting for more frames...");
                                }
                                // Receive next byte (if any)
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            Diagnostic.Msg(1, "SendCmdMultiResponse", "Exception: " + ex.Message);
                        }

                        Thread.Sleep(5);
                    }

                    // Timeout period has passed; dump bytes in rxBuffer so far...
                    bufAsHexChars = "";
                    foreach (byte by in rxQueue)
                        bufAsHexChars += string.Format("{0:X2} ", by);
                    Diagnostic.Msg(1, "SendCmdMultiResponse", "ReceiveCmd Timeout, rxBuffer contents: " + bufAsHexChars);
                    // ... and send command again if nothing received and any cmdTimeoutRetries left
                    if (responses.Count > 0 || cmdTimeoutRetries <= 0)
                        break;
                    cmdTimeoutRetries--;
                }

                if (0 == responses.Count)
                {
                    if (0 == cmdTimeoutMs)
                    {
                        // No responses expected, so it is ok to have none...
                        Diagnostic.Msg(2, "SendCmdMultiResponse", "No responses expected, leaving immediately...");

                        return;
                    }

                    // There should have been responses, throw Timeout exception...
                    Diagnostic.Msg(2, "SendCmdMultiResponse", string.Format("No responses for cmd 0x{0:X2} received, throwing TimeoutException!", cmd));

                    throw new TimeoutException(
                        string.Format("EE31 protocol timeout: No response for cmd 0x{0:X2} received!", cmd));
                }

                Diagnostic.Msg(2, "SendCmdMultiResponse", string.Format("{1} responses for cmd 0x{0:X2} successfully received.", cmd, responses.Count));
            }
        }

        /// <summary>
        /// Tries to the decode an EE31 frame.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="validCmds">The valid/expected commands.</param>
        /// <param name="checkAckByte">Check for ACK byte?</param>
        /// <param name="useSlidingWindow">
        /// Use sliding window (try to find packets anywhere within received bytes)?
        /// </param>
        /// <param name="rxCmd">The rx command.</param>
        /// <param name="response">@ACK: Payload data (without ACK), @NAK: Error code (1 Byte)</param>
        /// <param name="rxBusAddr">The rx bus address.</param>
        /// <param name="foundFrame">The found frame.</param>
        /// <returns></returns>
        private EE31Result TryDecodeEE31Frame(byte[] buffer, IList<byte> validCmds, bool checkAckByte, bool useSlidingWindow,
                                              out byte rxCmd, out byte[] response, out int rxBusAddr, out byte[] foundFrame)
        {
            rxCmd = 0;
            response = null;
            rxBusAddr = 0;
            foundFrame = null;

            int startIdx = 0;

            while (true)
            {
                if ((IncludeNetworkAddress && (buffer.Length - startIdx) < 6) ||
                    (!IncludeNetworkAddress && (buffer.Length - startIdx) < 4))
                {
                    return EE31Result.Invalid;
                }

                int frameIdx = startIdx;
                if (IncludeNetworkAddress)
                {
                    // 16 bit network address frame[0] and frame[1]
                    rxBusAddr = DataTypeConverter.ByteConverter.ToUInt16(buffer, frameIdx);
                    frameIdx += 2;
                }

                // Command byte (same as above)
                //if (cmd != buffer[frameIdx])
                if (null != validCmds && !validCmds.Contains(buffer[frameIdx]))
                {
                    // wrong cmd byte
                    if (!useSlidingWindow)
                        return EE31Result.Invalid;
                    startIdx++;
                    continue;
                }
                rxCmd = buffer[frameIdx];
                frameIdx++;

                // Byte count of parameters
                byte dataLength = buffer[frameIdx];
                if (0 == dataLength || dataLength > (byte)MaxPayload)
                {
                    // wrong data length (1 byte is minimum: ACK/NAK)
                    if (!useSlidingWindow)
                        return EE31Result.Invalid;
                    startIdx++;
                    continue;
                }
                frameIdx++;

                if ((frameIdx + dataLength + 1) > buffer.Length)
                {
                    // too few bytes received so far, wait for more
                    if (!useSlidingWindow)
                        return EE31Result.Invalid;
                    startIdx++;
                    continue;
                }

                byte ackByte = 0;
                if (checkAckByte)
                {
                    // <ACK> or <NAK>
                    ackByte = buffer[frameIdx];
                    if ((byte)EE31Result.ACK != ackByte && (byte)EE31Result.NAK != ackByte)
                    {
                        // wrong ack byte
                        if (!useSlidingWindow)
                            return EE31Result.Invalid;
                        startIdx++;
                        continue;
                    }
                }
                frameIdx++;

                // Checksum
                // Checksum: (BusAddr + Cmd + Param Length + Data 1..Data n) MOD 0x100
                byte checksum = 0;
                for (int i = startIdx; i < (frameIdx + dataLength - 1); i++)
                {
                    checksum += buffer[i];
                }
                if (checksum != buffer[frameIdx + dataLength - 1])
                {
                    // wrong checksum byte
                    if (!useSlidingWindow)
                        return EE31Result.Invalid;
                    startIdx++;
                    continue;
                }

                if (!checkAckByte)
                {
                    // i.e. command frame (slave mode)
                    response = new byte[dataLength];
                    Array.Copy(buffer, frameIdx - 1, response, 0, response.Count());
                    foundFrame = new byte[frameIdx + dataLength - startIdx];
                    Array.Copy(buffer, startIdx, foundFrame, 0, foundFrame.Count());
                    return EE31Result.ACK;
                }

                // <ACK>: Payload data (without ACK)
                // <NAK>: Error code (1 Byte)
                if ((byte)EE31Result.ACK == ackByte)
                {
                    response = new byte[dataLength - 1];
                    Array.Copy(buffer, frameIdx, response, 0, response.Count());
                    foundFrame = new byte[frameIdx + dataLength - startIdx];
                    Array.Copy(buffer, startIdx, foundFrame, 0, foundFrame.Count());
                    return EE31Result.ACK;
                }
                response = new byte[] { buffer[frameIdx] };
                return EE31Result.NAK;
            }
        }
    }
}