// <copyright file="Spreadsheet.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Represents a container of cells and the cell factory
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D array of cells in the spreadsheet
        /// </summary>
        private readonly Cell[,] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="numColumns">the number of columns</param>
        /// <param name="numRows">the number of rows</param>
        public Spreadsheet(int numColumns, int numRows)
        {
            this.cells = new Cell[numColumns, numRows];

            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numColumns; x++)
                {
                    this.cells[x, y] = new TextCell(x, y);

                    // subscribe to change events
                    this.cells[x, y].PropertyChanged += this.OnCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Fired when any cell or cells change
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged;

        /// <summary>
        /// Gets the number of columns
        /// </summary>
        public int ColumnCount { get => this.cells.GetLength(0); }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        public int RowCount { get => this.cells.GetLength(1); }

        /// <summary>
        /// Gets the cell at the specified location
        /// </summary>
        /// <param name="columnIndex">the column index of the cell</param>
        /// <param name="rowIndex">the row index of the cell</param>
        /// <returns>the cell</returns>
        public Cell GetCell(int columnIndex, int rowIndex)
        {
            // out of range check
            if (columnIndex > this.cells.GetLength(0) ||
                rowIndex > this.cells.GetLength(1) ||
                columnIndex < 0 ||
                rowIndex < 0)
            {
                throw new IndexOutOfRangeException("Cell index out of range");
            }

            return this.cells[columnIndex, rowIndex];
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
        /// Gets a cell by it's name, i.e A1 or B23
        /// </summary>
        /// <param name="cellName">the cell's name string</param>
        /// <returns>the cell, or null if not found</returns>
        public Cell? GetCellByName(string cellName)
        {
            // skip the column
            string? rowIdxString = cellName[1..];

            // parse the row index
            int rowIdx = 0;
            bool rowParseSuccess = int.TryParse(rowIdxString, out rowIdx);
            if (!rowParseSuccess)
            {
                return null;
            }

            // get cell
            Cell? refCell = this.GetCell(this.ColumnToIndex(cellName[0]), rowIdx - 1);
            return refCell;
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
                // cell doesn't have a formula
                if (!cell.Text.StartsWith('='))
                {
                    cell.Value = cell.Text;
                }
                else
                {
                    var expressionTree = new ExpressionTree.ExpressionTree(cell.Text[1..], this);
                    cell.Value = expressionTree.Evaluate().ToString();
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
