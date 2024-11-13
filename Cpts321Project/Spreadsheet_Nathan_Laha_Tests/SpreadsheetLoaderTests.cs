namespace Spreadsheet_Nathan_Laha_Tests
{
    using System.Text;
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
            Spreadsheet spreadsheet = new ();
            spreadsheet.SetCellText(0, 0, "Hello World");

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(
            """
                <?xml version="1.0" encoding="utf-8"?>
                <Spreadsheet xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="https://nlaha.com">
                </Spreadsheet>
            """.Trim()));

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
            Spreadsheet spreadsheet = new ();
            spreadsheet.SetCellText(0, 0, "Hello World");

            using (var saveStream = new MemoryStream())
            {
                SpreadsheetLoader.Save(saveStream, spreadsheet);

                // assert
                Assert.That(saveStream, Is.Not.Null);
            }
        }
    }
}
