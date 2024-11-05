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
            var cell = new TextCell(0, 0, string.Empty);
            var command = new CellChangeCommand(cell, "Text", "New Text");

            command.Execute();

            Assert.That(cell.Text, Is.EqualTo("New Text"));
        }

        /// <summary>
        /// Tests the cell change command with an invalid type
        /// </summary>
        [Test]
        public void CellChangeCommand_HandlesInvalidType()
        {
            var cell = new TextCell(0, 0, string.Empty);
            var command = new CellChangeCommand(cell, "Text", 123);

            command.Execute();

            Assert.That(cell.Text, Is.EqualTo("123"));
        }
    }
}
