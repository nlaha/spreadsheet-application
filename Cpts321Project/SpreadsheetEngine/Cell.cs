// <copyright file="Cell.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents a cell in the spreadsheet
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Protected field for the cell text
        /// </summary>
        protected string _text;

        /// <summary>
        /// Protected field for the cell value
        /// </summary>
        protected string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">the row index of the cell</param>
        /// <param name="columnIndex">the column index of the cell</param>
        public Cell(int columnIndex, int rowIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this._text = string.Empty;
            this._value = string.Empty;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the row index of the cell in the spreadsheet
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets the column index of the cell in the spreadsheet
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Gets or sets the text property of the cell
        /// </summary>
        public string Text
        {
            get => this._text;
            set
            {
                if (this._text != value)
                {
                    this._text = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the computed value of the cell
        /// </summary>
        public string Value
        {
            get => this._value;
            protected internal set
            {
                this._value = value;
            }
        }

        /// <summary>
        /// Helper function to invoke the property changed event
        /// </summary>
        /// <param name="propertyName">an optional property name</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
