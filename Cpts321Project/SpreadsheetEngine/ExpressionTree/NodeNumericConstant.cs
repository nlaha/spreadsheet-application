// <copyright file="NodeNumericConstant.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using SpreadsheetEngine.ExpressionTree.Interfaces;

    /// <summary>
    /// Represents an expression tree numeric constant node
    /// </summary>
    internal class NodeNumericConstant : Node, IMatchableExpressionNode
    {
        /// <summary>
        /// The numeric constant value
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeNumericConstant"/> class.
        /// </summary>
        /// <param name="value">the constant value</param>
        public NodeNumericConstant(string value)
        {
            this._value = double.Parse(value);
        }

        /// <summary>
        /// Gets node regex
        /// </summary>
        public static string NodeRegex => @"^\d+(\.+\d+)*";

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            return this._value;
        }
    }
}
