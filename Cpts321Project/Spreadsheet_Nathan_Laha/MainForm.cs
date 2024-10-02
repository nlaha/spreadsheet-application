// <copyright file="MainForm.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;
    using SpreadsheetEngine;

    /// <summary>
    /// The main form class for the main window
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The private spreadsheet instance
        /// </summary>
        private Spreadsheet _spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.InitializeDataGrid(this.dataGrid);
            this._spreadsheet = new Spreadsheet(Constants.NUMCOLUMNS, Constants.NUMROWS);

            this._spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
        }

        /// <summary>
        /// Double buffering for data grids
        /// See: https://stackoverflow.com/a/44188565/24202835
        /// Makes rendering much faster!
        /// </summary>
        /// <param name="ctl">The control</param>
        /// <param name="doubleBuffered">True to make the control double buffered</param>
        private static void SetDoubleBuffer(Control ctl, bool doubleBuffered)
        {
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                ctl,
                new object[] { doubleBuffered });
        }

        /// <summary>
        /// Called when something changes in a cell and we need to update it in the UI
        /// </summary>
        /// <param name="sender">the cell</param>
        /// <param name="e">args</param>
        /// <exception cref="ArgumentNullException">Thrown when the sender cell is null</exception>
        private void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? cell = sender as Cell;
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            this.dataGrid.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
        }

        /// <summary>
        /// Initializes the UI data grid with columns and rows
        /// </summary>
        /// <param name="dataGrid">the data grid to initialize</param>
        private void InitializeDataGrid(DataGridView dataGrid)
        {
            // initialize columns
            foreach (var letter in Constants.COLUMNS.ToCharArray())
            {
                dataGrid.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = letter.ToString(),
                    CellTemplate = new DataGridViewTextBoxCell(),
                });
            }

            // initialize rows
            dataGrid.Rows.Add(Constants.NUMROWS - 1);

            // number rows
            foreach (DataGridViewRow row in this.dataGrid.Rows)
            {
                row.HeaderCell.Value = $"{row.Index + 1}";
            }

            SetDoubleBuffer(this.dataGrid, true);

            // make sure numbers are all visible
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        /// <summary>
        /// Called when the demo button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">args</param>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            this._spreadsheet.PerformDemo();
        }
    }
}
