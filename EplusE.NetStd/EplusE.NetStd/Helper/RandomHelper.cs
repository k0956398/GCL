using System;

namespace EplusE
{
    // Based on Jon Skeet's StaticRandom:
    // http://www.yoda.arachsys.com/csharp/miscutil/

    /// <summary>
    /// Thread-safe equivalent of System.Random, using just static methods.
    /// If all you want is a source of random numbers, this is an easy class to use.
    /// If you need to specify your own seeds (eg for reproducible sequences of numbers), use System.Random.
    /// <locDE><para />Thread-sicheres Äquivalent zu System.Random unter Verwendung von statischen Methoden.
    /// Falls lediglich eine Bezugsquelle für Zufallszahlen benötigt wird, ist dies eine einfach zu verwendende Klasse.
    /// Falls eigene Seeds (z.B. für reproduzierbare Sequenzen von Zufallszahlen) angegeben werden sollen, sollte System.Random direkt verwendet werden.</locDE>
    /// </summary>
    public static class RandomHelper
    {
        private static object myLock = new object();
        private static Random random = new Random();

        #region Between

        /// <summary>
        /// Returns a random number between min and max (&gt;= min and &lt;= max).
        /// <locDE><para />Liefert eine Zufallszahl zwischen min und max (&gt;= min und &lt;= max).</locDE>
        /// </summary>
        /// <param name="min">The minimum value (result is &gt;= this value).
        /// <locDE><para />Der kleinsmögliche Wert (Ergebnis ist &gt;= dieser Wert).</locDE></param>
        /// <param name="maxInclusive">The maximum value (result is &lt;= this value).
        /// <locDE><para />Der größtmögliche Wert (Ergebnis ist &lt;= dieser Wert).</locDE></param>
        /// <returns>A random number.<locDE><para />Eine Zufallszahl.</locDE></returns>
        public static int Between(int min, int maxInclusive)
        {
            lock (myLock)
            {
                return random.Next(min, maxInclusive + 1);
            }
        }

        #endregion Between

        #region Next

        /// <summary>
        /// Returns a nonnegative random number.
        /// <locDE><para />Liefert eine positive Zufallszahl (0..n).</locDE>
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than Int32.MaxValue.
        /// <locDE><para />Ein 32-bit Integer &gt;= 0 und $lt; Int32.MaxValue.</locDE></returns>
        public static int Next()
        {
            lock (myLock)
            {
                return random.Next();
            }
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// <locDE><para />Liefert eine positive Zufallszahl &lt; <paramref name="max"/> (0..max-1).</locDE>
        /// </summary>
        /// <param name="max">The maximum value (result is 0..max-1).
        /// <locDE><para />Der Maximalwert (Ergebnis ist 0..max-1).</locDE></param>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than <paramref name="max"/>.
        /// <locDE><para />Ein 32-bit Integer &gt;= 0 und $lt; <paramref name="max"/>.</locDE></returns>
        /// <exception cref="ArgumentOutOfRangeException">max is less than zero.<locDE><para />max ist kleiner als 0.</locDE></exception>
        public static int Next(int max)
        {
            lock (myLock)
            {
                return random.Next(max);
            }
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// <locDE><para />Liefert eine Zufallszahl im Bereich &gt;= <paramref name="min"/> und &lt; <paramref name="max"/> (min..max-1).</locDE>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.
        /// <locDE><para />Der Minimalwert (Ergebnis ist &gt;= dieser Wert).</locDE></param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.
        /// <locDE><para />Der Maximalwert (Ergebnis ist &lt; dieser Wert). <paramref name="max"/> muss &gt;= <paramref name="min"/> sein.</locDE>
        /// </param>
        /// <returns>A 32-bit signed integer greater than or equal to <paramref name="min"/> and less than <paramref name="max"/>.
        /// <locDE><para />Ein 32-bit Integer &gt;= <paramref name="min"/> und $lt; <paramref name="max"/>.</locDE></returns>
        /// <exception cref="ArgumentOutOfRangeException">min is greater than max.<locDE><para />min ist größer als max.</locDE></exception>
        public static int Next(int min, int max)
        {
            lock (myLock)
            {
                return random.Next(min, max);
            }
        }

        #endregion Next

        #region NextDouble

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// <locDE><para />Liefert eine Zufallszahl zwischen 0,0 und 1,0.</locDE>
        /// </summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.
        /// <locDE><para />Liefert eine Zufallszahl &gt;= 0.0 und &lt; 1.0.</locDE></returns>
        public static double NextDouble()
        {
            lock (myLock)
            {
                return random.NextDouble();
            }
        }

        #endregion NextDouble

        #region NextBytes

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// <locDE><para />Füllt den angegebenen Byte-Array mit Zufallszahlen.</locDE>
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.
        /// <locDE><para />Byte-Array, welcher mit Zufallszahlen gefüllt werden soll.</locDE></param>
        /// <exception cref="ArgumentNullException">buffer is a null reference.<locDE><para />buffer ist Null.</locDE></exception>
        public static void NextBytes(byte[] buffer)
        {
            lock (myLock)
            {
                random.NextBytes(buffer);
            }
        }

        #endregion NextBytes
    }
}