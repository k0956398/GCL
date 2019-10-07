using System;
using System.Collections.Generic;
using System.Linq;

namespace EplusE.DataTypeConverter
{
    /// <summary>
    /// Byte conversion helpers, i.e. hex prefix aware parse method (0x...).
    /// <locDE><para />Byte Konvertierungshilfsmethoden, z.B. Parse-Methode mit HEX-Präfix-Unterstützung (0x...).</locDE>
    /// </summary>
    public static class ByteConverter
    {
        #region AppendToList methods

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(byte value, IList<byte> buffer)
        {
            buffer.Add(value);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(Int16 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(UInt16 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(Int32 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(UInt32 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(Int64 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(UInt64 value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(float value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        public static void AppendToList(double value, IList<byte> buffer)
        {
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer.Add(item);
        }

        #endregion AppendToList methods

        #region CopyToArray methods

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(Int16 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(UInt16 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(Int32 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(UInt32 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(Int64 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(UInt64 value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(float value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        /// <summary>
        /// Copies value to byte array (ensures little endian byte order).
        /// <locDE><para />Kopiert den Wert in einen Byte Array (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        public static void CopyToArray(double value, byte[] buffer, int startIndex)
        {
            int idx = startIndex;
            byte[] valueAsBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                valueAsBytes = valueAsBytes.Reverse().ToArray();
            foreach (byte item in valueAsBytes)
                buffer[idx++] = item;
        }

        #endregion CopyToArray methods

        #region To... methods

        /// <summary>
        /// Converts to byte.
        /// <locDE><para />Konvertiert zu Byte.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ToByte(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            //if (reverseByteOrder)
            //    Array.Reverse(buffer, startIndex, 2);
            //return BitConverter.ToInt16(buffer, startIndex);
            return buffer[startIndex];
        }

        /// <summary>
        /// Converts to byte.
        /// <locDE><para />Konvertiert zu Byte.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ToByte(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            //return buffer.ToArray().ToByte(startIndex, reverseByteOrder);
            return buffer[startIndex];
        }

        /// <summary>
        /// Converts to Double.
        /// <locDE><para />Konvertiert zu Double.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ToDouble(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 8);
            return BitConverter.ToDouble(buffer, startIndex);
        }

        /// <summary>
        /// Converts to Double.
        /// <locDE><para />Konvertiert zu Double.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ToDouble(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToDouble(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Float.
        /// <locDE><para />Konvertiert zu Float.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Float value.<locDE><para />Float Wert.</locDE></returns>
        public static float ToFloat(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 4);
            return BitConverter.ToSingle(buffer, startIndex);
        }

        /// <summary>
        /// Converts to Float.
        /// <locDE><para />Konvertiert zu Float.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Float value.<locDE><para />Float Wert.</locDE></returns>
        public static float ToFloat(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToFloat(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int16.
        /// <locDE><para />Konvertiert zu Int16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int16 value.<locDE><para />Int16 Wert.</locDE></returns>
        public static Int16 ToInt16(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 2);
            return BitConverter.ToInt16(buffer, startIndex);
        }

        /// <summary>
        /// Converts to Int16.
        /// <locDE><para />Konvertiert zu Int16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int16 value.<locDE><para />Int16 Wert.</locDE></returns>
        public static Int16 ToInt16(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToInt16(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int32.
        /// <locDE><para />Konvertiert zu Int32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static Int32 ToInt32(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 4);
            return BitConverter.ToInt32(buffer, startIndex);
        }

        /// <summary>
        /// Converts to Int32.
        /// <locDE><para />Konvertiert zu Int32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static Int32 ToInt32(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToInt32(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int64.
        /// <locDE><para />Konvertiert zu Int64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static Int64 ToInt64(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 8);
            return BitConverter.ToInt64(buffer, startIndex);
        }

        /// <summary>
        /// Converts to Int64.
        /// <locDE><para />Konvertiert zu Int64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static Int64 ToInt64(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToInt64(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt16.
        /// <locDE><para />Konvertiert zu UInt16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt16 value.<locDE><para />UInt16 Wert.</locDE></returns>
        public static UInt16 ToUInt16(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 2);
            return BitConverter.ToUInt16(buffer, startIndex);
        }

        /// <summary>
        /// Converts to UInt16.
        /// <locDE><para />Konvertiert zu UInt16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt16 value.<locDE><para />UInt16 Wert.</locDE></returns>
        public static UInt16 ToUInt16(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToUInt16(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt32.
        /// <locDE><para />Konvertiert zu UInt32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ToUInt32(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 4);
            return BitConverter.ToUInt32(buffer, startIndex);
        }

        /// <summary>
        /// Converts to UInt32.
        /// <locDE><para />Konvertiert zu UInt32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ToUInt32(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToUInt32(startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt64.
        /// <locDE><para />Konvertiert zu UInt64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ToUInt64(byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            if (reverseByteOrder)
                Array.Reverse(buffer, startIndex, 8);
            return BitConverter.ToUInt64(buffer, startIndex);
        }

        /// <summary>
        /// Converts to UInt64.
        /// <locDE><para />Konvertiert zu UInt64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ToUInt64(IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return buffer.ToArray().ToUInt64(startIndex, reverseByteOrder);
        }

        #endregion To... methods

        #region GetBit

        /// <summary>
        /// Determines if a specific bit of this byte is set.
        /// <locDE><para />Ermittelt, ob ein bestimmtes Bit dieses Bytes gesetzt ist.</locDE>
        /// </summary>
        /// <param name="value">The byte value to check.<locDE><para />Der zu prüfende Byte-Wert.</locDE></param>
        /// <param name="bitNumber">The bit number (0..7).<locDE><para />Die Nummer des fraglichen Bits (0..7).</locDE></param>
        /// <returns>True if bit is set; otherwise false.<locDE><para />True, falls das fragliche Bit gesetzt ist; sonst false.</locDE></returns>
        public static bool GetBit(byte value, int bitNumber)
        {
            return (value & (1 << bitNumber)) != 0;
            //System.Collections.BitArray ba = new BitArray(new byte[] { b });
            //return ba.Get(bitNumber);
        }

        #endregion GetBit

        #region TryParse

        /// <summary>
        /// Tries to parse the specified value (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string value, out byte result)
        {
            return TryParse(value, out result, null);
        }

        /// <summary>
        /// Tries to parse the specified value (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <param name="ifp">The format provider (or null).<locDE><para />Der Format-Provider (oder null).</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string value, out byte result, IFormatProvider ifp)
        {
            result = 0;

            if (null == value)
                return false;

            if (null == ifp)
                ifp = System.Globalization.CultureInfo.CurrentCulture;

            string work = value.ToLowerInvariant();
            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                if (null == ifp)
                    ifp = System.Globalization.CultureInfo.CurrentCulture;
                return byte.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out result);
            }

            #region Cut off at decimal point

            // If decimal point is included, parse only left part of it (ignore fractional part)
            int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
            if (-1 != decimalPos)
                work = work.Left(decimalPos);

            #endregion Cut off at decimal point

            return byte.TryParse(work, System.Globalization.NumberStyles.Any, ifp, out result);
        }

        /// <summary>
        /// Tries to parse the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert mit fixierter englischer Kultureinstellung zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParseInvariantCulture(string value, out byte result)
        {
            return TryParse(value, out result, CultureHelper.InvariantCulture);
        }

        #endregion TryParse

        #region Parse

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte Parse(string value, byte? defaultValue = null)
        {
            return Parse(value, defaultValue, null);
        }

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <param name="ifp">The format provider (or null).<locDE><para />Der Format-Provider (oder null).</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte Parse(string value, byte? defaultValue, IFormatProvider ifp)
        {
            if (null == value && null != defaultValue)
                return (byte)defaultValue;

            byte byteValue;
            string work = value.ToLowerInvariant();
            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                if (null == ifp)
                {
                    if (byte.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier,
                        System.Globalization.CultureInfo.CurrentCulture, out byteValue))
                        return byteValue;
                }
                else
                {
                    if (byte.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out byteValue))
                        return byteValue;
                }

                if (null != defaultValue)
                    return (byte)defaultValue;
            }

            if (null == ifp)
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (byte.TryParse(work, out byteValue))
                    return byteValue;
            }
            else
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (byte.TryParse(work, System.Globalization.NumberStyles.Number, ifp, out byteValue))
                    return byteValue;
            }

            if (null != defaultValue)
                return (byte)defaultValue;
            throw new ArgumentException("ByteConverter.Parse: Invalid value \"" + value + "\"");
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ParseInvariantCulture(object value, byte? defaultValue = null)
        {
            if (value is byte || value is sbyte || value is Int16 || value is Int32 || value is Int64 ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is double || value is float || value is decimal)
                return (byte)value;

            string strDefault = defaultValue.ToStringOrDefault();
            return Parse(value.ToStringOrDefault(strDefault), defaultValue, CultureHelper.InvariantCulture);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ParseInvariantCulture(string value, byte? defaultValue = null)
        {
            return Parse(value, defaultValue, CultureHelper.InvariantCulture);
        }

        #endregion Parse
    }
}