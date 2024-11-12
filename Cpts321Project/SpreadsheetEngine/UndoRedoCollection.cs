// <copyright file="UndoRedoCollection.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using SpreadsheetEngine.Commands;

    /// <summary>
    /// Holds the undo and redo history for the spreadsheet
    /// </summary>
    public class UndoRedoCollection
    {
        private readonly Spreadsheet _spreadsheet;

        /// <summary>
        /// Undo command stack
        /// </summary>
        private readonly Stack<ICommand> _undoStack = new ();

        /// <summary>
        /// Redo command stack
        /// </summary>
        private readonly Stack<ICommand> _redoStack = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCollection"/> class.
        /// </summary>
        /// <param name="spreadsheet">the spreadsheet</param>
        public UndoRedoCollection(Spreadsheet spreadsheet)
        {
            this._spreadsheet = spreadsheet;
        }

        /// <summary>
        /// The delegate for when there are new names for the undo/redo operations
        /// </summary>
        /// <param name="undoName">the new undo name</param>
        /// <param name="redoName">the new redo name</param>
        public delegate void UndoRedoNamesChangedEventHandler(string? undoName, string? redoName);

        /// <summary>
        /// Event fired when the undo or redo stacks change
        /// </summary>
        public event UndoRedoNamesChangedEventHandler? CurrentUndoRedoNamesChanged;

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

            this.SendNewUndoRedoNames();
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

            this.SendNewUndoRedoNames();
        }

        /// <summary>
        /// Adds a command to the undo history
        /// </summary>
        /// <param name="command">the command to add</param>
        public void AddCommand(ICommand command)
        {
            this._undoStack.Push(command);

            this.SendNewUndoRedoNames();
        }

        /// <summary>
        /// Sends new undo and redo names to listeners
        /// </summary>
        private void SendNewUndoRedoNames()
        {
            if (this.CurrentUndoRedoNamesChanged == null)
            {
                return;
            }

            ICommand? undo = null;
            this._undoStack.TryPeek(out undo);
            ICommand? redo = null;
            this._redoStack.TryPeek(out redo);

            this.CurrentUndoRedoNamesChanged.Invoke(undo?.GetName(), redo?.GetName());
        }
    }
}
