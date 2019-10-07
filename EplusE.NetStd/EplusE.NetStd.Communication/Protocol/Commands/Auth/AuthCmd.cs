namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class AuthCmd : EECmdBase<AuthCmdParams, AuthCmdResult>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            if (parameter is AuthCmdParams authParams)
            {
                // First request challenge
                authParams.PrepareStep0();
                IEECommandResult commandResult = base.Execute(authParams);
                if (commandResult.Code != EECmdResultCode.Success)
                    return commandResult;

                // Now authenticate with response
                authParams.PrepareStep1((commandResult as AuthCmdResult).Challenge);
                commandResult = base.Execute(authParams);

                // A password change is requested?
                if (authParams.ChangePassword)
                {
                    // Request new challenge
                    authParams.PrepareStep0();
                    if (commandResult.Code != EECmdResultCode.Success)
                        return commandResult;

                    // Generate response with new secret
                    authParams.PrepareStep1((commandResult as AuthCmdResult).Challenge, true);
                    commandResult = base.Execute(authParams);
                }

                return commandResult;
            }
            else
                return CreateResult(EECmdResultCode.InvalidParamClass, null, null);
        }
    }
}