// <copyright file="CellChangeCommand.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Commands
{
    using System.Reflection;

    /// <summary>
    /// Command for when we want to change the properties of a cell
    /// </summary>
    public class CellChangeCommand : ICommand
    {
        /// <summary>
        /// The target cell
        /// </summary>
        private readonly Cell _target;

        /// <summary>
        /// The name of the property to change
        /// </summary>
        private readonly string _propertyName;

        /// <summary>
        /// The new value of the property
        /// </summary>
        private readonly object? _newValue;

        /// <summary>
        /// The old value of the property
        /// </summary>
        private object? _oldValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeCommand"/> class.
        /// </summary>
        /// <param name="target">the target cell</param>
        /// <param name="propertyName">the property to change</param>
        /// <param name="newValue">the new property value</param>
        public CellChangeCommand(Cell target, string propertyName, object newValue)
        {
            this._target = target;

            this._propertyName = propertyName;
            this._newValue = newValue;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            // use reflection to modify the property on the cell
            PropertyInfo? property = this._target.GetType()?.GetProperty(this._propertyName);
            if (property == null)
            {
                throw new ArgumentNullException(nameof(this._propertyName), "Property does not exist on the target object.");
            }

            this._oldValue = property.GetValue(this._target);
            property.SetValue(this._target, this._newValue);
        }

        /// <inheritdoc/>
        public string GetName()
        {
            return $"changing {this._propertyName} on cell";
        }

        /// <inheritdoc/>
        public void Undo()
        {
            // use reflection to revert the property value back to the old value
            PropertyInfo? property = this._target.GetType()?.GetProperty(this._propertyName);
            if (property == null)
            {
                throw new ArgumentNullException(nameof(this._propertyName), "Property does not exist on the target object.");
            }

            // TODO: cell is a difference object reference when it gets recreated by the spreadsheet for expressions
            property.SetValue(this._target, this._oldValue);
        }
    }
}
