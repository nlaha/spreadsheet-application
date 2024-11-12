// <copyright file="CellChangeCommandTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine;
    using SpreadsheetEngine.Commands;

    /// <summary>
    /// Tests the cell change command
    /// </summary>
    internal class CellChangeCommandTests
    {
        /// <summary>
        /// Tests the cell change command
        /// </summary>
        [Test]
        public void CellChangeCommand_ChangesProperty()
        {
            var spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);
            var cell = spreadsheet.GetCell(0, 0);
            var command = new CellChangeCommand(spreadsheet, cell.RowIndex, cell.ColumnIndex, "Text", "Hello World!");

            command.Execute();

            Assert.That(cell.Text, Is.EqualTo("Hello World!"));
        }

        /// <summary>
        /// Tests the cell change command with an invalid type
        /// </summary>
        [Test]
        public void CellChangeCommand_HandlesInvalidType()
        {
            var spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);
            var cell = spreadsheet.GetCell(0, 0);
            var command = new CellChangeCommand(spreadsheet, cell.RowIndex, cell.ColumnIndex, "Text", 123);

            Assert.Throws<System.ArgumentException>(() => command.Execute());
        }
    }
}
