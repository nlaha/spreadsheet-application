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
        private UndoRedoCollection _collection = new ();

        /// <summary>
        /// Tests the undo operation
        /// </summary>
        [Test]
        public void UndoRedoCollection_Undo_ModifiesStack()
        {
            // arrange
            var cell = new TextCell(0, 0, string.Empty);
            this._collection.AddCommand(new CellChangeCommand(cell, "Text", "Hello World!"));

            // act
            this._collection.Undo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "redoStack");

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
            var cell = new TextCell(0, 0, string.Empty);
            this._collection.AddCommand(new CellChangeCommand(cell, "Text", "Hello World!"));
            this._collection.Undo();

            // act
            this._collection.Redo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "redoStack");

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
            this._collection = new ();

            // act
            this._collection.Undo();

            // assert
            Stack<ICommand>? undoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "undoStack");
            Stack<ICommand>? redoStack = (Stack<ICommand>?)TestHelpers.GetProperty(this._collection, "redoStack");

            Assert.That(undoStack, Is.Not.Null);
            Assert.That(redoStack, Is.Not.Null);
            Assert.That(undoStack, Is.Empty);
            Assert.That(redoStack, Is.Empty);
        }
    }
}
