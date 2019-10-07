namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class UploadBlockCmdParams : EECmdParamBase
    {
        public UploadBlockCmdParams(byte memID, uint startAddr, byte[] blockData) :
            base(0x0, 0, 0)
        {
            //... set bytes
        }

        internal byte[] BlockData { get; private set; }
    }
}