namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputConfigCmdParams_Inactive : SetOutputConfigCmdParamsBase
    {
        /// <summary>
        /// Inactive output parameter construction
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="isInput"></param>
        public SetOutputConfigCmdParams_Inactive(byte portId) :
            base(portId)
        {
            //... set bytes
        }
    }
}