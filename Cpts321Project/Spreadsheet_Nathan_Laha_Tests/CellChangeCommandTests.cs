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
            var command = new CellChangeCommand(cell, "Text", "Hello World!");

            command.Execute();

            Assert.That(cell.Text, Is.EqualTo("Hello World!"));
        }

        /// <summary>
        /// Tests the cell change command with an invalid type
        /// </summary>
        [Test]
        public void CellChangeCommand_HandlesInvalidType()
        {
            var cell = new TextCell(0, 0, string.Empty);
            var newCell = new TextCell(0, 0, "Hello World!");
            var command = new CellChangeCommand(cell, "Text", 123);

            Assert.Throws<System.ArgumentException>(() => command.Execute());
        }
    }
}
