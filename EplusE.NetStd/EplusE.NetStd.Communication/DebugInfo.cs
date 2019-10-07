namespace EplusE.NetStd.Communication
{
    public interface IDebugInfo
    {
        void GetTimings(out long writeTimeMs, out long responseTimeMs);
    }
}