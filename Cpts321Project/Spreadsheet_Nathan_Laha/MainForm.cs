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
    using SpreadsheetEngine.Commands;
    using SpreadsheetEngine.Exceptions;

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

            this._spreadsheet = new Spreadsheet();
            this._spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
            this._spreadsheet.UndoRedoCollection.CurrentUndoRedoNamesChanged += this.UpdateUndoRedoNames;
            this.UpdateUndoRedoNames(null, null);
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
        /// Sets a new spreadsheet and cleans up the old one
        /// </summary>
        /// <param name="spreadsheet">the new spreadsheet</param>
        private void SetNewSpreadsheet(Spreadsheet spreadsheet)
        {
            // subscribe to events in the new spreadsheet
            spreadsheet.CellPropertyChanged += this.OnCellPropertyChanged;
            spreadsheet.UndoRedoCollection.CurrentUndoRedoNamesChanged += this.UpdateUndoRedoNames;

            // now that we're subscribed, we can call the cell property changed method
            // on each modified cell
            foreach (Cell cell in spreadsheet.Cells)
            {
                cell.NotifyPropertyChanged();
            }

            // clean up the existing spreadsheet
            this._spreadsheet.Dispose();

            this._spreadsheet = spreadsheet;
            this.UpdateUndoRedoNames(null, null);
        }

        /// <summary>
        /// Gets a cell in the data grid
        /// </summary>
        /// <param name="columnIndex">the column index</param>
        /// <param name="rowIndex">the row index</param>
        /// <returns>the cell</returns>
        private DataGridViewCell GetDataGridCell(int columnIndex, int rowIndex)
        {
            // range check
            if (columnIndex < 0 || columnIndex >= this.dataGrid.ColumnCount ||
                rowIndex < 0 || rowIndex >= this.dataGrid.RowCount)
            {
                throw new NullReferenceException($"Could not find cell at {columnIndex}, {rowIndex}");
            }

            return this.dataGrid.Rows[rowIndex].Cells[columnIndex];
        }

        /// <summary>
        /// Clears the data grid and reinitializes all cells
        /// </summary>
        private void ClearDataGrid()
        {
            // clear the data grid by constructing new cells
            for (int c = 0; c < this.dataGrid.ColumnCount; c++)
            {
                for (int r = 0; r < this.dataGrid.RowCount; r++)
                {
                    this.dataGrid.Rows[r].Cells[c] = new DataGridViewTextBoxCell();
                }
            }
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

            var dataGridCell = this.GetDataGridCell(cell.ColumnIndex, cell.RowIndex);

            // we're not changing the background color so update the value
            dataGridCell.Value = cell.Value;
            dataGridCell.ErrorText = string.Empty;

            // update cell background color when it changes
            var color = System.Drawing.Color.FromArgb((int)cell.BGColor);
            dataGridCell.Style.BackColor = color;
        }

        /// <summary>
        /// Callback for when a cell is put into editing mode by the user
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the args</param>
        private void OnCellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            Cell? cell = this._spreadsheet.GetCell(e.ColumnIndex, e.RowIndex);
            var dataGridCell = this.GetDataGridCell(e.ColumnIndex, e.RowIndex);

            // set display to text view mode
            dataGridCell.Value = cell?.Text ?? string.Empty;
        }

        /// <summary>
        /// Callback for when a cell is no longer in editing mode
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the args</param>
        private void OnCellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            Cell? cell = this._spreadsheet.GetCell(e.ColumnIndex, e.RowIndex);
            var dataGridCell = this.GetDataGridCell(e.ColumnIndex, e.RowIndex);

            // update cell text
            var command = new CellChangeCommand(cell, "Text", dataGridCell.Value?.ToString() ?? string.Empty);
            this.ExecuteCommand(command);

            dataGridCell.ErrorText = string.Empty;

            // force a property change event, this way if we don't change the text at all
            // we still get back into value display mode
            cell!.NotifyPropertyChanged();
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
        /// Callback for the cell background color button
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void BackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.colorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (DataGridViewCell dataGridCell in this.dataGrid.SelectedCells)
                {
                    Cell cell = this._spreadsheet.GetCell(dataGridCell.ColumnIndex, dataGridCell.RowIndex);

                    var command = new CellChangeCommand(cell, "BGColor", (uint)this.colorDialog.Color.ToArgb());
                    this.ExecuteCommand(command);
                }
            }
        }

        /// <summary>
        /// Updates the text of the undo/redo buttons
        /// </summary>
        /// <param name="undoName">what will be undone</param>
        /// <param name="redoName">what will be re-done</param>
        private void UpdateUndoRedoNames(string? undoName, string? redoName)
        {
            this.undoToolStripMenuItem.Text = $"Undo {undoName ?? string.Empty}";
            this.redoToolStripMenuItem.Text = $"Redo {redoName ?? string.Empty}";

            this.undoToolStripMenuItem.Enabled = undoName != null;
            this.redoToolStripMenuItem.Enabled = redoName != null;
        }

        /// <summary>
        /// Adds a command to the spreadsheet's undo history
        /// and executes it
        /// </summary>
        /// <param name="command">the command</param>
        private void ExecuteCommand(ICommand command)
        {
            command.Execute();
            this._spreadsheet.UndoRedoCollection.AddCommand(command);
        }

        /// <summary>
        /// Called when the undo button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._spreadsheet.UndoRedoCollection.Undo();
        }

        /// <summary>
        /// Called when the redo button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._spreadsheet.UndoRedoCollection.Redo();
        }

        /// <summary>
        /// Called when the save button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream fs = new FileStream(this.saveFileDialog.FileName, FileMode.Create);
                try
                {
                    SpreadsheetLoader.Save(fs, this._spreadsheet);
                }
                catch (IOException exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Called when the load button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // clear the UI
                this.ClearDataGrid();

                FileStream fs = new FileStream(this.openFileDialog.FileName, FileMode.Open);
                try
                {
                    this.SetNewSpreadsheet(SpreadsheetLoader.Load(fs));
                }
                catch (IOException exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    fs.Close();
                }
            }
        }
    }
}
