namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class GetOutputConfigCmd : EECmdBase<GetOutputConfigCmdParams, EECmdResultBase>
    {
        // Override CreateResult because we might have different results based on output type
        protected override IEECommandResult CreateResult(EECmdResultCode cmdResultCode, byte[] cmdResponse, IEECommandParameter cmdParams)
        {
            if (cmdResultCode == EECmdResultCode.Success)
            {
                if (cmdResponse.Length < 2)
                    return new EECmdResultBase() { Code = EECmdResultCode.InvalidResult };

                EECmdResultBase result = null;

                // Fist byte is always 0. Second is output mode.
                // Based on output mode we create the correct result class.
                OutputMode outputMode = (OutputMode)cmdResponse[1];
                switch (outputMode)
                {
                    case OutputMode.Inactive:
                        result = new GetOutputConfigCmdResultBase(outputMode);
                        break;

                    case OutputMode.Current:
                    case OutputMode.Voltage:
                        result = new GetOutputConfigCmdResult_Analog(outputMode);
                        break;

                    case OutputMode.Relais_Hysteresis:
                    case OutputMode.Relais_Window:
                    case OutputMode.Relais_ErrorIndication:
                        result = new GetOutputConfigCmdResult_Relais(outputMode);
                        break;

                    case OutputMode.Pulse:
                        result = new GetOutputConfigCmdResult_Pulse(outputMode);
                        break;
                }

                if (result != null)
                {
                    result.Code = cmdResultCode;
                    result.Data = cmdResponse;
                    result.InterpretResult(ProtocolCallbacks.LookupOrCreateCmdSpecialTreatment(cmdParams.Cmd).ReverseByteOrder.Value, ProtocolConverters, cmdParams);
                }
                else
                    return new EECmdResultBase() { Code = EECmdResultCode.InvalidResult };

                return result;
            }
            else
            {
                return new EECmdResultBase() { Code = cmdResultCode };
            }
        }
    }
}