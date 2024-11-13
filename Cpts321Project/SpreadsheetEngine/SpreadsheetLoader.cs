// <copyright file="SpreadsheetLoader.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.Xml.Serialization;

    /// <summary>
    /// Class responsible for saving and loading a spreadsheet
    /// </summary>
    public class SpreadsheetLoader
    {
        /// <summary>
        /// Saves a spreadsheet
        /// </summary>
        /// <param name="stream">the stream to save to</param>
        /// <param name="spreadsheet">the spreadsheet</param>
        public static void Save(Stream stream, Spreadsheet spreadsheet)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Spreadsheet));
            TextWriter writer = new StreamWriter(stream);
            serializer.Serialize(writer, spreadsheet);
            writer.Close();
        }

        /// <summary>
        /// Loads a spreadsheet
        /// </summary>
        /// <param name="stream">the stream to load from</param>
        /// <returns>the loaded spreadsheet</returns>
        public static Spreadsheet Load(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Spreadsheet));
            var newSpreadsheet = serializer.Deserialize(stream) as Spreadsheet;
            if (newSpreadsheet != null)
            {
                return newSpreadsheet;
            }
            else
            {
                throw new IOException("Failed to load spreadsheet");
            }
        }
    }
}
