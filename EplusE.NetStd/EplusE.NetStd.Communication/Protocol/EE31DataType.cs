using System;

namespace EplusE.NetStd.Communication.Protocol
{
    internal static class EE31DataType
    {
        /// <summary>
        /// Gets byte size of data type defined in EE31 protocol reference.
        /// </summary>
        /// <param name="dataType">The EE31 Protocol data type</param>
        /// <param name="scale">Scale for scaled data types.</param>
        /// <returns>Data type size in bytes.</returns>
        public static int GetSize(byte dataType, out double scale)
        {
            scale = 1.0;

            switch (dataType)
            {
                case 1: // float (4 Bytes)
                case 2: // long (4 Bytes)
                case 3: // ulong (4 Bytes)
                    return 4;

                case 4: // int (short, 2 Bytes)
                case 5: // uint (WORD, 2 Bytes)
                    return 2;

                case 6: // char (1 Byte)
                case 7: // uchar (1 Byte)
                case 11: // Boolean (1 Byte)
                    return 1;

                case 8: // Date (4 Bytes)
                    return 4;

                case 9: // Date+Time (6 Bytes)
                    return 6;

                case 10: // double (8 Bytes)
                    return 8;
                // Scaled types
                case 20: // long (4 Bytes)
                case 30: // ulong (DWORD, 4 Bytes)
                    scale = 10.0;
                    return 4;

                case 21: // long (4 Bytes)
                case 31: // ulong (DWORD, 4 Bytes)
                    scale = 100.0;
                    return 4;

                case 22: // long (4 Bytes)
                case 32: // ulong (DWORD, 4 Bytes)
                    scale = 1000.0;
                    return 4;

                case 23: // long (4 Bytes)
                case 33: // ulong (DWORD, 4 Bytes)
                    scale = 10000.0;
                    return 4;

                case 25: // long (4 Bytes)
                case 35: // ulong (DWORD, 4 Bytes)
                    scale = 1 / 10;
                    return 4;

                case 26: // long (4 Bytes)
                case 36: // ulong (DWORD, 4 Bytes)
                    scale = 1 / 100;
                    return 4;

                case 27: // long (4 Bytes)
                case 37: // ulong (DWORD, 4 Bytes)
                    scale = 1 / 1000;
                    return 4;

                case 28: // long (4 Bytes)
                case 38: // ulong (DWORD, 4 Bytes)
                    scale = 1 / 10000;
                    return 4;

                case 40: // int (short, 2 Bytes)
                case 50: // uint (WORD, 2 Bytes)
                    scale = 10.0;
                    return 2;

                case 41: // int (short, 2 Bytes)
                case 51: // uint (WORD, 2 Bytes)
                    scale = 100.0;
                    return 2;

                case 42: // int (short, 2 Bytes)
                case 52: // uint (WORD, 2 Bytes)
                    scale = 1000.0;
                    return 2;

                case 43: // int (short, 2 Bytes)
                case 53: // uint (WORD, 2 Bytes)
                    scale = 10000.0;
                    return 2;

                case 45: // int (short, 2 Bytes)
                case 55: // uint (WORD, 2 Bytes)
                    scale = 1 / 10;
                    return 2;

                case 46: // int (short, 2 Bytes)
                case 56: // uint (WORD, 2 Bytes)
                    scale = 1 / 100;
                    return 2;

                case 47: // int (short, 2 Bytes)
                case 57: // uint (WORD, 2 Bytes)
                    scale = 1 / 1000;
                    return 2;

                case 48: // int (short, 2 Bytes)
                case 58: // uint (WORD, 2 Bytes)
                    scale = 1 / 10000;
                    return 2;
            }

            return 0;
        }

        /// <summary>
        /// Gets value of data type defined in EE31 protocol reference.
        /// </summary>
        /// <param name="dataType">The EE31 protocol data type</param>
        /// <param name="buffer">Buffer containing protocol data</param>
        /// <param name="startIdx">Start index of value data in buffer</param>
        /// <param name="value">The converted value</param>
        /// <returns>False if invalid data or type, true otherwise</returns>
        public static bool ToValue(byte dataType, byte[] buffer, int startIdx, out object value, out byte[] valueData)
        {
            value = null;
            valueData = null;

            if (buffer == null)
                return false;

            int size = GetSize(dataType, out double scale);
            if (startIdx + size > buffer.Length)
                return false;

            // Get raw value data
            valueData = new byte[size];
            Array.Copy(buffer, startIdx, valueData, 0, size);

            switch (dataType)
            {
                case 1: // float (4 Bytes)
                    value = BitConverter.ToSingle(buffer, startIdx);
                    return true;

                case 2: // long (4 Bytes)
                    value = BitConverter.ToInt32(buffer, startIdx);
                    return true;

                case 3: // ulong (4 Bytes)
                    value = BitConverter.ToUInt32(buffer, startIdx);
                    return true;

                case 4: // int (short, 2 Bytes)
                    value = BitConverter.ToInt16(buffer, startIdx);
                    return true;

                case 5: // uint (WORD, 2 Bytes)
                    value = BitConverter.ToUInt16(buffer, startIdx);
                    return true;

                case 6: // char (1 Byte)
                    value = Convert.ToSByte(buffer[startIdx]);
                    return true;

                case 7: // uchar (1 Byte)
                    value = buffer[startIdx];
                    return true;

                case 11: // Boolean (1 Byte)
                    value = Convert.ToBoolean(buffer[startIdx]);
                    return true;

                case 8: // Date (4 Bytes)
                    value = new DateTime(2000 + buffer[startIdx + 2], buffer[startIdx + 1], buffer[startIdx]);
                    return true;

                case 9: // Date+Time (6 Bytes)
                    value = new DateTime(2000 + buffer[startIdx + 2], buffer[startIdx + 1], buffer[startIdx], buffer[startIdx + 3], buffer[startIdx + 4], buffer[startIdx + 5]);
                    return true;

                case 10: // double (8 Bytes)
                    value = BitConverter.ToDouble(buffer, startIdx);
                    return true;

                // Scaled types
                case 20: // long (4 Bytes)
                case 21: // long (4 Bytes)
                case 22: // long (4 Bytes)
                case 23: // long (4 Bytes)
                case 25: // long (4 Bytes)
                case 26: // long (4 Bytes)
                case 27: // long (4 Bytes)
                case 28: // long (4 Bytes)
                    value = BitConverter.ToInt32(buffer, startIdx) * scale;
                    return true;

                case 30: // ulong (DWORD, 4 Bytes)
                case 31: // ulong (DWORD, 4 Bytes)
                case 32: // ulong (DWORD, 4 Bytes)
                case 33: // ulong (DWORD, 4 Bytes)
                case 35: // ulong (DWORD, 4 Bytes)
                case 36: // ulong (DWORD, 4 Bytes)
                case 37: // ulong (DWORD, 4 Bytes)
                case 38: // ulong (DWORD, 4 Bytes)
                    value = BitConverter.ToUInt32(buffer, startIdx) * scale;
                    return true;

                case 40: // int (short, 2 Bytes)
                case 41: // int (short, 2 Bytes)
                case 42: // int (short, 2 Bytes)
                case 43: // int (short, 2 Bytes)
                case 45: // int (short, 2 Bytes)
                case 46: // int (short, 2 Bytes)
                case 47: // int (short, 2 Bytes)
                case 48: // int (short, 2 Bytes)
                    value = BitConverter.ToInt16(buffer, startIdx) * scale;
                    return true;

                case 50: // uint (WORD, 2 Bytes)
                case 51: // uint (WORD, 2 Bytes)
                case 52: // uint (WORD, 2 Bytes)
                case 53: // uint (WORD, 2 Bytes)
                case 55: // uint (WORD, 2 Bytes)
                case 56: // uint (WORD, 2 Bytes)
                case 57: // uint (WORD, 2 Bytes)
                case 58: // uint (WORD, 2 Bytes)
                    value = BitConverter.ToUInt16(buffer, startIdx) * scale;
                    return true;
            }

            return false;
        }
    }
}