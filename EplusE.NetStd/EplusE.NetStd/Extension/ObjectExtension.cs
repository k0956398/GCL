using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace EplusE
{
    /// <summary>
    /// Object extension methods.
    /// <locDE><para />Object Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class ObjectExtension
    {
        #region ChangeType
        /// <summary>
        /// Changes the type safely (swallow exceptions).
        /// <locDE><para />Führt eine Typumwandlung "sicher" durch (schluckt etwaige Ausnahmen).</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="fieldName">Name of the field (i.e. check suffix "Utc" for DateTime).
        /// <locDE><para />Name des Feldes (z.B. zur Prüfung auf Suffix "Utc" bei Datumswerten).</locDE></param>
        /// <returns>Changed value.<locDE><para />Geänderter Wert.</locDE></returns>
        public static T ChangeTypeSafe<T>(this object value, string fieldName = null)
        {
            try
            {
                return ChangeType<T>(value, fieldName);
            }
            catch //(Exception ex)
            {
                //Log.AppLogger.Log(ex);
            }
            return default(T);
        }

        /// <summary>
        /// Changes the type safely (swallow exceptions).
        /// <locDE><para />Führt eine Typumwandlung "sicher" durch (schluckt etwaige Ausnahmen).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="targetType">The target type.<locDE><para />Der Zieldatentyp.</locDE></param>
        /// <param name="fieldName">Name of the field (i.e. check suffix "Utc" for DateTime).
        /// <locDE><para />Name des Feldes (z.B. zur Prüfung auf Suffix "Utc" bei Datumswerten).</locDE></param>
        /// <returns>Changed value.<locDE><para />Geänderter Wert.</locDE></returns>
        public static object ChangeTypeSafe(this object value, Type targetType, string fieldName = null)
        {
            try
            {
                return ChangeType(value, targetType, fieldName);
            }
            catch //(Exception ex)
            {
                //Log.AppLogger.Log(ex);
            }
            return null;
        }

        /// <summary>
        /// Changes the type, may throw exceptions.
        /// <locDE><para />Führt eine Typumwandlung durch, kann Ausnahmen werfen.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="fieldName">Name of the field (i.e. check suffix "Utc" for DateTime).
        /// <locDE><para />Name des Feldes (z.B. zur Prüfung auf Suffix "Utc" bei Datumswerten).</locDE></param>
        /// <returns>Changed value.<locDE><para />Geänderter Wert.</locDE></returns>
        public static T ChangeType<T>(this object value, string fieldName = null)
        {
            // See also: below + DALBaseConnection.ConvertJsonDataType()

            Type targetType = typeof(T);

            #region Null value
            if (null == value)
            {
                // Find a suitable match for a null value with non-nullable data types
                if (typeof(Guid) == targetType)
                    return default(T); // Guid.Empty;

                if (typeof(System.DateTime) == targetType)
                    return (T)(object)DateTimeHelper.InvalidDateTime;

                if (targetType.IsSubclassOf(typeof(System.Enum)))
                    return (T)Enum.ToObject(targetType, 0);

                return default(T);
            }
            #endregion

            Type sourceType = value.GetType();

            if (sourceType == targetType)
                return (T)value;

            #region Guid?
            if (typeof(Guid?) == targetType)
            {
                // Expect Guid
                if (value is string)
                {
                    string strValue = value.ToStringOrDefault();
                    if (string.IsNullOrWhiteSpace(strValue))
                        return default(T);

                    return (T)(object)Guid.Parse(strValue).ValueOrNullIfEmpty();
                }
                return (T)(object)((Guid)Convert.ChangeType(value, typeof(Guid), null)).ValueOrNullIfEmpty();
            }
            #endregion

            #region If target type is Nullable, get underlying type instead
            if (targetType.IsGenericType__Workaround() &&
                targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            #endregion

            #region Guid
            if (typeof(Guid) == targetType)
            {
                // Expect Guid
                if (value is string)
                {
                    string strValue = value.ToStringOrDefault();
                    if (string.IsNullOrWhiteSpace(strValue))
                        return default(T); // Guid.Empty;

                    return (T)(object)Guid.Parse(strValue);
                }
                return (T)Convert.ChangeType(value, typeof(Guid), null);
            }
            #endregion

            #region Enum
            if (sourceType.IsSubclassOf(typeof(System.Enum)))
            {
                // Cast enum to int
                value = (int)value;
            }

            if (targetType.IsSubclassOf(typeof(System.Enum)))
            {
                // Cast int to enum
                //return (T)Enum.ToObject(targetType, (value is int) ? (int)value : value.ToStringOrDefault().ToInt32(0));
                return (T)Enum.ToObject(targetType, DataTypeConverter.Int32Converter.ParseInvariantCulture(value, 0));
            }
            #endregion

            #region Bool
            if (typeof(bool) == targetType)
            {
                // 0/1 --> false/true
                return (T)(object)DataTypeConverter.BoolConverter.ParseInvariantCulture(value, false);
            }
            #endregion
            #region Byte
            if (typeof(byte) == targetType)
            {
                return (T)(object)DataTypeConverter.ByteConverter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Decimal
            if (typeof(decimal) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return (T)(object)DataTypeConverter.DecimalConverter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Double
            if (typeof(double) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return (T)(object)DataTypeConverter.DoubleConverter.ParseInvariantCulture(value, double.NaN);
            }
            #endregion
            #region Float
            if (typeof(float) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return (T)(object)DataTypeConverter.FloatConverter.ParseInvariantCulture(value, float.NaN);
            }
            #endregion
            #region Int32
            if (typeof(Int32) == targetType)
            {
                return (T)(object)DataTypeConverter.Int32Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Int64
            if (typeof(Int64) == targetType)
            {
                return (T)(object)DataTypeConverter.Int64Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt16
            if (typeof(UInt16) == targetType)
            {
                return (T)(object)DataTypeConverter.UInt16Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt32
            if (typeof(UInt32) == targetType)
            {
                return (T)(object)DataTypeConverter.UInt32Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt64
            if (typeof(UInt64) == targetType)
            {
                return (T)(object)DataTypeConverter.UInt64Converter.ParseInvariantCulture(value, 0);
            }
            #endregion

            #region DateTime
            if (typeof(System.DateTime) == targetType)
            {
                string strValue = value.ToStringOrDefault();
                if (string.IsNullOrWhiteSpace(strValue))
                    return (T)(object)DateTimeHelper.InvalidDateTime;

                if (strValue.Right(1).EqualsSafeIgnoreCase("Z"))
                {
                    // Handle Suffix "Z" to ensure UTC kind!
                    System.Globalization.DateTimeStyles dateTimeStyles =
                        //System.Globalization.DateTimeStyles.AssumeLocal |         // incompatible with RoundtripKind
                        System.Globalization.DateTimeStyles.RoundtripKind |         // Preserve time zone information if contained (i.e. "Z(ulu)")
                        System.Globalization.DateTimeStyles.NoCurrentDateDefault |
                        System.Globalization.DateTimeStyles.AllowWhiteSpaces;

                    DateTime dt = DateTime.Parse(strValue, CultureHelper.InvariantCulture, dateTimeStyles);

                    if (DateTimeKind.Utc != dt.Kind)
                        dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    return (T)(object)dt;
                }
                else
                {
                    DateTime dt;
                    // Maybe we can guess the DateTime kind from the field name?
                    if (null != fieldName && fieldName.EndsWith("utc", StringComparison.OrdinalIgnoreCase))
                    {
                        System.Globalization.DateTimeStyles dateTimeStyles =
                            //System.Globalization.DateTimeStyles.AssumeLocal |         // incompatible with RoundtripKind
                            System.Globalization.DateTimeStyles.RoundtripKind |         // Preserve time zone information if contained (i.e. "Z(ulu)")
                            System.Globalization.DateTimeStyles.NoCurrentDateDefault |
                            System.Globalization.DateTimeStyles.AllowWhiteSpaces;

                        dt = DateTime.Parse(strValue, CultureHelper.InvariantCulture, dateTimeStyles);

                        if (DateTimeKind.Utc != dt.Kind)
                            dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        return (T)(object)dt;
                    }

                    // No more ideas right now to determine if this is an UTC value...

                    try
                    {
                        // Try standard parsing with invariant culture
                        dt = (DateTime)Convert.ChangeType(value, targetType, CultureHelper.InvariantCulture);
                        return (T)(object)dt;
                    }
                    catch { }

                    // Last resort: Try to parse using locale settings and placeholder values
                    if (DateTimeHelper.TryParse(strValue, out dt, true))
                        return (T)(object)dt;

                    throw new FormatException("ObjectExtension.ChangeType(): Could not parse DateTime value!");
                }
            }
            #endregion

            return (T)Convert.ChangeType(value, targetType, CultureHelper.InvariantCulture);
        }

        /// <summary>
        /// Changes the type, may throw exceptions.
        /// <locDE><para />Führt eine Typumwandlung durch, kann Ausnahmen werfen.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="targetType">The target type.<locDE><para />Der Zieldatentyp.</locDE></param>
        /// <param name="fieldName">Name of the field (i.e. check suffix "Utc" for DateTime).
        /// <locDE><para />Name des Feldes (z.B. zur Prüfung auf Suffix "Utc" bei Datumswerten).</locDE></param>
        /// <returns>Changed value.<locDE><para />Geänderter Wert.</locDE></returns>
        public static object ChangeType(this object value, Type targetType, string fieldName = null)
        {
            // See also: above + DALBaseConnection.ConvertJsonDataType()

            #region Null value
            if (null == value)
            {
                // Find a suitable match for a null value with non-nullable data types
                if (typeof(Guid) == targetType)
                    return Guid.Empty;

                if (typeof(System.DateTime) == targetType)
                    return DateTimeHelper.InvalidDateTime;

                if (targetType.IsSubclassOf(typeof(System.Enum)))
                    return Enum.ToObject(targetType, 0);

                return null;
            }
            #endregion

            Type sourceType = value.GetType();

            if (sourceType == targetType)
                return value;

            #region Guid?
            if (typeof(Guid?) == targetType)
            {
                // Expect Guid
                if (value is string)
                {
                    string strValue = value.ToStringOrDefault();
                    if (string.IsNullOrWhiteSpace(strValue))
                        return null;

                    return Guid.Parse(strValue).ValueOrNullIfEmpty();
                }
                return ((Guid)Convert.ChangeType(value, typeof(Guid), null)).ValueOrNullIfEmpty();
            }
            #endregion

            #region If target type is Nullable, get underlying type instead
            if (targetType.IsGenericType__Workaround() &&
                targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }
            #endregion

            #region Guid
            if (typeof(Guid) == targetType)
            {
                // Expect Guid
                if (value is string)
                {
                    string strValue = value.ToStringOrDefault();
                    if (string.IsNullOrWhiteSpace(strValue))
                        return Guid.Empty;

                    return Guid.Parse(strValue);
                }
                return Convert.ChangeType(value, typeof(Guid), null);
            }
            #endregion

            #region Enum
            if (sourceType.IsSubclassOf(typeof(System.Enum)))
            {
                // Cast enum to int
                value = (int)value;
            }

            if (targetType.IsSubclassOf(typeof(System.Enum)))
            {
                // Cast int to enum
                //return Enum.ToObject(targetType, (value is int) ? (int)value : value.ToStringOrDefault().ToInt32(0));
                return Enum.ToObject(targetType, DataTypeConverter.Int32Converter.ParseInvariantCulture(value, 0));
            }
            #endregion

            #region Bool
            if (typeof(System.Boolean) == targetType)
            {
                // 0/1 --> false/true
                return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, false);
            }
            #endregion
            #region Byte
            if (typeof(byte) == targetType)
            {
                return DataTypeConverter.ByteConverter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Decimal
            if (typeof(decimal) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return DataTypeConverter.DecimalConverter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Double
            if (typeof(double) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return DataTypeConverter.DoubleConverter.ParseInvariantCulture(value, double.NaN);
            }
            #endregion
            #region Float
            if (typeof(float) == targetType)
            {
                // "12,34" or "12.34" --> 12.34
                return DataTypeConverter.FloatConverter.ParseInvariantCulture(value, float.NaN);
            }
            #endregion
            #region Int32
            if (typeof(Int32) == targetType)
            {
                return DataTypeConverter.Int32Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region Int64
            if (typeof(Int64) == targetType)
            {
                return DataTypeConverter.Int64Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt16
            if (typeof(UInt16) == targetType)
            {
                return DataTypeConverter.UInt16Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt32
            if (typeof(UInt32) == targetType)
            {
                return DataTypeConverter.UInt32Converter.ParseInvariantCulture(value, 0);
            }
            #endregion
            #region UInt64
            if (typeof(UInt64) == targetType)
            {
                return DataTypeConverter.UInt64Converter.ParseInvariantCulture(value, 0);
            }
            #endregion

            #region DateTime
            if (typeof(System.DateTime) == targetType)
            {
                string strValue = value.ToStringOrDefault();
                if (string.IsNullOrWhiteSpace(strValue))
                    return DateTimeHelper.InvalidDateTime;

                if (strValue.Right(1).EqualsSafeIgnoreCase("Z"))
                {
                    // Handle Suffix "Z" to ensure UTC kind!
                    System.Globalization.DateTimeStyles dateTimeStyles =
                        //System.Globalization.DateTimeStyles.AssumeLocal |         // incompatible with RoundtripKind
                        System.Globalization.DateTimeStyles.RoundtripKind |         // Preserve time zone information if contained (i.e. "Z(ulu)")
                        System.Globalization.DateTimeStyles.NoCurrentDateDefault |
                        System.Globalization.DateTimeStyles.AllowWhiteSpaces;

                    DateTime dt = DateTime.Parse(strValue, CultureHelper.InvariantCulture, dateTimeStyles);

                    if (DateTimeKind.Utc != dt.Kind)
                        dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    return dt;
                }
                else
                {
                    DateTime dt;
                    // Maybe we can guess the DateTime kind from the field name?
                    if (null != fieldName && fieldName.EndsWith("utc", StringComparison.OrdinalIgnoreCase))
                    {
                        System.Globalization.DateTimeStyles dateTimeStyles =
                            //System.Globalization.DateTimeStyles.AssumeLocal |         // incompatible with RoundtripKind
                            System.Globalization.DateTimeStyles.RoundtripKind |         // Preserve time zone information if contained (i.e. "Z(ulu)")
                            System.Globalization.DateTimeStyles.NoCurrentDateDefault |
                            System.Globalization.DateTimeStyles.AllowWhiteSpaces;

                        dt = DateTime.Parse(strValue, CultureHelper.InvariantCulture, dateTimeStyles);

                        if (DateTimeKind.Utc != dt.Kind)
                            dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        return dt;
                    }

                    // No more ideas right now to determine if this is an UTC value...

                    try
                    {
                        // Try standard parsing with invariant culture
                        dt = (DateTime)Convert.ChangeType(value, targetType, CultureHelper.InvariantCulture);
                        return dt;
                    }
                    catch { }

                    // Last resort: Try to parse using locale settings and placeholder values
                    if (DateTimeHelper.TryParse(strValue, out dt, true))
                        return dt;

                    throw new FormatException("ObjectExtension.ChangeType(): Could not parse DateTime value!");
                }
            }
            #endregion

            return Convert.ChangeType(value, targetType, CultureHelper.InvariantCulture);
        }
        #endregion

        #region BytesToHex, HexToBytes
        /// <summary>
        /// Convert byte array to groups of two hex digits without delimiter/space.
        /// <locDE><para />Konvertiert Byte Array zu Hex-Zweiergruppen ohne Trennzeichen/Leerzeichen.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Groups of two hex digits without delimiter/space.<locDE><para />Hex-Zweiergruppen ohne Trennzeichen/Leerzeichen.</locDE></returns>
        public static string BytesToHex(this byte[] value)
        {
            System.Text.StringBuilder hex = new System.Text.StringBuilder(value.Length * 2);
            foreach (byte by in value)
                hex.AppendFormat("{0:x2}", by);
            return hex.ToString();
        }

        /// <summary>
        /// Convert groups of two hex digits without delimiter/space to byte array.
        /// <locDE><para />Konvertiert Hex-Zweiergruppen ohne Trennzeichen/Leerzeichen zu Byte Array.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Byte array.<locDE><para />Byte Array.</locDE></returns>
        public static byte[] HexToBytes(this string value)
        {
            int numberOfChars = value.Length;
            byte[] bytes = new byte[numberOfChars / 2];
            for (int i = 0; i < numberOfChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }
        #endregion

        #region BinarySerialization        
        /// <summary>
        /// Serializes a arbitrary Object to a byte array.
        /// <locDE><para />Serialisiert ein beliebiges Object zu einem Byte Array.</locDE>
        /// </summary>
        /// <param name="input">The object to serialize.<locDE><para />Das zu serialisiernde Objekt.</locDE></param>
        /// <returns></returns>
        /// <exception cref="Exception">If member is not serializeable</exception>
        public static Byte[] ToBinary(this object input)
        {
            MemoryStream ms = null;
            Byte[] byteArray = null;
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                serializer.Serialize(ms, input);
                byteArray = ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return byteArray;
        }

        /// <summary>
        /// Deserializes a byte array to an Object.
        /// <locDE><para />Deserialisiert ein Byte Array zu einem Objekt.</locDE>
        /// </summary>
        /// <param name="buffer">The byte array to deserialize.<locDE><para />Das zu deserialisiernde Byte Array.</locDE></param>
        /// <returns>System.Object.</returns>
        /// <exception cref="Exception">If member is not serializeable</exception>
        public static object FromBinary(this byte[] buffer)
        {
            MemoryStream ms = null;
            object deserializedObject = null;

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                deserializedObject = serializer.Deserialize(ms);
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return deserializedObject;
        }
        #endregion
    }
}
