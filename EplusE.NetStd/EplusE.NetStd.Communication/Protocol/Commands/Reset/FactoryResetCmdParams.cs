namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class FactoryResetCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Factory reset command
        /// </summary>
        /// <param name="rstGroup">Product specific group index or bits (0xffff = all groups)</param>
        /// <param name="rstElement">Product specific element index or bits (0xffff = all elements)</param>
        public FactoryResetCmdParams(ushort rstGroup, ushort rstElement) :
            base(0x0)
        {
            //... set bytes
        }
    }
}