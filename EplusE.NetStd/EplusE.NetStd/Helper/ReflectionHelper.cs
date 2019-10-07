using System;
using System.Collections.Generic;

namespace EplusE
{
    /// <summary>
    /// ReflectionHelper class.
    /// <locDE><para />ReflectionHelper Klasse.</locDE>
    /// </summary>
    public static class ReflectionHelper
    {
        #region _DefaultBindingFlags
        // Our default adds "IgnoreCase" to the system default binding flags
        private static readonly System.Reflection.BindingFlags _DefaultBindingFlags =
            System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
        #endregion

        #region Workaround methods for breaking changes: .NET 4.5 vs. .NET for Windows Store Apps
        // Type has lost some methods/properties, which are now separated into TypeInfo:
        // https://blogs.msdn.microsoft.com/dotnet/2012/08/28/evolving-the-reflection-api/

        /// <summary>
        /// Determines whether <paramref name="target"/> is generic type. 
        /// Use this workaround method in code which should compile against pre AND post .NET 4.5 versions.
        /// <locDE><para />Ermittelt, ob <paramref name="target"/> ein generischer Typ ist. 
        /// Diesen Workaround in Code verwenden, der sowohl gegen .NET Versionen vor UND nach 4.5 kompilieren soll.</locDE>
        /// </summary>
        /// <param name="target">The target type.<locDE><para />Der Zieltyp.</locDE></param>
        /// <returns>True, if the target type is generic.<locDE><para />True, falls der Zieltyp generisch ist.</locDE></returns>
        public static bool IsGenericType__Workaround(this Type target)
        {
            return target.IsGenericType;
            //return target.GetTypeInfo().IsGenericType;
        }

        /// <summary>
        /// Determines whether <paramref name="target"/> is enumeration type. 
        /// Use this workaround method in code which should compile against pre AND post .NET 4.5 versions.
        /// <locDE><para />Ermittelt, ob <paramref name="target"/> ein Enumerationstyp ist. 
        /// Diesen Workaround in Code verwenden, der sowohl gegen .NET Versionen vor UND nach 4.5 kompilieren soll.</locDE>
        /// </summary>
        /// <param name="target">The target type.<locDE><para />Der Zieltyp.</locDE></param>
        /// <returns>True, if the target type is enumeration.<locDE><para />True, falls der Zieltyp eine Enumeration ist.</locDE></returns>
        public static bool IsEnum__Workaround(this Type target)
        {
            return target.IsEnum;
            //return target.GetTypeInfo().IsEnum;
        }
        #endregion

        #region GetGenericMethod
        /// <summary>
        /// The generic methods cache.
        /// <locDE><para />Der Cache für generische Methoden.</locDE>
        /// </summary>
        private static readonly IDictionary<string, System.Reflection.MethodInfo> _GenericMethodsCache = new Dictionary<string, System.Reflection.MethodInfo>();

        /// <summary>
        /// Gets the generic method for a given type (caches internally for speedup).
        /// <locDE><para />Holt die generische Methode für den angegebenen Typ (cacht intern zur Geschwindigkeitssteigerung).</locDE>
        /// </summary>
        /// <param name="classContainingMethod">The class containing the method.<locDE><para />Die Klasse, welche die Methode enthält.</locDE></param>
        /// <param name="methodName">The name of the method.<locDE><para />Der Methodenname.</locDE></param>
        /// <param name="genericType">The type to find a generic method for.<locDE><para />Der Typ, für welchen eine generische Methode gefunden werden soll.</locDE></param>
        /// <param name="bindingFlags">The binding flags.<locDE><para />Die Bindungsflags.</locDE></param>
        /// <returns>MethodInfo object (if found).<locDE><para />MethodInfo-Objekt (falls gefunden).</locDE></returns>
        public static System.Reflection.MethodInfo GetGenericMethod(Type classContainingMethod, string methodName,
            System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public,
            Type genericType = null)
        {
            // NOTE: See also this discussion about an maybe easier way:
            // How do I use reflection to call a generic method?
            // http://stackoverflow.com/a/232621


            if (string.IsNullOrWhiteSpace(methodName))
                return null;

            string key = classContainingMethod.ToStringInvariant() + "#" + methodName + "#" + genericType.ToStringInvariant() + "#" + bindingFlags.ToStringInvariant();
            if (_GenericMethodsCache.ContainsKey(key))
            {
                // Cache hit
                return _GenericMethodsCache[key];
            }

            // Using System.Type to call a generic method
            // http://stackoverflow.com/a/14222568
            //System.Reflection.MethodInfo method = this.GetType().GetMethods()
            //    .First(m => m.Name == "QuerySingle" && m.GetParameters().Any(par => par.ParameterType == typeof(IDictionary<string, object>)));
            System.Reflection.MethodInfo method = null;
            method = classContainingMethod.GetMethod(methodName, bindingFlags);

            if (null == method)
                return null;

            if (null != genericType)
            {
                System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(genericType);
                if (null == genericMethod)
                    return null;
                method = genericMethod;
            }

            // Cache found generic method
            _GenericMethodsCache[key] = method;
            return method;
        }

        /// <summary>
        /// Gets the generic method for a given type (caches internally for speedup).
        /// </summary>
        /// <param name="classContainingMethod">The class containing the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericType">Type to find a generic method for.</param>
        /// <param name="paramTypes">The parameter types to resolve possible ambiguity (if necessary, may be null).</param>
        /// <returns></returns>
        public static System.Reflection.MethodInfo GetGenericMethod(Type classContainingMethod, string methodName, Type genericType, Type[] paramTypes)
        {
            // NOTE: See also this discussion about an maybe easier way:
            // How do I use reflection to call a generic method?
            // http://stackoverflow.com/a/232621


            if (string.IsNullOrWhiteSpace(methodName) || null == genericType)
                return null;

            string key = classContainingMethod.ToString() + "#" + methodName + "#" + genericType.ToString();
            if (null != paramTypes)
            {
                foreach (Type t in paramTypes)
                    key += "#" + t.ToString();
            }
            if (_GenericMethodsCache.ContainsKey(key))
            {
                // Cache hit
                return _GenericMethodsCache[key];
            }

            // Using System.Type to call a generic method
            // http://stackoverflow.com/a/14222568
            //System.Reflection.MethodInfo method = this.GetType().GetMethods()
            //    .First(m => m.Name == "QuerySingle" && m.GetParameters().Any(par => par.ParameterType == typeof(IDictionary<string, object>)));
            System.Reflection.MethodInfo method = null;
            if (null != paramTypes)
                method = classContainingMethod.GetMethod(methodName, paramTypes);
            else
                method = classContainingMethod.GetMethod(methodName);

            if (null == method)
                return null;

            System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(genericType);
            if (null == genericMethod)
                return null;

            // Cache found generic method
            _GenericMethodsCache[key] = genericMethod;
            return genericMethod;
        }
        #endregion

        #region GetStaticField
        /// <summary>
        /// The static fields cache.
        /// <locDE><para />Der Cache für statische Felder.</locDE>
        /// </summary>
        private static readonly IDictionary<string, System.Reflection.FieldInfo> _StaticFieldsCache = new Dictionary<string, System.Reflection.FieldInfo>();

        /// <summary>
        /// Gets a static field for a given type (caches internally for speedup).
        /// <locDE><para />Holt ein statisches Feld für den angegebenen Typ (cacht intern zur Geschwindigkeitssteigerung).</locDE>
        /// </summary>
        /// <param name="classContainingField">The class containing the field.<locDE><para />Die Klasse, welche das Feld enthält.</locDE></param>
        /// <param name="fieldName">The name of the static field.<locDE><para />Der Name des statischen Felds.</locDE></param>
        /// <returns>FieldInfo object (if found).<locDE><para />FieldInfo-Objekt (falls gefunden).</locDE></returns>
        public static System.Reflection.FieldInfo GetStaticField(Type classContainingField, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                return null;

            string key = classContainingField.ToString() + "#" + fieldName;
            if (_StaticFieldsCache.ContainsKey(key))
            {
                // Cache hit
                return _StaticFieldsCache[key];
            }

            System.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public;
            System.Reflection.FieldInfo field = null;
            field = classContainingField.GetField(fieldName, bindingFlags);

            if (null == field)
                return null;

            // Cache found static field
            _StaticFieldsCache[key] = field;
            return field;
        }
        #endregion

        #region GetStaticFieldValue
        /// <summary>
        /// Gets the static field value.
        /// <locDE><para />Holt den Wert eines statischen Felds.</locDE>
        /// </summary>
        /// <param name="classContainingField">The class containing field.<locDE><para />Die Klasse, die das statische Feld enthält.</locDE></param>
        /// <param name="fieldName">The name of the static field.<locDE><para />Der Name des statischen Felds.</locDE></param>
        /// <returns>Value of the static field.<locDE><para />Wert des statischen Feldes.</locDE></returns>
        public static object GetStaticFieldValue(Type classContainingField, string fieldName)
        {
            System.Reflection.FieldInfo fi = GetStaticField(classContainingField, fieldName);
            if (null != fi)
                return fi.GetValue(null);
            return null;
        }
        #endregion

        #region GetPropertyValue
        /// <summary>
        /// Gets the property value, returns null on any errors (undefined, exception, etc).
        /// <locDE><para />Holt den Eigenschaftswert, liefert Null im Fehlerfall (undefiniert, Exception, etc).</locDE>
        /// </summary>
        /// <param name="obj">The object to get the property value from.<locDE><para />Das Objekt, von dem der Eigenschaftswert geholt werden soll.</locDE></param>
        /// <param name="names">The name(s), i.e. property1.property2[X].property3.<locDE><para />Der/die Name(n), z.B. property1.property2[X].property3.</locDE></param>
        /// <param name="bindingFlags">The binding flags (optional).<locDE><para />Die Bindungsflags (optional).</locDE></param>
        /// <returns>Property value or null on any errors.<locDE><para />Eigenschaftswert oder Null im Fehlerfall.</locDE></returns>
        public static object GetPropertyValue(this object obj, string names, System.Reflection.BindingFlags? bindingFlags = null)
        {
            // Based on:
            // http://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection-in-c-sharp

            try
            {
                if (obj == null || string.IsNullOrWhiteSpace(names))
                    return null;

                // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
                string[] propertyNameParts = names.Split('.');

                foreach (string propertyNamePart in propertyNameParts)
                {
                    if (obj == null)
                        return null;

                    // propertyNamePart could contain reference to specific 
                    // element (by index) inside a collection
                    if (!propertyNamePart.Contains("["))
                    {
                        System.Reflection.PropertyInfo pi = null;
                        if (null != bindingFlags)
                            pi = obj.GetType().GetProperty(propertyNamePart, (System.Reflection.BindingFlags)bindingFlags);
                        else
                            pi = obj.GetType().GetProperty(propertyNamePart, _DefaultBindingFlags);
                        if (pi == null)
                            return null;

                        obj = pi.GetValue(obj, null);
                    }
                    else
                    {   // propertyNamePart is areference to specific element 
                        // (by index) inside a collection
                        // like AggregatedCollection[123]
                        //   get collection name and element index
                        int indexStart = propertyNamePart.IndexOf("[") + 1;
                        string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                        int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                        //   get collection object
                        System.Reflection.PropertyInfo pi = null;
                        if (null != bindingFlags)
                            pi = obj.GetType().GetProperty(collectionPropertyName, (System.Reflection.BindingFlags)bindingFlags);
                        else
                            pi = obj.GetType().GetProperty(collectionPropertyName, _DefaultBindingFlags);
                        if (pi == null)
                            return null;

                        object unknownCollection = pi.GetValue(obj, null);
                        // try to process the collection as array
                        if (unknownCollection.GetType().IsArray)
                        {
                            object[] collectionAsArray = unknownCollection as Array[];
                            obj = collectionAsArray[collectionElementIndex];
                        }
                        else
                        {
                            // try to process the collection as IList
                            System.Collections.IList collectionAsList = unknownCollection as System.Collections.IList;
                            if (collectionAsList != null)
                            {
                                obj = collectionAsList[collectionElementIndex];
                            }
                            else
                            {
                                // ??? Unsupported collection type
                            }
                        }
                    }
                }
                return obj;
            }
            catch { }
            return null;
        }
        #endregion

        #region GetPropertyValues
        /// <summary>
        /// Gets name and value of all properties, returns null on any errors (undefined, exception, etc).
        /// <locDE><para />Holt Name und Wert von allen Eigenschaften, liefert Null im Fehlerfall (undefiniert, Exception, etc).</locDE>
        /// </summary>
        /// <param name="obj">The object to get property names/values from.<locDE><para />Das Objekt, von dem Eigenschaftsnamen/-werte geholt werden sollen.</locDE></param>
        /// <param name="bindingFlags">The binding flags (optional).<locDE><para />Die Bindungsflags (optional).</locDE></param>
        /// <returns>Property names/values or null on any errors.<locDE><para />Eigenschaftsnamen/-werte oder Null im Fehlerfall.</locDE></returns>
        public static IDictionary<string, object> GetPropertyValues(this object obj, System.Reflection.BindingFlags? bindingFlags = null)
        {
            try
            {
                IDictionary<string, object> propsAndValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                System.Reflection.PropertyInfo[] pis = null;
                if (null != bindingFlags)
                    pis = obj.GetType().GetProperties((System.Reflection.BindingFlags)bindingFlags);
                else
                    pis = obj.GetType().GetProperties(_DefaultBindingFlags);
                if (pis == null)
                    return null;

                foreach (System.Reflection.PropertyInfo pi in pis)
                {
                    if (propsAndValues.ContainsKey(pi.Name))
                        throw new ArgumentException("Class members with same name but different casing: " + pi.Name);

                    propsAndValues[pi.Name] = pi.GetValue(obj, null);
                }
                return propsAndValues;
            }
            catch { }
            return null;
        }
        #endregion

        #region SetPropertyValues
        /// <summary>
        /// Sets values of multiple properties, catches exceptions.
        /// <locDE><para />Setzt Werte mehrerer Properties, fängt Exceptions.</locDE>
        /// </summary>
        /// <param name="obj">The object to set the property values.<locDE><para />Das Objekt, dessen Eigenschaftswerte gesetzt werden sollen.</locDE></param>
        /// <param name="propsAndValues">The property names and values.<locDE><para />Die Eigenschaftsnamen und -werte.</locDE></param>
        /// <param name="bindingFlags">The binding flags (optional).<locDE><para />Die Bindungsflags (optional).</locDE></param>
        public static void SetPropertyValues(this object obj, IDictionary<string, object> propsAndValues, System.Reflection.BindingFlags? bindingFlags = null)
        {
            try
            {
                System.Reflection.PropertyInfo[] pis = null;
                if (null != bindingFlags)
                    pis = obj.GetType().GetProperties((System.Reflection.BindingFlags)bindingFlags);
                else
                    pis = obj.GetType().GetProperties(_DefaultBindingFlags);
                if (pis == null)
                    return;

                foreach (System.Reflection.PropertyInfo pi in pis)
                {
                    if (!propsAndValues.ContainsKey(pi.Name))
                        continue;

                    try
                    {
                        if (pi.CanWrite)
                        {
                            //pi.SetValue(obj, Convert.ChangeType(propsAndValues[pi.Name], pi.PropertyType, CultureHelper.InvariantCulture), null);
                            pi.SetValue(obj, propsAndValues[pi.Name].ChangeType(pi.PropertyType, pi.Name), null);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
        #endregion

        #region GetPropertyTypes
        /// <summary>
        /// Gets name and type of all properties, returns null on any errors (undefined, exception, etc).
        /// <locDE><para />Holt Namen und Typen aller Eigenschaften, liefert Null im Fehlerfall (undefiniert, Exception, etc).</locDE>
        /// </summary>
        /// <param name="obj">The object to get the properties and types from.<locDE><para />Das Objekt, dessen Eigenschaften und Typen geholt werden sollen.</locDE></param>
        /// <param name="bindingFlags">The binding flags (optional).<locDE><para />Die Bindungsflags (optional).</locDE></param>
        /// <returns>Property names/types or null on any errors.<locDE><para />Eigenschaftsnamen/-typen oder Null im Fehlerfall.</locDE></returns>
        public static IDictionary<string, Type> GetPropertyTypes(this object obj, System.Reflection.BindingFlags? bindingFlags = null)
        {
            try
            {
                IDictionary<string, Type> propsAndValues = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

                System.Reflection.PropertyInfo[] pis = null;
                if (null != bindingFlags)
                    pis = obj.GetType().GetProperties((System.Reflection.BindingFlags)bindingFlags);
                else
                    pis = obj.GetType().GetProperties(_DefaultBindingFlags);
                if (pis == null)
                    return null;

                foreach (System.Reflection.PropertyInfo pi in pis)
                {
                    if (propsAndValues.ContainsKey(pi.Name))
                        throw new ArgumentException("Class members with same name but different casing: " + pi.Name);

                    propsAndValues[pi.Name] = pi.PropertyType;
                }
                return propsAndValues;
            }
            catch { }
            return null;
        }
        #endregion
    }
}
