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
        [XmlAttribute("Text")]
        [DefaultValueAttribute("")]
        protected string _text;

        /// <summary>
        /// Protected field for the cell value
        /// </summary>
        [XmlAttribute("Value")]
        [DefaultValueAttribute("")]
        protected string _value;

        /// <summary>
        /// Protected field for the background color of the cell
        /// in RGBA notation
        /// </summary>
        [XmlAttribute("BGColor")]
        [DefaultValueAttribute(0xFFFFFFFF)]
        protected uint _bgColor;

        /// <summary>
        /// Optional expression tree for the cell
        /// </summary>
        private ExpressionTree.ExpressionTree? _expressionTree;

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
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the background color of the cell in RGBA notation
        /// </summary>
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
        /// Gets the row index of the cell in the spreadsheet
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets the column index of the cell in the spreadsheet
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Gets or sets the expression tree for the cell
        /// </summary>
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
        public virtual string Value
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
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
