namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public enum EECmdResultCode
    {
        Failed,
        Success,
        NeedUnlock,
        UnknownCmd,
        InvalidResult,
        InvalidParamClass,
        InvalidParameter,
        Busy
    }
}