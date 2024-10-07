// <copyright file="NodeBinaryOperator.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.ComponentModel;
    using SpreadsheetEngine.Exceptions;

    /// <summary>
    /// Represents a binary operator, i.e. + or -
    /// </summary>
    public enum BinaryOperator
    {
        /// <summary>
        /// Addition, +
        /// </summary>
        [Description("+")]
        Add,

        /// <summary>
        /// Subtraction, -
        /// </summary>
        [Description("-")]
        Subtract,
    }

    /// <summary>
    /// Represents an expression tree binary operator node
    /// </summary>
    internal class NodeBinaryOperator : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeBinaryOperator"/> class.
        /// </summary>
        /// <param name="lhsChild">the left hand child</param>
        /// <param name="rhsChild">the right hand child</param>
        /// <param name="op">the operation to perform when evaluated</param>
        public NodeBinaryOperator(Node lhsChild, Node rhsChild, BinaryOperator op)
        {
            this.LhsChild = lhsChild;
            this.RhsChild = rhsChild;
            this.Operator = op;
        }

        /// <summary>
        /// Gets left hand child
        /// </summary>
        public Node LhsChild { get; }

        /// <summary>
        /// Gets right hand child
        /// </summary>
        public Node RhsChild { get; }

        /// <summary>
        /// Gets the operation to perform when evaluated
        /// </summary>
        public BinaryOperator Operator { get; }

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            return this.Operator switch
            {
                BinaryOperator.Add => this.LhsChild.Evaluate() + this.RhsChild.Evaluate(),
                BinaryOperator.Subtract => this.LhsChild.Evaluate() - this.RhsChild.Evaluate(),
                _ => throw new InvalidExpressionTreeException("Invalid operator in expression!"),
            };
        }
    }
}
