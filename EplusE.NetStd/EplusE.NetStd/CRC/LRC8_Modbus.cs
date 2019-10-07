namespace EplusE.CRC
{
    /// <summary>
    /// LRC - Longitudinal Redundancy Check (Modbus).
    /// </summary>
    public class LRC8_Modbus
    {
        /// <summary>
        /// Gets the LRC - Longitudinal Redundancy Check (Modbus).
        /// </summary>
        /// <param name="value">The value byte array.</param>
        /// <param name="count">The count (-1 means whole value array).</param>
        /// <returns></returns>
        public static byte GetCRC8(byte[] value, int count = -1)
        {
            if (null == value)
                return 0;

            if (-1 == count)
                count = value.Length;

            // Longitudinal Redundancy Check from Modbus-ida.org
            int curCRC = 0;

            for (int idx = 0; idx < count; idx++)
            {
                curCRC = (byte)(curCRC + value[idx]);
            }

            curCRC = (byte)(0xFF - curCRC);     // 1's complement
            curCRC++;						    // 2's complement

            return (byte)curCRC;
        }
    }
}