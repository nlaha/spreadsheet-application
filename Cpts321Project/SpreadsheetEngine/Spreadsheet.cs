// <copyright file="Spreadsheet.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.Text.RegularExpressions;

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
        /// Performs the demo for HW4
        /// </summary>
        public void PerformDemo()
        {
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                this.cells[
                    random.Next(0, this.ColumnCount - 1),
                    random.Next(0, this.RowCount - 1)].Text = "Hello World!";
            }

            for (int i = 0; i < this.RowCount; i++)
            {
                this.cells[1, i].Text = $"This is cell B{i}";
                this.cells[0, i].Text = $"=B{i}";
            }
        }

        /// <summary>
        /// Gets the cell at the specified location
        /// </summary>
        /// <param name="columnIndex">the column index of the cell</param>
        /// <param name="rowIndex">the row index of the cell</param>
        /// <returns>the cell</returns>
        public Cell? GetCell(int columnIndex, int rowIndex)
        {
            // out of range check
            if (columnIndex > this.cells.GetLength(0) ||
                rowIndex > this.cells.GetLength(1) ||
                columnIndex < 0 ||
                rowIndex < 0)
            {
                return null;
            }

            return this.cells[columnIndex, rowIndex];
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
                    this.EvaluateCellFormula(cell);
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

        /// <summary>
        /// Evaulates the formula in the specified cell and sets the cell's value
        /// </summary>
        /// <param name="cell">the cell</param>
        /// <returns>False for errors, true if completed</returns>
        private bool EvaluateCellFormula(Cell cell)
        {
            bool result = true;

            // simple cell value fetch formula

            // match on cell names
            Regex regex = new Regex(@"[A-Z]\d*");
            bool cellNameFound = regex.IsMatch(cell.Text);
            if (!cellNameFound)
            {
                result = false;
            }

            // get cell from name found in formula
            string cellName = regex.Match(cell.Text).Groups[0].Value;
            Cell? refCell = this.GetCellByName(cellName);
            if (refCell == null)
            {
                result = false;
            }

            if (result == false)
            {
                cell.Value = "ERR";
            }
            else
            {
                cell.Value = refCell!.Text;
            }

            return result;
        }

        /// <summary>
        /// Gets a cell by it's name, i.e A1 or B23
        /// </summary>
        /// <param name="cellName">the cell's name string</param>
        /// <returns>the cell, or null if not found</returns>
        private Cell? GetCellByName(string cellName)
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
            Cell? refCell = this.GetCell(this.ColumnToIndex(cellName[0]), rowIdx);
            return refCell;
        }
    }
}
