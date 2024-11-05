// <copyright file="RedoCommand.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Commands
{
    /// <summary>
    /// Command for when we want to change the properties of a cell
    /// </summary>
    internal class CellChangeCommand : ICommand
    {
        /// <summary>
        /// The cell instance to change
        /// </summary>
        private Cell _cell;

        /// <summary>
        /// The property name to change
        /// </summary>
        private string _propertyName;

        /// <summary>
        /// The value to set the property to
        /// </summary>
        private object? _propertyValue;

        /// <summary>
        /// The previous value of the property (for undoing)
        /// </summary>
        private object? _oldValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeCommand"/> class.
        /// </summary>
        /// <param name="cell">the cell to change</param>
        /// <param name="propertyName">the property to change</param>
        /// <param name="propertyValue">the property value</param>
        public CellChangeCommand(Cell cell, string propertyName, object? propertyValue)
        {
            this._cell = cell;
            this._propertyName = propertyName;
            this._propertyValue = propertyValue;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            // use reflection to change the property
            var property = this._cell.GetType().GetProperty(this._propertyName);
            this._oldValue = property?.GetValue(this._cell);
            property?.SetValue(this._cell, this._propertyValue);
        }

        /// <inheritdoc/>
        public void Undo()
        {
            // use reflection to change the property to the old value
            var property = this._cell.GetType().GetProperty(this._propertyName);
            property?.SetValue(this._cell, this._oldValue);
        }
    }
}
