// <copyright file="NodeBinaryOperator.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    /// <summary>
    /// Represents a binary operator node in an expression tree
    /// </summary>
    internal abstract class NodeBinaryOperator : Node
    {
        /// <summary>
        /// Gets the precedence of the operator
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// Gets the associativity of the operator
        /// </summary>
        public abstract EAssociativity Associativity { get; }

        /// <summary>
        /// Gets or sets the left child
        /// </summary>
        public Node? Left { get; set; } = null;

        /// <summary>
        /// Gets or sets the right child
        /// </summary>
        public Node? Right { get; set; } = null;
    }
}