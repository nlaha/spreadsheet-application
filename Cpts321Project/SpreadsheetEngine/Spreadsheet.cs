// <copyright file="Spreadsheet.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using SpreadsheetEngine.Commands;
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Represents a container of cells and the cell factory
    /// </summary>
    [XmlRoot("Spreadsheet", Namespace = "https://nlaha.com", IsNullable = false)]
    public class Spreadsheet : IDisposable
    {
        /// <summary>
        /// 2D array of cells in the spreadsheet
        /// </summary>
        private Cell[,] _cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        public Spreadsheet()
        {
            this.UndoRedoCollection = new UndoRedoCollection();
            this._cells = new Cell[Constants.NUMCOLUMNS, Constants.NUMROWS];

            for (int y = 0; y < Constants.NUMROWS; y++)
            {
                for (int x = 0; x < Constants.NUMCOLUMNS; x++)
                {
                    this._cells[x, y] = new Cell(x, y);

                    // subscribe to change events
                    this._cells[x, y].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Fired when any cell or cells change
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged;

        /// <summary>
        /// Gets the collection of undo/redo commands
        /// </summary>
        [XmlIgnore]
        public UndoRedoCollection UndoRedoCollection { get; }

        /// <summary>
        /// Gets or sets the cells in the spreadsheet
        /// The getter returns only the cells that were modified
        /// </summary>
        [XmlArray("Cells")]
        public Cell[] Cells
        {
            get
            {
                // flatten the cells array from 2D to 1D as
                // we can't
                return this._cells.Cast<Cell>().Where(x => x.WasModified).ToArray();
            }

            set
            {
                foreach (Cell cell in value)
                {
                    cell.WasModified = true;
                    cell.PropertyChanged += this.OnCellPropertyChanged;
                    this._cells[cell.ColumnIndex, cell.RowIndex] = cell;
                }
            }
        }

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        [XmlAttribute("ColumnCount")]
        public int ColumnCount { get => this._cells.GetLength(0); }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        [XmlAttribute("RowCount")]
        public int RowCount { get => this._cells.GetLength(1); }

        /// <summary>
        /// Gets the cell at the specified location
        /// </summary>
        /// <param name="columnIndex">the column index of the cell</param>
        /// <param name="rowIndex">the row index of the cell</param>
        /// <returns>the cell</returns>
        public Cell GetCell(int columnIndex, int rowIndex)
        {
            // out of range check
            if (columnIndex > this._cells.GetLength(0) ||
                rowIndex > this._cells.GetLength(1) ||
                columnIndex < 0 ||
                rowIndex < 0)
            {
                throw new IndexOutOfRangeException("Cell index out of range");
            }

            return this._cells[columnIndex, rowIndex];
        }

        /// <summary>
        /// Sets the value of a cell
        /// </summary>
        /// <param name="columnIndex">the column index</param>
        /// <param name="rowIndex">the row index</param>
        /// <param name="value">the value to set</param>
        public void SetCellValue(int columnIndex, int rowIndex, string value)
        {
            Cell? cell = this.GetCell(columnIndex, rowIndex);
            if (cell != null)
            {
                cell.Text = value;
            }
        }

        /// <summary>
        /// Sets the text of a cell
        /// </summary>
        /// <param name="columnIndex">the column index</param>
        /// <param name="rowIndex">the row index</param>
        /// <param name="value">the value to set</param>
        public void SetCellText(int columnIndex, int rowIndex, string value)
        {
            Cell? cell = this.GetCell(columnIndex, rowIndex);
            if (cell != null)
            {
                cell.Text = value;
            }
        }

        /// <summary>
        /// Gets a cell by it's name, i.e A1 or B23
        /// </summary>
        /// <param name="cellName">the cell's name string</param>
        /// <returns>the cell</returns>
        /// <exception cref="NullReferenceException">thrown when the cell name is invalid</exception>
        public Cell GetCellByName(string cellName)
        {
            // skip the column
            string? rowIdxString = cellName[1..];

            // parse the row index
            int rowIdx = 0;
            bool rowParseSuccess = int.TryParse(rowIdxString, out rowIdx);
            if (!rowParseSuccess)
            {
                throw new NullReferenceException($"Could not parse row index from {cellName}");
            }

            // get cell
            return this.GetCell(this.ColumnToIndex(cellName[0]), rowIdx - 1);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // unsubscribe all subscribers
            Delegate[] cellPropertySubscribers = this.CellPropertyChanged?.GetInvocationList() ?? [];
            foreach (Delegate subscriber in cellPropertySubscribers)
            {
                this.CellPropertyChanged -= (PropertyChangedEventHandler)subscriber;
            }

            this.UndoRedoCollection.Dispose();
        }

        /// <summary>
        /// Gets a column index from a column character, i.e. 'A' -> 0
        /// </summary>
        /// <param name="columnName">the column name</param>
        /// <returns>the column index</returns>
        private int ColumnToIndex(char columnName)
        {
            return Constants.COLUMNS.IndexOf(columnName);
        }

        /// <summary>
        /// Fired when a cell changes
        /// </summary>
        /// <param name="sender">the cell</param>
        /// <param name="e">the args</param>
        /// <exception cref="ArgumentNullException">thrown when cell is null</exception>
        private void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? cell = sender as Cell;
            if (cell != null)
            {
                // check if cell has a formula
                if (cell.Text.StartsWith('='))
                {
                    // recompute the formula
                    try
                    {
                        cell.ExpressionTree = new ExpressionTree.ExpressionTree(cell, this);
                        cell.ErrorText = string.Empty;
                    }
                    catch (InvalidExpressionTreeException ex)
                    {
                        cell.ErrorText = ex.Message;
                        cell.Value = "ERR!";
                    }
                }
                else
                {
                    cell.Value = cell.Text;
                    cell.ErrorText = string.Empty;
                    cell.ExpressionTree = null;
                }

                // invoke the property changed event
                // this should trigger an update in the UI
                this.CellPropertyChanged?.Invoke(cell, e);
            }
            else
            {
                throw new ArgumentNullException(nameof(cell));
            }
        }
    }
}
