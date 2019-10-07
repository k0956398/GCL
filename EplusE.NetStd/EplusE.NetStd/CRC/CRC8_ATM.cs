using System;

namespace EplusE.CRC
{
    /// <summary>
    /// CRC8 (ATM, HEC, ITU-T) using polynomial x^8 + x^2 + x + 1.
    /// </summary>
    public class CRC8_ATM
    {
        private static readonly UInt32 CRC8_POLYNOMIAL = (0x1070U << 3);

        /// <summary>
        /// Gets the CRC8 (ATM, HEC, ITU-T) using polynomial x^8 + x^2 + x + 1.
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

            byte curCRC = 0;
            int idx = 0;
            while (idx < count)
            {
                curCRC = StepCRC8(curCRC, value[idx]);
                idx++;
            }
            return curCRC;
        }

        private static byte StepCRC8(byte curCRC, byte data)
        {
            int i;
            ushort wordData;

            wordData = (ushort)(curCRC ^ data);
            wordData <<= 8;

            for (i = 0; i < 8; i++)
            {
                if (0 != (wordData & 0x8000))
                {
                    wordData = (ushort)(wordData ^ CRC8_POLYNOMIAL);
                }
                wordData = (ushort)(wordData << 1);
            }
            return (byte)(wordData >> 8);
        }
    }
}