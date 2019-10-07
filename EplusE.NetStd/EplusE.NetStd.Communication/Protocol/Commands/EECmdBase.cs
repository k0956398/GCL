namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command base class that implements IEECommCommand.
    /// </summary>
    /// <typeparam name="TParamClass">The command parameter class type</typeparam>
    /// <typeparam name="TResultClass">The command result class type</typeparam>
    internal class EECmdBase<TParamClass, TResultClass> : IEECommCommand
        where TResultClass : new()
    {
        internal EECmdBase()
        {
        }

        public IEECmdCallbacks ProtocolCallbacks { get; set; }
        public IEECmdConverters ProtocolConverters { get; set; }

        /// <summary>
        /// Provide default execution function. Might be overwritten in command classes.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual IEECommandResult Execute(IEECommandParameter parameter)
        {
            if (parameter is TParamClass)
            {
                EECmdResultCode result = ProtocolCallbacks.SendReceive(parameter, out byte[] response);
                return CreateResult(result, response, parameter);
            }
            else
                return CreateResult(EECmdResultCode.InvalidParamClass, null, null);
        }

        /// <summary>
        /// Provide default result function. Might be overwritten in command classes.
        /// </summary>
        /// <param name="cmdResultCode"></param>
        /// <param name="cmdResponse"></param>
        /// <returns></returns>
        protected virtual IEECommandResult CreateResult(EECmdResultCode cmdResultCode, byte[] cmdResponse, IEECommandParameter cmdParams)
        {
            EECmdResultBase result = new TResultClass() as EECmdResultBase;

            // Set properties that might be needed to interpret command result
            result.Code = cmdResultCode;
            result.Data = cmdResponse;

            // Commands have the chance to interpret the result data (in case of {ACK}/Success)
            if (cmdResultCode == EECmdResultCode.Success)
                result.InterpretResult(ProtocolCallbacks.LookupOrCreateCmdSpecialTreatment(cmdParams.Cmd).ReverseByteOrder.Value, ProtocolConverters, cmdParams);

            return result;
        }
    }
}