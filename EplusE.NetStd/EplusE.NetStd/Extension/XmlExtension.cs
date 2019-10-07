using System;
using System.Linq;
using System.Xml;

namespace EplusE
{
    /// <summary>
    /// XML extension methods.
    /// </summary>
    public static class XmlExtension
    {
        #region XmlNode GetAttribute extensions

        /// <summary>
        /// Gets the attribute safely (returns null if not existing).
        /// The name is case insensitive.
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <param name="name">The name (case insensitive).</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static string GetAttributeSafe(this XmlNode xmlNode, string name, string defaultValue = null)
        {
            if (null == xmlNode)
                return null;

            if (string.IsNullOrWhiteSpace(name))
                return null;

            System.Xml.XmlAttribute xmlAttrib =
                xmlNode.Attributes.Cast<System.Xml.XmlAttribute>().Where(a => name.Equals(a.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (null == xmlAttrib || xmlAttrib.Value.Equals("[DEFAULTVALUE]", StringComparison.OrdinalIgnoreCase))
                return defaultValue;
            if (xmlAttrib.Value.Equals("[NULL]", StringComparison.OrdinalIgnoreCase))
                return null;
            return xmlAttrib.Value;
        }

        /// <summary>
        /// Gets the attribute safely (data type: bool).
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static bool GetAttributeSafeBool(this XmlNode xmlNode, string name, bool defaultValue = false)
        {
            string strDefaultValue = "0";
            if (defaultValue)
                strDefaultValue = "1";

            string value = GetAttributeSafe(xmlNode, name, strDefaultValue);
            if (null == value)
                return defaultValue;
            return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Gets the attribute safely (data type: nullable bool).
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static bool? GetAttributeSafeBoolNullable(this XmlNode xmlNode, string name, bool? defaultValue = null)
        {
            string strDefaultValue = null;
            if (null != defaultValue)
            {
                if ((bool)defaultValue)
                    strDefaultValue = "1";
                else
                    strDefaultValue = "0";
            }

            string value = GetAttributeSafe(xmlNode, name, strDefaultValue);
            if (null == value)
                return null;
            return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, defaultValue ?? false);
        }

        #endregion XmlNode GetAttribute extensions

        #region XmlReader GetAttribute extensions

        /// <summary>
        /// Gets the attribute case insensitive.
        /// </summary>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetAttributeCaseInsensitive(this XmlReader xmlReader, string name)
        {
            // Based on:
            // https://social.msdn.microsoft.com/Forums/en-US/1662ba1d-da4d-448c-b088-955ee5e85a2b/xmltextreader-getattribute-case-insensitivity-question?forum=csharplanguage

            if (null == xmlReader)
                return null;

            if (string.IsNullOrWhiteSpace(name)) // || !xmlReader.HasAttributes)
                return null;
            int maxAttributesToCheck = 99; //xmlReader.AttributeCount;
            if (maxAttributesToCheck <= 0)
                return null;

            string result = null;

            if (!xmlReader.MoveToFirstAttribute())
                return null;

            int attributesChecked = 0;
            while (attributesChecked < maxAttributesToCheck)
            {
                attributesChecked++;

                if (string.IsNullOrWhiteSpace(xmlReader.Prefix) &&
                    xmlReader.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    result = xmlReader.Value;
                    break;
                }

                if (!xmlReader.MoveToNextAttribute())
                    break;
            }

            // Go back to the containing element.
            xmlReader.MoveToElement();

            return result;
        }

        /// <summary>
        /// Gets the attribute safely (returns null if not existing).
        /// The name is case insensitive.
        /// </summary>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="name">The name (case insensitive).</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static string GetAttributeSafe(this XmlReader xmlReader, string name, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string value = xmlReader.GetAttributeCaseInsensitive(name);

            if (null == value || value.Equals("[DEFAULTVALUE]", StringComparison.OrdinalIgnoreCase))
                return defaultValue;
            if (value.Equals("[NULL]", StringComparison.OrdinalIgnoreCase))
                return null;
            return value;
        }

        /// <summary>
        /// Gets the attribute safely (data type: bool).
        /// </summary>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static bool GetAttributeSafeBool(this XmlReader xmlReader, string name, bool defaultValue = false)
        {
            string strDefaultValue = "0";
            if (defaultValue)
                strDefaultValue = "1";

            string value = GetAttributeSafe(xmlReader, name, strDefaultValue);
            if (null == value)
                return defaultValue;
            return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Gets the attribute safely (data type: nullable bool).
        /// </summary>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value to return if attribute is not found.</param>
        /// <returns></returns>
        public static bool? GetAttributeSafeBoolNullable(this XmlReader xmlReader, string name, bool? defaultValue = null)
        {
            string strDefaultValue = null;
            if (null != defaultValue)
            {
                if ((bool)defaultValue)
                    strDefaultValue = "1";
                else
                    strDefaultValue = "0";
            }

            string value = GetAttributeSafe(xmlReader, name, strDefaultValue);
            if (null == value)
                return null;
            return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, defaultValue ?? false);
        }

        #endregion XmlReader GetAttribute extensions

        #region XmlReader extensions

        /// <summary>
        /// Skips the (rest of the) XML element.
        /// </summary>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="elementName">Name of the element (null means auto detect).</param>
        /// <param name="elementDepth">The element depth (-1 means auto detect).</param>
        public static void SkipElement(this XmlReader xmlReader, string elementName = null, int elementDepth = -1)
        {
            if (null == xmlReader)
                return;

            if (null == elementName)
                elementName = xmlReader.LocalName;
            if (-1 == elementDepth)
                elementDepth = xmlReader.Depth;

            while (!xmlReader.EOF)
            {
                xmlReader.Read();

                if (xmlReader.NodeType == XmlNodeType.EndElement
                    && xmlReader.Name.Equals(elementName, StringComparison.OrdinalIgnoreCase) &&
                    xmlReader.Depth <= elementDepth)
                {
                    break;
                }
            }
        }

        #endregion XmlReader extensions

        #region XPathNavigator extensions

        #region AdjustNaviIfNeeded

        /// <summary>
        /// Adjusts the navigator if needed.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="selectPath">The select path, i.e. "//CommProt/CommParameters".</param>
        /// <returns></returns>
        public static System.Xml.XPath.XPathNavigator AdjustNaviIfNeeded(this System.Xml.XPath.XPathNavigator stdnavi, string selectPath)
        {
            System.Xml.XPath.XPathNavigator navi = stdnavi;
            if (null != navi && null != selectPath && 0 != selectPath.Length)
            {
                System.Xml.XPath.XPathNodeIterator it = stdnavi.Select(selectPath);
                if (!it.MoveNext())
                    return stdnavi;
                navi = it.Current;
            }
            return navi;
        }

        #endregion AdjustNaviIfNeeded

        #region GetChildCount

        /// <summary>
        /// Gets the child count.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="childName">The child tag name, i.e. "Output".</param>
        /// <param name="selectPath">The select path, i.e. "//CommProt/PhysOutputs".</param>
        /// <returns></returns>
        public static int? GetChildCount(this System.Xml.XPath.XPathNavigator stdnavi, string childName, string selectPath)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    System.Xml.XPath.XPathNodeIterator it = navi.SelectChildren(childName, string.Empty);
                    return it.Count;
                }
                catch { }
            }
            return null;
        }

        #endregion GetChildCount

        #region GetAttributeAsString

        /// <summary>
        /// Gets the attribute as string.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path (or null if no path adjustment needed).</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetAttributeAsString(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, string defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    string tmp;
                    if (null != selectPath)
                    {
                        System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                        tmp = navi.GetAttribute(attributeName, string.Empty);
                    }
                    else
                    {
                        tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    }
                    if (string.IsNullOrEmpty(tmp))
                        return defaultValue;
                    return tmp;
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsString

        #region GetAttributeAsBool

        /// <summary>
        /// Gets the attribute as bool.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static bool GetAttributeAsBool(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, bool defaultValue = false)
        {
            if (null != stdnavi)
            {
                try
                {
                    string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    if (string.IsNullOrEmpty(tmp))
                        return defaultValue;
                    return DataTypeConverter.BoolConverter.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the attribute as bool.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static bool GetAttributeAsBool(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, bool defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    return GetAttributeAsBool(navi, attributeName, defaultValue);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsBool

        #region GetAttributeAsDouble

        /// <summary>
        /// Gets the attribute as double.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="expectEnglish">The expect english.</param>
        /// <returns></returns>
        public static double? GetAttributeAsDouble(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, bool expectEnglish = true)
        {
            if (null != stdnavi)
            {
                try
                {
                    string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    if (expectEnglish)
                    {
                        if (!string.IsNullOrEmpty(tmp))
                            return DataTypeConverter.DoubleConverter.ParseInvariantCulture(tmp);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(tmp))
                            return DataTypeConverter.DoubleConverter.Parse(tmp);
                    }
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// Gets the attribute as double.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static double GetAttributeAsDouble(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, double defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return DataTypeConverter.DoubleConverter.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsDouble

        #region GetAttributeAsUInt16

        /// <summary>
        /// Gets the attribute as unsigned int 16 (WORD).
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static UInt16 GetAttributeAsUInt16(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, UInt16 defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return DataTypeConverter.UInt16Converter.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsUInt16

        #region GetAttributeAsInt32

        /// <summary>
        /// Gets the attribute as int 32.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static Int32? GetAttributeAsInt32(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName)
        {
            if (null != stdnavi)
            {
                try
                {
                    string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return DataTypeConverter.Int32Converter.Parse(tmp);
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// Gets the attribute as int 32.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Int32 GetAttributeAsInt32(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, Int32 defaultValue)
        {
            try
            {
                string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                if (!string.IsNullOrEmpty(tmp))
                    return DataTypeConverter.Int32Converter.Parse(tmp);
            }
            catch { }
            return defaultValue;
        }

        /// <summary>
        /// Gets the attribute as int 32.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Int32 GetAttributeAsInt32(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, Int32 defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return DataTypeConverter.Int32Converter.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsInt32

        #region GetAttributeAsUInt32

        /// <summary>
        /// Gets the attribute as unsigned int 32.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static UInt32 GetAttributeAsUInt32(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, UInt32 defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return DataTypeConverter.UInt32Converter.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsUInt32

        #region GetAttributeAsTimeSpan

        /// <summary>
        /// Gets the attribute as time span.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static TimeSpan GetAttributeAsTimeSpan(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName)
        {
            if (null != stdnavi)
            {
                try
                {
                    string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return TimeSpan.Parse(tmp);
                }
                catch { }
            }
            return TimeSpan.Zero;
        }

        /// <summary>
        /// Gets the attribute as time span.
        /// </summary>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TimeSpan GetAttributeAsTimeSpan(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, TimeSpan defaultValue)
        {
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                        return TimeSpan.Parse(tmp);
                }
                catch { }
            }
            return defaultValue;
        }

        #endregion GetAttributeAsTimeSpan

        #region GetAttributeAsEnum

        /// <summary>
        /// Tries to parse an enum value from an attribute.
        /// Does NOT throw any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="result">Successfully parsed enum value (return true) or enum default value (return false).</param>
        /// <returns>True if attribute was set (not empty) and was successfully parsed.</returns>
        public static bool GetAttributeAsEnum<T>(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, out T result)
        {
            result = default(T);
            if (null != stdnavi)
            {
                try
                {
                    string tmp = stdnavi.GetAttribute(attributeName, string.Empty);
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        //string strAdditionalData = stringHelper.GetSubstringInsideOf("[", "]", tmp);
                        string enumPart = tmp.Truncate("[", false).Trim();
                        result = (T)Enum.Parse(typeof(T), enumPart);
                        return true;
                    }
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Tries to parse an enum value from an attribute.
        /// Does NOT throw any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="selectPath">The select path.</param>
        /// <param name="result">Successfully parsed enum value (return true) or enum default value (return false).</param>
        /// <returns>True if attribute was set (not empty) and was successfully parsed.</returns>
        public static bool GetAttributeAsEnum<T>(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, string selectPath, out T result)
        {
            result = default(T);
            if (null != stdnavi)
            {
                try
                {
                    System.Xml.XPath.XPathNavigator navi = AdjustNaviIfNeeded(stdnavi, selectPath);
                    string tmp = navi.GetAttribute(attributeName, string.Empty).TrimStart();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        //string strAdditionalData = stringHelper.GetSubstringInsideOf("[", "]", tmp);
                        string enumPart = tmp.Truncate("[", false).Trim();
                        result = (T)Enum.Parse(typeof(T), enumPart);
                        return true;
                    }
                }
                catch { }
            }
            return false;
        }

        #endregion GetAttributeAsEnum

        #region GetAttributeAsEnumList

        /// <summary>
        /// Tries to parse an enum value from an attribute.
        /// Does NOT throw any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="result">Successfully parsed enum value (return true) or enum default value (return false).</param>
        /// <returns>True if attribute was set (not empty) and was successfully parsed.</returns>
        public static bool GetAttributeAsEnumList<T>(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName, out System.Collections.Generic.List<T> result)
        {
            System.Collections.Generic.List<string> additionalData;
            return GetAttributeAsEnumList<T>(stdnavi, attributeName, out result, out additionalData);
        }

        /// <summary>
        /// Tries to parse an enum value from an attribute.
        /// Does NOT throw any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="stdnavi">The navigator.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="result">Successfully parsed enum value (return true) or enum default value (return false).</param>
        /// <param name="additionalData">Additional data for each parsed enum value (optional).</param>
        /// <returns>True if attribute was set (not empty) and was successfully parsed.</returns>
        public static bool GetAttributeAsEnumList<T>(this System.Xml.XPath.XPathNavigator stdnavi, string attributeName,
            out System.Collections.Generic.List<T> result, out System.Collections.Generic.List<string> additionalData)
        {
            result = new System.Collections.Generic.List<T>();
            additionalData = new System.Collections.Generic.List<string>();

            string list = null;
            if (null != stdnavi)
                list = stdnavi.GetAttribute(attributeName, string.Empty);
            if (string.IsNullOrEmpty(list))
                return false;

            list = list.Replace(';', ',');
            list = list.Replace('|', ',');
            foreach (string item in list.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                item.Trim();
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        string strAdditionalData = StringHelper.GetSubstringInsideOf("[", "]", item);
                        string enumPart = item.Truncate("[", false).Trim();
                        result.Add((T)Enum.Parse(typeof(T), enumPart));
                        additionalData.Add(strAdditionalData);
                    }
                    catch { }
                }
            }
            return true;
        }

        #endregion GetAttributeAsEnumList

        #endregion XPathNavigator extensions
    }
}