using System;
using System.Collections.Generic;

namespace EplusE.Measurement
{
    /// <summary>
    /// Helper class to handle MVCode string representations.
    /// </summary>
    public static class MVCodeHelper
    {
        /// <summary>
        /// Adds the specified MVCodes (given as string list) to MVCode collection.
        /// </summary>
        /// <param name="mvCodes">MVCode collection</param>
        /// <param name="itemsToAdd">Items to add</param>
        public static void Add(IList<MVCode> mvCodes, string itemsToAdd)
        {
            Modify(mvCodes, itemsToAdd, true);
        }

        /// <summary>
        /// Parses a possible MVCode safely (returns INVALID if no valid MVCode).
        /// </summary>
        /// <param name="possibleMVCode">The possible MVCode</param>
        /// <param name="setSIunitIfUnknown">If undefined, set to SI or US unit system?</param>
        /// <returns></returns>
        public static MVCode ParseSafe(string possibleMVCode, bool setSIunitIfUnknown = true)
        {
            if (string.IsNullOrWhiteSpace(possibleMVCode))
                return MVCode.INVALID;

            string codePart = possibleMVCode.Truncate("[", false).Trim();
            if (string.IsNullOrWhiteSpace(codePart))
                return MVCode.INVALID;

            try
            {
                //return (MVCode)Enum.Parse(typeof(MVCode), codePart, true);
                MVCode mvCode;
                if (Enum.TryParse(codePart, true, out mvCode))
                    return mvCode;
            }
            catch { }

            return MVCode.INVALID;
        }

        /// <summary>
        /// Removes the specified MVCodes (given as string list) from MVCode collection.
        /// </summary>
        /// <param name="mvCodes">MVCode collection</param>
        /// <param name="itemsToRemove">Items to remove</param>
        public static void Remove(IList<MVCode> mvCodes, string itemsToRemove)
        {
            Modify(mvCodes, itemsToRemove, false);
        }

        /// <summary>
        /// Modifies the MVCode collection with specified MVCodes (given as string list).
        /// </summary>
        /// <param name="mvCodes">MVCode collection</param>
        /// <param name="itemsToProcess">Items to add or remove</param>
        /// <param name="adding">Given items should be added (true) or removed (false)</param>
        private static void Modify(IList<MVCode> mvCodes, string itemsToProcess, bool adding)
        {
            if (null == mvCodes || null == itemsToProcess)
                return;

            string[] items = itemsToProcess.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                MVCode mvCode = MVCodeHelper.ParseSafe(item);
                MVClass mvClass = MVClassHelper.ParseSafe(item);

                if (MVCode.INVALID != mvCode)
                {
                    if (adding && !mvCodes.Contains(mvCode))
                        mvCodes.Add(mvCode);
                    if (!adding && mvCodes.Contains(mvCode))
                        mvCodes.Remove(mvCode);
                }

                if (MVClass.INVALID != mvClass)
                {
                    IEnumerator<MVCode> enumerator = MVEnumerator.GetCodesOfClass(mvClass);
                    while (enumerator.MoveNext())
                    {
                        MVCode current = enumerator.Current;
                        if (current == MVCode.INVALID)
                            continue;

                        if (adding && !mvCodes.Contains(current))
                            mvCodes.Add(current);
                        if (!adding && mvCodes.Contains(current))
                            mvCodes.Remove(current);
                    }
                }
            }
        }
    }
}