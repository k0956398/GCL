namespace EplusE
{
    /// <summary>
    /// File stream helper class.
    /// <locDE><para />FileStream Hilfsklasse.</locDE>
    /// </summary>
    public static class FileStreamHelper
    {
        #region WriteToFile

        /// <summary>
        /// Writes the stream to file.
        /// <locDE><para />Schreibt den Datenstrom in eine Datei.</locDE>
        /// </summary>
        /// <param name="stream">The stream.<locDE><para />Der Datenstrom.</locDE></param>
        /// <param name="targetFilename">The target filename.<locDE><para />Der Zieldateiname.</locDE></param>
        /// <param name="seekToBeginning">Seek to beginning of input stream?<locDE><para />Zum Anfang des Quelldatenstroms springen?</locDE></param>
        public static void WriteToFile(this System.IO.Stream stream, string targetFilename, bool seekToBeginning = true)
        {
            using (System.IO.FileStream fileStream = System.IO.File.Create(targetFilename))
            {
                if (seekToBeginning && stream.CanSeek)
                    stream.Position = 0;    // or: Seek(0, SeekOrigin.Begin);

                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }

        #endregion WriteToFile
    }
}