// <copyright file="Cell.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a cell in the spreadsheet
    /// </summary>
    public class Cell : INotifyPropertyChanged
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
        /// Protected field for the background color of the cell
        /// in RGBA notation
        /// </summary>
        protected uint _bgColor;

        /// <summary>
        /// Set of cells this expression references
        /// </summary>
        private readonly HashSet<Cell> _referencedCells;

        /// <summary>
        /// Optional expression tree for the cell
        /// </summary>
        private ExpressionTree.ExpressionTree? _expressionTree;

        /// <summary>
        /// Private field indicating whether the cell was modified
        /// </summary>
        private bool _wasModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        public Cell()
        {
            this._text = string.Empty;
            this._value = string.Empty;
            this._bgColor = 0xFFFFFFFF;
            this._expressionTree = null;
            this._referencedCells = new HashSet<Cell>();
        }

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
            this._bgColor = 0xFFFFFFFF;
            this._expressionTree = null;
            this._referencedCells = new HashSet<Cell>();
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets error text to display error messages in the UI
        /// </summary>
        [XmlIgnore]
        public string ErrorText { get; set; } = string.Empty;

        /// <summary>
        /// Gets the set of cells this cell references
        /// </summary>
        [XmlIgnore]
        public HashSet<Cell> ReferencedCells
        {
            get => this._referencedCells;
        }

        /// <summary>
        /// Gets or sets the background color of the cell in RGBA notation
        /// </summary>
        [XmlAttribute]
        public uint BGColor
        {
            get => this._bgColor;
            set
            {
                if (this._bgColor != value)
                {
                    this._bgColor = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cell was modified in any way
        /// </summary>
        [XmlIgnore]
        public bool WasModified
        {
            get => this._wasModified;
            set => this._wasModified = value;
        }

        /// <summary>
        /// Gets or sets the row index of the cell in the spreadsheet
        /// </summary>
        [XmlAttribute]
        public int RowIndex { get; set; }

        /// <summary>
        /// Gets or sets the column index of the cell in the spreadsheet
        /// </summary>
        [XmlAttribute]
        public int ColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets the expression tree for the cell
        /// </summary>
        [XmlIgnore]
        public ExpressionTree.ExpressionTree? ExpressionTree
        {
            get => this._expressionTree;
            set
            {
                if (this._expressionTree != null)
                {
                    this._expressionTree.Dispose();
                    this._expressionTree = null;
                }

                // re-evaluate referenced cells before clearing
                var references = new HashSet<Cell>(this.ReferencedCells);
                this.ReferencedCells.Clear();
                foreach (Cell cell in references)
                {
                    if (cell.ExpressionTree != null)
                    {
                        cell.NotifyPropertyChanged();
                    }
                }

                this._expressionTree = value;

                if (this._expressionTree != null)
                {
                    this.Value = this._expressionTree.Evaluate().ToString();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text property of the cell
        /// </summary>
        [XmlElement("Text")]
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
        [XmlElement("Value")]
        public virtual string Value
        {
            get => this._value;
            set
            {
                this._value = value;
            }
        }

        /// <summary>
        /// Helper function to invoke the property changed event
        /// </summary>
        /// <param name="propertyName">an optional property name</param>
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this._wasModified = true;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Recursively checks if this cell or any of its referenced cells
        /// have references to the specified cell
        /// </summary>
        /// <param name="cell">the cell to check references to</param>
        /// <returns>true if theres a circular reference</returns>
        public bool HasCircularReference(Cell cell)
        {
            if (cell == this)
            {
                return true;
            }

            foreach (Cell referencedCell in this._referencedCells)
            {
                if (referencedCell == cell)
                {
                    return true;
                }

                if (referencedCell.HasCircularReference(cell))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
