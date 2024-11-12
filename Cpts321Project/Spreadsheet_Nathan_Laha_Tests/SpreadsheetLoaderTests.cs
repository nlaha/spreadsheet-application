namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine;

    /// <summary>
    /// Class for testing the spreadsheet loader
    /// </summary>
    internal class SpreadsheetLoaderTests
    {
        /// <summary>
        /// Test spreadsheet loading
        /// </summary>
        [Test]
        public void Spreadsheet_LoadEmpty_ClearsCells()
        {
            // arrange
            Spreadsheet spreadsheet = new (Constants.NUMCOLUMNS, Constants.NUMROWS);
            spreadsheet.SetCellText(0, 0, "Hello World");

            Stream stream = new MemoryStream();

            // act
            spreadsheet = SpreadsheetLoader.Load(stream);

            // assert
            Assert.That(spreadsheet.GetCell(0, 0).Text, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests saving a spreadsheet
        /// </summary>
        [Test]
        public void Spreadsheet_Save()
        {
            // arrange
            Spreadsheet spreadsheet = new (Constants.NUMCOLUMNS, Constants.NUMROWS);
            spreadsheet.SetCellText(0, 0, "Hello World");

            Stream stream = new MemoryStream();

            // act
            SpreadsheetLoader.Save(stream, spreadsheet);

            // assert
            Assert.That(stream.Length, Is.GreaterThan(0));
        }
    }
}
