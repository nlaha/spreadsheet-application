// <copyright file="UndoRedoCollection.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using SpreadsheetEngine.Commands;

    /// <summary>
    /// Holds the undo and redo history for the spreadsheet
    /// </summary>
    internal class UndoRedoCollection
    {
        /// <summary>
        /// Undo command stack
        /// </summary>
        private readonly Stack<ICommand> _undoStack = new ();

        /// <summary>
        /// Redo command stack
        /// </summary>
        private readonly Stack<ICommand> _redoStack = new ();

        /// <summary>
        /// Undo the last command
        /// </summary>
        public void Undo()
        {
            if (this._undoStack.Count == 0)
            {
                return;
            }

            var undoCommand = this._undoStack.Pop();
            this._redoStack.Push(undoCommand);

            undoCommand.Undo();
        }

        /// <summary>
        /// Redo the last command
        /// </summary>
        public void Redo()
        {
            if (this._redoStack.Count == 0)
            {
                return;
            }

            var redoCommand = this._redoStack.Pop();
            this._undoStack.Push(redoCommand);

            redoCommand.Execute();
        }

        /// <summary>
        /// Adds a command to the undo history
        /// </summary>
        /// <param name="command">the command to add</param>
        public void AddCommand(ICommand command)
        {
            this._undoStack.Push(command);
        }
    }
}
