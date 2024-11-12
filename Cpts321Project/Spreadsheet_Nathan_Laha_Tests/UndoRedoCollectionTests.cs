// <copyright file="UndoRedoCollectionTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine;
    using SpreadsheetEngine.Commands;

    /// <summary>
    /// Test the undo redo collection
    /// </summary>
    internal class UndoRedoCollectionTests
    {
        /// <summary>
        /// Instance for reflection
        /// </summary>
        private UndoRedoCollection _collection = new (new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS));

        /// <summary>
        /// Tests the undo operation
        /// </summary>
        [Test]
        public void UndoRedoCollection_Undo_ModifiesStack()
        {
            // arrange
            var spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);
            this._collection = new (spreadsheet);
            var cell = spreadsheet.GetCell(0, 0);
            var command = new CellChangeCommand(spreadsheet, cell.RowIndex, cell.ColumnIndex, "Text", "Hello World!");
            command.Execute();
            this._collection.AddCommand(command);

            // act
            this._collection.Undo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_redoStack");

            Assert.That(undoStack, Is.Not.Null);
            Assert.That(redoStack, Is.Not.Null);
            Assert.That(undoStack, Is.Empty);
            Assert.That(redoStack, Has.Count.EqualTo(1));
        }

        /// <summary>
        /// Tests the redo operation
        /// </summary>
        [Test]
        public void UndoRedoCollection_Redo_ModifiesStack()
        {
            // arrange
            var spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);
            this._collection = new (spreadsheet);
            var cell = spreadsheet.GetCell(0, 0);
            cell.Text = string.Empty;
            var command = new CellChangeCommand(spreadsheet, cell.RowIndex, cell.ColumnIndex, "Text", "Hello World!");
            command.Execute();
            this._collection.AddCommand(command);
            this._collection.Undo();

            // act
            this._collection.Redo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_redoStack");

            Assert.That(undoStack, Is.Not.Null);
            Assert.That(redoStack, Is.Not.Null);
            Assert.That(undoStack, Has.Count.EqualTo(1));
            Assert.That(redoStack, Is.Empty);
        }

        /// <summary>
        /// Tests the undo operation with an empty stack
        /// </summary>
        [Test]
        public void UndoRedoCollection_Undo_EmptyStack()
        {
            // arrange
            var spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);
            this._collection = new (spreadsheet);

            // act
            this._collection.Undo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetField(this._collection, "_redoStack");

            Assert.That(undoStack, Is.Not.Null);
            Assert.That(redoStack, Is.Not.Null);
            Assert.That(undoStack, Is.Empty);
            Assert.That(redoStack, Is.Empty);
        }
    }
}
