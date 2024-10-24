// <copyright file="ExpressionCell.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine
{
    using SpreadsheetEngine.Exceptions;

    /// <summary>
    /// A cell holding normal text
    /// </summary>
    public class ExpressionCell : Cell
    {
        /// <summary>
        /// The expression tree contained in the cell
        /// </summary>
        private ExpressionTree.ExpressionTree _expressionTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionCell"/> class.
        /// </summary>
        /// <param name="columnIndex">the column index</param>
        /// <param name="rowIndex">the row index</param>
        /// <param name="expression">the cell expression</param>
        /// <param name="spreadsheet">the spreadsheet this cell is in</param>
        public ExpressionCell(int columnIndex, int rowIndex, string expression, Spreadsheet spreadsheet)
            : base(columnIndex, rowIndex)
        {
            this.Text = expression;
            this._expressionTree = new ExpressionTree.ExpressionTree(this, spreadsheet);
        }

        /// <inheritdoc/>
        public override string Value
        {
            get => this._expressionTree.Evaluate().ToString();
        }
    }
}
