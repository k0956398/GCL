using System;
using System.Collections.Generic;
using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Collection extension methods.
    /// <locDE><para />Collection Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class CollectionExtension
    {
        #region IgnoreNullValues

        /// <summary>
        /// Should null values be ignored (i.e. within AddIfNotContains, RemoveIfContains)?
        /// <locDE><para />Sollen null Werte ignoriert werden (z.B. bei AddIfNotContains, RemoveIfContains)?</locDE>
        /// </summary>
        public static bool IgnoreNullValues { get; set; } = true;

        #endregion IgnoreNullValues

        #region Count (with stop limit)

        /// <summary>
        /// Counts the items within the given list.
        /// <locDE><para />Zählt die Elemente in der angegebenen Liste.</locDE>
        /// </summary>
        /// <param name="source">The list to count the contained items.<locDE><para />Liste, deren Elemente gezählt werden soll.</locDE></param>
        /// <param name="stopCountAt">Stop counting at this value (i.e. just want to know if there are at least n items).
        /// <locDE><para />Zählung beim Erreichen dieses Wertes abbrechen (wenn man nur wissen will, ob mind. soviele Elemente enthalten sind).</locDE></param>
        /// <returns>Item count (maybe limited by stopCountAt value).<locDE><para />Anzahl der Elemente (ggf. limitiert durch stopCountAt Wert).</locDE></returns>
        public static int Count(this System.Collections.IEnumerable source, int stopCountAt)
        {
            if (null == source)
                return 0;

            System.Collections.ICollection stdCollection = source as System.Collections.ICollection;
            if (null != stdCollection)
            {
                return Math.Min(stdCollection.Count, stopCountAt);
            }

            int num = 0;
#pragma warning disable 0168, 0169, 0414, 0649    // Unused parameter, Unused field, Field assigned but never used, Variable is only assigned to
            foreach (var item in source)
            {
                num++;
                if (num > stopCountAt)
                    break;
            }
#pragma warning restore 0168, 0169, 0414, 0649    // Unused parameter, Unused field, Field assigned but never used, Variable is only assigned to
            return num;
        }

        /// <summary>
        /// Counts the items within the given list.
        /// <locDE><para />Zählt die Elemente in der angegebenen Liste.</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the given list.<locDE><para />Elementtyp der angegebenen Liste.</locDE></typeparam>
        /// <param name="source">The list to count the contained items.<locDE><para />Liste, deren Elemente gezählt werden soll.</locDE></param>
        /// <param name="stopCountAt">Stop counting at this value (i.e. just want to know if there are at least n items).
        /// <locDE><para />Zählung beim Erreichen dieses Wertes abbrechen (wenn man nur wissen will, ob mind. soviele Elemente enthalten sind).</locDE></param>
        /// <returns>Item count (maybe limited by stopCountAt value).<locDE><para />Anzahl der Elemente (ggf. limitiert durch stopCountAt Wert).</locDE></returns>
        public static int Count<T>(this System.Collections.Generic.IEnumerable<T> source, int stopCountAt)
        {
            if (null == source)
                return 0;

            System.Collections.Generic.ICollection<T> genCollection = source as System.Collections.Generic.ICollection<T>;
            if (null != genCollection)
            {
                return Math.Min(genCollection.Count, stopCountAt);
            }

            System.Collections.ICollection stdcollection = source as System.Collections.ICollection;
            if (null != stdcollection)
            {
                return Math.Min(stdcollection.Count, stopCountAt);
            }

            int num = 0;
            using (System.Collections.Generic.IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    num++;
                    if (num > stopCountAt)
                        break;
                }
            }
            return num;
        }

        #endregion Count (with stop limit)

        #region EqualContentsIgnoreOrder

        /// <summary>
        /// Determines if contents of both lists are equal ignoring their order.
        /// <locDE><para />Ermittelt ob die beiden Listen den gleichen Inhalt haben (ohne Rücksicht auf die Reihenfolge).</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the lists.<locDE><para />Elementtyp der Listen.</locDE></typeparam>
        /// <param name="list1">The first list.<locDE><para />Erste Liste.</locDE></param>
        /// <param name="list2">The second list.<locDE><para />Zweite Liste.</locDE></param>
        /// <param name="diff">The found difference between the collections (if any).<locDE><para />Gefundene Unterschiede (falls nicht gleich).</locDE></param>
        /// <returns>True if equal.<locDE><para />True falls identisch.</locDE></returns>
        public static bool EqualContentsIgnoreOrder<T>(this System.Collections.Generic.IEnumerable<T> list1, System.Collections.Generic.IEnumerable<T> list2,
            out System.Collections.Generic.ICollection<T> diff)
        {
            var listType = typeof(System.Collections.Generic.List<>);
            var constructedListType = listType.MakeGenericType(typeof(T));
            diff = Activator.CreateInstance(constructedListType) as System.Collections.Generic.ICollection<T>;

            #region Handle list(s) being null

            if (null == list1 && null == list2)
                return true;
            if (null == list1 && null != list2)
            {
                foreach (T s in list2)
                    diff.Add(s);
                return false;
            }
            if (null != list1 && null == list2)
            {
                foreach (T s in list1)
                    diff.Add(s);
                return false;
            }

            #endregion Handle list(s) being null

            // https://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order

            var cnt = new System.Collections.Generic.Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]++;
                else
                    cnt.Add(s, 1);
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]--;
                else
                    diff.Add(s);
            }
            //diff.Union(cnt.Where(x => x.Value != 0).Select(x => x.Key));
            System.Collections.Generic.IEnumerable<T> cntsNotZero = cnt.Where(x => x.Value != 0).Select(x => x.Key);
            if (null != cntsNotZero)
            {
                foreach (T cntNoZero in cntsNotZero)
                    diff.AddIfNotContains(cntNoZero);
            }
            return 0 == diff.Count();
        }

        /// <summary>
        /// Determines if contents of both lists are equal ignoring their order.
        /// <locDE><para />Ermittelt ob die beiden Listen den gleichen Inhalt haben (ohne Rücksicht auf die Reihenfolge).</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the lists.<locDE><para />Elementtyp der Listen.</locDE></typeparam>
        /// <param name="list1">The first list.<locDE><para />Erste Liste.</locDE></param>
        /// <param name="list2">The second list.<locDE><para />Zweite Liste.</locDE></param>
        /// <returns>True if equal.<locDE><para />True falls identisch.</locDE></returns>
        public static bool EqualContentsIgnoreOrder<T>(this System.Collections.Generic.IEnumerable<T> list1, System.Collections.Generic.IEnumerable<T> list2)
        {
            #region Handle list(s) being null

            if (null == list1 && null == list2)
                return true;
            if (null == list1 && null != list2)
                return false;
            if (null != list1 && null == list2)
                return false;

            #endregion Handle list(s) being null

            // https://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order

            var cnt = new System.Collections.Generic.Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]++;
                else
                    cnt.Add(s, 1);
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]--;
                else
                    return false;
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// Determines if contents of both lists are equal ignoring their order.
        /// <locDE><para />Ermittelt ob die beiden Listen den gleichen Inhalt haben (ohne Rücksicht auf die Reihenfolge).</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the lists.<locDE><para />Elementtyp der Listen.</locDE></typeparam>
        /// <param name="list1">The first list.<locDE><para />Erste Liste.</locDE></param>
        /// <param name="list2">The second list.<locDE><para />Zweite Liste.</locDE></param>
        /// <param name="comparer">The comparer method.<locDE><para />Vergleichsmethode.</locDE></param>
        /// <returns>True if equal.<locDE><para />True falls identisch.</locDE></returns>
        public static bool EqualContentsIgnoreOrder<T>(this System.Collections.Generic.IEnumerable<T> list1, System.Collections.Generic.IEnumerable<T> list2,
            System.Collections.Generic.IEqualityComparer<T> comparer)
        {
            #region Handle list(s) being null

            if (null == list1 && null == list2)
                return true;
            if (null == list1 && null != list2)
                return false;
            if (null != list1 && null == list2)
                return false;

            #endregion Handle list(s) being null

            // https://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order

            var cnt = new System.Collections.Generic.Dictionary<T, int>(comparer);
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]++;
                else
                    cnt.Add(s, 1);
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                    cnt[s]--;
                else
                    return false;
            }
            return cnt.Values.All(c => c == 0);
        }

        #endregion EqualContentsIgnoreOrder

        #region AddIfNotContains

        /// <summary>
        /// Adds if collection not already contains the given element.
        /// <locDE><para />Fügt das Element hinzu, falls es noch nicht in der Liste ist.</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the list.<locDE><para />Elementtyp der Liste.</locDE></typeparam>
        /// <param name="collection">The collection.<locDE><para />Die Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="comparer">The comparer method (or null).<locDE><para />Die Vergleichsmethode (oder null).</locDE></param>
        /// <returns>True if element was not contained yet.<locDE><para />True falls das Element noch nicht in der Liste war.</locDE></returns>
        public static bool AddIfNotContains<T>(this System.Collections.Generic.ICollection<T> collection, T value,
            System.Collections.Generic.IEqualityComparer<T> comparer = null)
        {
            if (null == collection)
                return false;

            if (IgnoreNullValues && null == value)
            {
                // Don't add null values
                // Null Wert nicht hinzufügen
                return false;
            }

            if (null == comparer)
            {
                if (collection.Contains(value))
                {
                    // Already contained
                    // Bereits enthalten
                    return false;
                }
            }
            else
            {
                if (collection.Contains(value, comparer))
                {
                    // Already contained
                    // Bereits enthalten
                    return false;
                }
            }

            collection.Add(value);
            return true;
        }

        #endregion AddIfNotContains

        #region RemoveIfContains

        /// <summary>
        /// Removes if collection contains the given element.
        /// <locDE><para />Entfernt das Element falls es in der Liste enthalten ist.</locDE>
        /// </summary>
        /// <typeparam name="T">The element type of the list.<locDE><para />Elementtyp der Liste.</locDE></typeparam>
        /// <param name="collection">The collection.<locDE><para />Die Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="comparer">The comparer method (or null).<locDE><para />Die Vergleichsmethode (oder null).</locDE></param>
        /// <returns>True if element was contained.<locDE><para />True falls das Element in der Liste war.</locDE></returns>
        public static bool RemoveIfContains<T>(this System.Collections.Generic.ICollection<T> collection, T value,
            System.Collections.Generic.IEqualityComparer<T> comparer = null)
        {
            if (null == collection)
                return false;

            if (IgnoreNullValues && null == value)
            {
                // Don't remove null values
                // Null Wert nicht entfernen
                return false;
            }

            if (null == comparer)
            {
                if (collection.Contains(value))
                {
                    // Contained, remove
                    // Enthalten, entfernen
                    collection.Remove(value);
                    return true;
                }
            }
            else
            {
                if (collection.Contains(value, comparer))
                {
                    // Contained, remove
                    // Enthalten, entfernen
                    collection.Remove(value);
                    return true;
                }
            }

            // Not contained
            // Nicht enthalten
            return false;
        }

        #endregion RemoveIfContains

        #region RemoveAll

        /// <summary>
        /// Removes multiple items (i.e. determined by Linq expression) from the specified collection.
        /// <locDE><para />Entfernt mehrere Elemente (z.B. ermittelt durch Linq Ausdruck) von der angegebenen Collection.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="collection">The collection.<locDE><para />Die Collection.</locDE></param>
        /// <param name="condition">The condition.<locDE><para />Die Bedingung.</locDE></param>
        /// <example>
        /// Removes all elements with IsSelected property set to true.
        /// <locDE><para />Entfernt alle Elemente, deren Eigenschaft IsSelected = true enthält.</locDE>
        /// <code>
        /// var collection = new ObservableCollection&lt;SelectableItem&gt;();
        /// collection.RemoveAll(x =&gt; x.IsSelected);
        /// </code>
        /// </example>
        /// <returns>Number of removed items.<locDE><para />Anzahl der entfernten Elemente.</locDE></returns>
        public static int RemoveAll<T>(this System.Collections.Generic.ICollection<T> collection, Func<T, bool> condition)
        {
            if (null == collection)
                return 0;

            // .ToList() saves us from "collection was modified" exception
            // .ToList() bewahrt uns vor einer "Collection wurde verändert" Ausnahme.
            var itemsToRemove = collection.Where(condition).ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                collection.Remove(itemToRemove);
            }

            return itemsToRemove.Count;
        }

        #endregion RemoveAll

        #region RemoveAllBut

        /// <summary>
        /// Removes all but mentioned items (i.e. determined by Linq expression) from the specified collection.
        /// <locDE><para />Entfernt alle außer die angegebenen Elemente (z.B. ermittelt durch Linq Ausdruck) von der angegebenen Collection.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="collection">The collection.<locDE><para />Die Collection.</locDE></param>
        /// <param name="condition">The condition.<locDE><para />Die Bedingung.</locDE></param>
        /// <example>
        /// Removes all elements but those with IsSelected property set to true.
        /// <locDE><para />Entfernt alle Elemente außer jenen, deren Eigenschaft IsSelected = true enthält.</locDE>
        /// <code>
        /// var collection = new ObservableCollection&lt;SelectableItem&gt;();
        /// collection.RemoveAllBut(x =&gt; x.IsSelected);
        /// </code>
        /// </example>
        /// <returns>Number of removed items.<locDE><para />Anzahl der entfernten Elemente.</locDE></returns>
        public static int RemoveAllBut<T>(this System.Collections.Generic.ICollection<T> collection, Func<T, bool> condition)
        {
            if (null == collection)
                return 0;

            // .ToList() saves us from "collection was modified" exception
            // .ToList() bewahrt uns vor einer "Collection wurde verändert" Ausnahme.
            var itemsToKeep = collection.Where(condition).ToList();

            int itemsRemoved = collection.Count - itemsToKeep.Count();
            collection.Clear();
            foreach (var itemToKeep in itemsToKeep)
            {
                collection.Add(itemToKeep);
            }

            return itemsRemoved;
        }

        #endregion RemoveAllBut

        #region Apply, AddRange, RemoveRange

        // More information: / Nähere Informationen:
        // http://xcalibursystems.com/2013/12/making-a-better-observablecollection-part-1-extensions/

        /// <summary>
        /// Adds the range.
        /// <locDE><para />Bereich (die angegebenen Elemente) hinzufügen.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="items">The items.<locDE><para />Die Elemente.</locDE></param>
        /// <param name="collection">The collection of items to add.<locDE><para />Die Collection von hinzuzufügenden Elementen.</locDE></param>
        public static void AddRange<T>(this System.Collections.Generic.IList<T> items, System.Collections.Generic.IEnumerable<T> collection)
        {
            if (null == items || null == collection)
                return;

            // Add range to local items
            // Den Bereich (die angegebenen Elemente) zu den lokalen Elementen hinzufügen
            collection.Apply(items.Add);
        }

        /// <summary>
        /// Applies the specified changes to the collection.
        /// <locDE><para />Wendet die angegebenen Änderungen auf die Collection an.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="items">The items.<locDE><para />Die Elemente.</locDE></param>
        /// <param name="predicate">The predicate.<locDE><para />Das Prädikat.</locDE></param>
        public static void Apply<T>(this System.Collections.Generic.IEnumerable<T> items, Action<T> predicate)
        {
            if (null == items || null == predicate)
                return;

            // .ToList() saves us from "collection was modified" exception
            // .ToList() bewahrt uns vor einer "Collection wurde verändert" Ausnahme.
            foreach (var item in items.ToList())
            {
                predicate(item);
            }
        }

        /// <summary>
        /// Removes the range.
        /// <locDE><para />Bereich (die angegebenen Elemente) entfernen.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="items">The items.<locDE><para />Die Elemente.</locDE></param>
        /// <param name="collection">The collection of items to remove.<locDE><para />Die Collection von zu entfernenden Elementen.</locDE></param>
        public static void RemoveRange<T>(this System.Collections.Generic.IList<T> items, System.Collections.Generic.IEnumerable<T> collection)
        {
            if (null == items || null == collection)
                return;

            // Remove range from local items
            // Den Bereich (die angegebenen Elemente) aus den lokalen Elementen entfernen
            collection.Apply(p => items.Remove(p));
        }

        #endregion Apply, AddRange, RemoveRange

        #region Append (for different data types)

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, byte value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, Int16 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, UInt16 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, Int32 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, UInt32 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, Int64 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, UInt64 value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, float value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        /// <summary>
        /// Appends to list (ensures little endian byte order).
        /// <locDE><para />Fügt den Wert einer Liste hinzu (stellt Little Endian Bytereihenfolge sicher).</locDE>
        /// </summary>
        /// <param name="list">The list of bytes.<locDE><para />Die Byte-Liste.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The list for fluent API cascade.<locDE><para />Die Liste für Fluent-API-Kaskadierung.</locDE></returns>
        public static IList<byte> Append(this IList<byte> list, double value)
        {
            DataTypeConverter.ByteConverter.AppendToList(value, list);
            return list;
        }

        #endregion Append (for different data types)

        #region To... methods

        /// <summary>
        /// Converts to byte.
        /// <locDE><para />Konvertiert zu Byte.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ToByte(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToByte(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to byte.
        /// <locDE><para />Konvertiert zu Byte.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ToByte(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToByte(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to double.
        /// <locDE><para />Konvertiert zu Double.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ToDouble(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToDouble(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to double.
        /// <locDE><para />Konvertiert zu Double.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ToDouble(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToDouble(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to float.
        /// <locDE><para />Konvertiert zu Float.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Float value.<locDE><para />Float Wert.</locDE></returns>
        public static float ToFloat(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToFloat(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to float.
        /// <locDE><para />Konvertiert zu Float.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Float value.<locDE><para />Float Wert.</locDE></returns>
        public static float ToFloat(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToFloat(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int16.
        /// <locDE><para />Konvertiert zu Int16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int16 value.<locDE><para />Int16 Wert.</locDE></returns>
        public static Int16 ToInt16(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt16(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int16.
        /// <locDE><para />Konvertiert zu Int16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int16 value.<locDE><para />Int16 Wert.</locDE></returns>
        public static Int16 ToInt16(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt16(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int32.
        /// <locDE><para />Konvertiert zu Int32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static Int32 ToInt32(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt32(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int32.
        /// <locDE><para />Konvertiert zu Int32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static Int32 ToInt32(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt32(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int64.
        /// <locDE><para />Konvertiert zu Int64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static Int64 ToInt64(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt64(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to Int64.
        /// <locDE><para />Konvertiert zu Int64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static Int64 ToInt64(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToInt64(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt16.
        /// <locDE><para />Konvertiert zu UInt16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt16 value.<locDE><para />UInt16 Wert.</locDE></returns>
        public static UInt16 ToUInt16(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt16(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt16.
        /// <locDE><para />Konvertiert zu UInt16.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt16 value.<locDE><para />UInt16 Wert.</locDE></returns>
        public static UInt16 ToUInt16(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt16(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt32.
        /// <locDE><para />Konvertiert zu UInt32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ToUInt32(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt32(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt32.
        /// <locDE><para />Konvertiert zu UInt32.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ToUInt32(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt32(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt64.
        /// <locDE><para />Konvertiert zu UInt64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ToUInt64(this byte[] buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt64(buffer, startIndex, reverseByteOrder);
        }

        /// <summary>
        /// Converts to UInt64.
        /// <locDE><para />Konvertiert zu UInt64.</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="reverseByteOrder">Reverse byte order?<locDE><para />Byte-Reihenfolge umkehren?</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ToUInt64(this IList<byte> buffer, int startIndex, bool reverseByteOrder = false)
        {
            return DataTypeConverter.ByteConverter.ToUInt64(buffer, startIndex, reverseByteOrder);
        }

        #endregion To... methods

        #region FindSequences

        // FindSequences is named Locate in source post:
        // http://stackoverflow.com/questions/283456/byte-array-pattern-search

        //private static readonly int[] Empty = new int[0];

        /// <summary>
        /// Finds the given sequence in this list, returns 0..n start indices of found sequence(s).
        /// <locDE><para />Findet die angegebene Sequenz in dieser Liste, liefert 0..n Startindizes der gefundenen Sequenz(en).</locDE>
        /// </summary>
        /// <example>
        /// Find all BACnet message headers:
        /// <locDE><para />Finde alle BACnet Nachrichtenheader:</locDE>
        /// <code>
        /// IList&lt;int&gt; msgStarts = rxBuffer.FindSequences(new byte[] { 0x55, 0xFF, 0x01 });
        /// </code>
        /// </example>
        /// <param name="self">The self reference.<locDE><para />Selbstreferenz.</locDE></param>
        /// <param name="sequence">The sequence to find.<locDE><para />Die zu findende Sequenz.</locDE></param>
        /// <returns></returns>
        public static System.Collections.Generic.IList<int> FindSequences(this System.Collections.IList self, System.Collections.IList sequence)
        {
            if (IsEmptyLocate(self, sequence))
                return null;
            //return Empty;

            var list = new System.Collections.Generic.List<int>();

            for (int i = 0; i < self.Count; i++)
            {
                if (!IsMatch(self, i, sequence))
                    continue;

                list.Add(i);
            }

            //return list.Count == 0 ? Empty : list.ToArray();
            return list;
        }

        private static bool IsEmptyLocate(System.Collections.IList buffer, System.Collections.IList sequence)
        {
            return buffer == null
                || sequence == null
                || buffer.Count == 0
                || sequence.Count == 0
                || sequence.Count > buffer.Count;
        }

        private static bool IsMatch(System.Collections.IList buffer, int position, System.Collections.IList sequence)
        {
            if (sequence.Count > (buffer.Count - position))
                return false;

            for (int i = 0; i < sequence.Count; i++)
            {
                // DOES not work with two objects, never matches:
                //if (buffer[position + i] != sequence[i])
                //    return false;

                object left = buffer[position + i];
                object right = sequence[i];
                if (null == left && null == right)
                    continue;   // Both are null, which is the same
                if (null == left || null == right || !left.Equals(right))
                    return false;
            }

            return true;
        }

        #endregion FindSequences

        #region GetOrDefault

        /// <summary>
        /// Gets the dictionary entry or default value (i.e. null).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key to fetch the entry for.</param>
        /// <returns></returns>
        public static U GetOrDefault<T, U>(this IDictionary<T, U> dict, T key) //where U : class
        {
            // https://stackoverflow.com/a/14160879/5848880

            U val;
            if (dict.TryGetValue(key, out val))
                return val;
            return default(U);
        }

        /// <summary>
        /// Gets the dictionary entry or default value (i.e. null).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key to fetch the entry for.</param>
        /// <param name="wasFound">If set to <c>true</c>, key was found in dictionary.</param>
        /// <returns></returns>
        public static U GetOrDefault<T, U>(this IDictionary<T, U> dict, T key, out bool wasFound) //where U : class
        {
            wasFound = false;

            // https://stackoverflow.com/a/14160879/5848880

            U val;
            if (dict.TryGetValue(key, out val))
            {
                wasFound = true;
                return val;
            }
            return default(U);
        }

        #endregion GetOrDefault

        #region Maybe helpful? Vielleicht mal hilfreich?

        ///// <summary>
        ///// Finds pattern (byte sequences) in buffer, returns list of start indices if found.
        ///// </summary>
        ///// <param name="buffer">The buffer to search in.</param>
        ///// <param name="pattern">The pattern to find within buffer.</param>
        ///// <param name="startIndex">The start index for search operation.</param>
        ///// <returns></returns>
        //public static System.Collections.Generic.List<int> IndexOfSequence(this byte[] buffer, byte[] pattern, int startIndex = 0)
        //{
        //    // http://stackoverflow.com/questions/283456/byte-array-pattern-search
        //    System.Collections.Generic.List<int> positions = new System.Collections.Generic.List<int>();
        //    int i = Array.IndexOf<byte>(buffer, pattern[0], startIndex);
        //    while (i >= 0 && i <= buffer.Length - pattern.Length)
        //    {
        //        byte[] segment = new byte[pattern.Length];
        //        Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
        //        if (segment.SequenceEqual<byte>(pattern))
        //            positions.Add(i);
        //        //i = Array.IndexOf<byte>(buffer, pattern[0], i + pattern.Length);
        //        i = Array.IndexOf<byte>(buffer, pattern[0], i + 1);
        //    }
        //    return positions;
        //}

        //#region CloneDictionaryCloningValues
        ///// <summary>
        ///// Clones the dictionary cloning values.
        ///// </summary>
        ///// <typeparam name="TKey">The type of the T key.</typeparam>
        ///// <typeparam name="TValue">The type of the T value.</typeparam>
        ///// <param name="original">The original.</param>
        ///// <returns></returns>
        //public static System.Collections.Generic.Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(this System.Collections.Generic.Dictionary<TKey, TValue> original) where TValue : ICloneable
        //{
        //    // http://stackoverflow.com/questions/139592/what-is-the-best-way-to-clone-deep-copy-a-net-generic-dictionarystring-t

        //    System.Collections.Generic.Dictionary<TKey, TValue> ret = new System.Collections.Generic.Dictionary<TKey, TValue>(original.Count, original.Comparer);
        //    foreach (System.Collections.Generic.KeyValuePair<TKey, TValue> entry in original)
        //    {
        //        ret.Add(entry.Key, (TValue)entry.Value.Clone());
        //    }
        //    return ret;
        //}
        //#endregion

        #endregion Maybe helpful? Vielleicht mal hilfreich?
    }
}