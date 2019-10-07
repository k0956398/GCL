namespace EplusE
{
    /// <summary>
    /// Stream helper class.
    /// <locDE><para />Stream Hilfsklasse.</locDE>
    /// </summary>
    public static class StreamHelper
    {
        #region StringToMemoryStream

        /// <summary>
        /// Creates a memory stream from a string value.
        /// NOTE: Don't forget to call Dispose on the memory stream!
        /// <locDE><para />Erzeugt einen MemoryStream aus einem Stringwert.
        /// HINWEIS: Nicht vergessen, Dispose des MemoryStreams aufzurufen!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>MemoryStream object.<locDE><para />MemoryStream Objekt.</locDE></returns>
        public static System.IO.MemoryStream ToMemoryStream(this string value)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            // StreamWriter closes the attached stream by default!
            // http://stackoverflow.com/a/2666906
            // In earlier versions of .NET Framework prior to 4.5, StreamWriter assumes it owns the stream.
            // So don't dispose the StreamWriter; just flush it.
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
            {
                writer.Write(value);
                writer.Flush();
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Creates a memory stream from a string value.
        /// NOTE: Don't forget to call Dispose on the memory stream!
        /// <locDE><para />Erzeugt einen MemoryStream aus einem Stringwert.
        /// HINWEIS: Nicht vergessen, Dispose des MemoryStreams aufzurufen!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="count">The count (-1 = from <paramref name="startIndex"/> to end).<locDE><para />Die Anzahl (-1 = von Startindex bis zum Ende).</locDE></param>
        /// <returns>MemoryStream object.<locDE><para />MemoryStream Objekt.</locDE></returns>
        public static System.IO.MemoryStream ToMemoryStream(this string value, int startIndex, int count)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream(StringHelper.StringToByteArray(value, startIndex, count));
            stream.Position = 0;
            return stream;
        }

        #endregion StringToMemoryStream

        #region ReadToEnd

        /// <summary>
        /// Reads whole contents of a stream and returns a string.
        /// <locDE><para />Liest den gesamten Datenstrominhalt und liefert einen String.</locDE>
        /// </summary>
        /// <param name="stream">The stream.<locDE><para />Der Datenstrom.</locDE></param>
        /// <returns>String value with whole contents of the stream.<locDE><para />String mit dem gesamten Datenstrominhalt.</locDE></returns>
        public static string ReadToEnd(this System.IO.Stream stream)
        {
            byte[] contents;
            ReadToEnd(stream, out contents);
            return StringHelper.ExtractStringContent(contents);
        }

        /// <summary>
        /// Reads whole contents of a stream and returns a memory stream.
        /// NOTE: Don't forget to call Dispose on the memory stream!
        /// <locDE><para />Liest den gesamten Datenstrominhalt und liefert einen MemoryStream.
        /// HINWEIS: Nicht vergessen, Dispose des MemoryStreams aufzurufen!</locDE>
        /// </summary>
        /// <param name="stream">The stream.<locDE><para />Der Datenstrom.</locDE></param>
        /// <param name="contents">The whole contents of the stream (or null).<locDE><para />Der gesamte Datenstrominhalt (oder null).</locDE></param>
        public static void ReadToEnd(this System.IO.Stream stream, out System.IO.MemoryStream contents)
        {
            contents = null;

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Flush();
            memoryStream.Position = 0;
            contents = memoryStream;
        }

        /// <summary>
        /// Reads whole contents of a stream and returns a byte array.
        /// <locDE><para />Liest den gesamten Datenstrominhalt und liefert einen Byte Array.</locDE>
        /// </summary>
        /// <param name="stream">The stream.<locDE><para />Der Datenstrom.</locDE></param>
        /// <param name="contents">The whole contents of the stream (or null).<locDE><para />Der gesamte Datenstrominhalt (oder null).</locDE></param>
        public static void ReadToEnd(this System.IO.Stream stream, out byte[] contents)
        {
            contents = null;

            System.IO.MemoryStream memoryStream;
            ReadToEnd(stream, out memoryStream);
            if (null != memoryStream)
            {
                contents = memoryStream.ToArray();
                memoryStream.Dispose();
            }

            #region Alternative (?)

            //// Creating a byte array from a stream
            //// http://stackoverflow.com/a/221941/5848880
            //byte[] buffer = new byte[16 * 1024];
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    int read;
            //    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, 0, read);
            //    }
            //    return ms.ToArray();
            //}

            //// How to convert an Stream into a byte[] in C#?
            //// https://stackoverflow.com/questions/1080442/how-to-convert-an-stream-into-a-byte-in-c

            //long originalPosition = 0;

            //if (stream.CanSeek)
            //{
            //    originalPosition = stream.Position;
            //    stream.Position = 0;
            //}

            //try
            //{
            //    byte[] readBuffer = new byte[4096];

            //    int totalBytesRead = 0;
            //    int bytesRead;

            //    while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            //    {
            //        totalBytesRead += bytesRead;

            //        if (totalBytesRead == readBuffer.Length)
            //        {
            //            int nextByte = stream.ReadByte();
            //            if (nextByte != -1)
            //            {
            //                byte[] temp = new byte[readBuffer.Length * 2];
            //                Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
            //                Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
            //                readBuffer = temp;
            //                totalBytesRead++;
            //            }
            //        }
            //    }

            //    byte[] buffer = readBuffer;
            //    if (readBuffer.Length != totalBytesRead)
            //    {
            //        buffer = new byte[totalBytesRead];
            //        Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            //    }
            //    return buffer;
            //}
            //finally
            //{
            //    if (stream.CanSeek)
            //    {
            //        stream.Position = originalPosition;
            //    }
            //}

            #endregion Alternative (?)
        }

        #endregion ReadToEnd
    }
}