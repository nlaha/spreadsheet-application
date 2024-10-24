// <copyright file="TextCell.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// A cell holding normal text
    /// </summary>
    public class TextCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextCell"/> class.
        /// </summary>
        /// <param name="columnIndex">the column index</param>
        /// <param name="rowIndex">the row index</param>
        /// <param name="text">the text for the cell</param>
        public TextCell(int columnIndex, int rowIndex, string text)
            : base(columnIndex, rowIndex)
        {
            this.Text = text;
        }

        /// <inheritdoc/>
        public override string Value
        {
            get => this.Text;
        }
    }
}
