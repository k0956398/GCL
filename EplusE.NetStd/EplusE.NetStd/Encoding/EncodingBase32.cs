using System;
using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Base32 encoding/decoding class.
    /// <locDE><para />Base32 Codierung/Decodierung.</locDE>
    /// </summary>
    public static class EncodingBase32
    {
        // https://stackoverflow.com/questions/641361/base32-decoding
        // Check with:
        // https://www.dcode.fr/base-32-encoding
        // https://emn178.github.io/online-tools/base32_decode.html

        #region DecodeToString

        /// <summary>
        /// Decodes string to string.
        /// <locDE><para />Decodiert die Zeichenkette in eine neue Zeichenkette.</locDE>
        /// </summary>
        /// <param name="input">The input.<locDE><para />Die Zeichenkette.</locDE></param>
        /// <returns>Decoded string.<locDE><para />Decodierte Zeichenkette.</locDE></returns>
        public static string DecodeToString(string input)
        {
            byte[] bytes = DecodeToBytes(input);
            return StringHelper.ExtractStringContent(bytes);
        }

        #endregion DecodeToString

        #region DecodeToBytes

        /// <summary>
        /// Decodes string to byte array.
        /// <locDE><para />Decodiert die Zeichenkette in einen Byte-Array.</locDE>
        /// </summary>
        /// <param name="input">The input.<locDE><para />Die Zeichenkette.</locDE></param>
        /// <returns>Decoded string.<locDE><para />Decodierte Zeichenkette.</locDE></returns>
        public static byte[] DecodeToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            input = input.TrimEnd('='); //remove padding characters
            int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (char c in input)
            {
                int cValue = CharToValue(c);

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }

            return returnArray;
        }

        #endregion DecodeToBytes

        #region Encode

        /// <summary>
        /// Encodes the string to a string.
        /// <locDE><para />Codiert die Zeichenkette in eine neue Zeichenkette.</locDE>
        /// </summary>
        /// <param name="input">The input.<locDE><para />Die Zeichenkette.</locDE></param>
        /// <returns>Encoded string.<locDE><para />Codierte Zeichenkette.</locDE></returns>
        public static string Encode(string input)
        {
            System.Collections.Generic.IList<byte> bytes = new System.Collections.Generic.List<byte>();
            StringHelper.AddStringToByteList(input, bytes);
            return Encode(bytes.ToArray());
        }

        /// <summary>
        /// Encodes the byte array to a string.
        /// <locDE><para />Codiert den Byte-Array in eine neue Zeichenkette.</locDE>
        /// </summary>
        /// <param name="input">The input.<locDE><para />Der Byte-Array.</locDE></param>
        /// <returns>Encoded string.<locDE><para />Codierte Zeichenkette.</locDE></returns>
        public static string Encode(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input");
            }

            int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in input)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            //if we didn't end with a full char
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
            }

            return new string(returnArray);
        }

        #endregion Encode

        #region CharToValue

        private static int CharToValue(char c)
        {
            int value = (int)c;

            //65-90 == uppercase letters
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            //50-55 == numbers 2-7
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            //97-122 == lowercase letters
            if (value < 123 && value > 96)
            {
                return value - 97;
            }

            throw new ArgumentException("Character is not a Base32 character.", "c");
        }

        #endregion CharToValue

        #region ValueToChar

        private static char ValueToChar(byte b)
        {
            if (b < 26)
            {
                return (char)(b + 65);
            }

            if (b < 32)
            {
                return (char)(b + 24);
            }

            throw new ArgumentException("Byte is not a value Base32 value.", "b");
        }

        #endregion ValueToChar
    }
}