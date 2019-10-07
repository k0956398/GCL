using System;

namespace EplusE.Measurement
{
    /// <summary>
    /// Helper class to handle MVClass string representations.
    /// </summary>
    public static class MVClassHelper
    {
        /// <summary>
        /// Parses a possible MVClass safely (returns INVALID if no valid MVClass).
        /// </summary>
        /// <param name="possibleMVClass">The possible MVClass</param>
        /// <returns></returns>
        public static MVClass ParseSafe(string possibleMVClass)
        {
            if (string.IsNullOrEmpty(possibleMVClass))
                return MVClass.INVALID;

            string codePart = possibleMVClass.Truncate("[", false).Trim();
            if (string.IsNullOrWhiteSpace(codePart))
                return MVClass.INVALID;

            try
            {
                //return (MVClass)Enum.Parse(typeof(MVClass), codePart, true);
                MVClass mvClass;
                if (Enum.TryParse(codePart, true, out mvClass))
                    return mvClass;
            }
            catch { }

            return MVClass.INVALID;
        }
    }
}