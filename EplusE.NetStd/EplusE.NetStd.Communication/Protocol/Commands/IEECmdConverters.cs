using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal interface IEECmdConverters
    {
        /// <summary>
        /// Use MVCode enum values instead of EE31 index numbers to identify measurands (with
        /// unit)? Only newer devices like EE210 support this.
        /// </summary>
        bool UseMVCodeInsteadOfEE31Index { get; }

        /// <summary>
        /// Converts the MVCode to EE31 standardized measval index (Cmds 0x33/0x34).
        /// </summary>
        /// <param name="mvCode">The mv code.</param>
        /// <returns>EE31 standardized measval index</returns>
        byte MVCodeToEE31MVIndex(MVCode mvCode);

        /// <summary>
        /// Converts the EE31 standardized measval index (Cmds 0x33/0x34) to MVCode.
        /// </summary>
        /// <param name="ee31MVIdx"></param>
        /// <returns>MVCode</returns>
        MVCode MVIndexToMVCode(int ee31MVIdx);
    }
}