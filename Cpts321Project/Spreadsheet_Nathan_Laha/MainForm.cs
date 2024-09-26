// <copyright file="MainForm.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha
{
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// The main form class for the main window
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constant storing a string with each letter of the alphabet.
        /// Used for column names.
        /// </summary>
        private const string LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
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
        /// Initializes the UI data grid with columns and rows
        /// </summary>
        private void InitializeDataGrid()
        {
            // initialize columns
            foreach (var letter in LETTERS.ToCharArray())
            {
                this.dataGrid.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = letter.ToString(),
                    CellTemplate = new DataGridViewTextBoxCell(),
                });
            }

            // initialize rows
            this.dataGrid.Rows.Add(50);

            // number rows
            foreach (DataGridViewRow row in this.dataGrid.Rows)
            {
                row.HeaderCell.Value = $"{row.Index + 1}";
            }

            SetDoubleBuffer(this.dataGrid, true);

            // make sure numbers are all visible
            this.dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }
    }
}
