// <copyright file="NodeBinaryOperator.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.ComponentModel;
    using SpreadsheetEngine.Attributes;
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree.Interfaces;

    /// <summary>
    /// Represents a binary operator, i.e. + or -
    /// </summary>
    public enum BinaryOperator
    {
        /// <summary>
        /// Addition, +
        /// </summary>
        [OperatorPrecedence(0)]
        [OperatorSymbolAttribute("+")]
        Add,

        /// <summary>
        /// Subtraction, -
        /// </summary>
        [OperatorPrecedence(0)]
        [OperatorSymbolAttribute("-")]
        Subtract,
    }

    /// <summary>
    /// Represents an expression tree binary operator node
    /// </summary>
    internal class NodeBinaryOperator : Node, IMatchableExpressionNode, IOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeBinaryOperator"/> class.
        /// </summary>
        /// <param name="opString">The string representing the operator symbol, i.e. + or -</param>
        public NodeBinaryOperator(string opString)
        {
            this.Operator = OperatorSymbolAttribute.GetValueFromSymbol<BinaryOperator>(opString);
        }

        /// <summary>
        /// Gets node regex
        /// For this case, it's built dynamically from the <see cref="BinaryOperator"/> enum
        /// </summary>
        public static string NodeRegex
        {
            get
            {
                var operatorSymbols = AttributeHelpers.GetAttributes<OperatorSymbolAttribute>(
                    typeof(BinaryOperator)).Select(a => a.OperatorSymbol);

                return $"[{string.Join(string.Empty, operatorSymbols)}]";
            }
        }

        /// <summary>
        /// Gets or sets left hand child
        /// </summary>
        public Node? LhsChild { get; set; }

        /// <summary>
        /// Gets or sets right hand child
        /// </summary>
        public Node? RhsChild { get; set; }

        /// <summary>
        /// Gets the operation to perform when evaluated
        /// </summary>
        public BinaryOperator Operator { get; }

        /// <inheritdoc/>
        public int Precedence => this.Operator.GetAttribute<OperatorPrecedenceAttribute>().OperatorPrecedence;

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            if (this.RhsChild == null || this.LhsChild == null)
            {
                throw new InvalidExpressionTreeException("Binary operators must have two values!");
            }

            return this.Operator switch
            {
                BinaryOperator.Add => this.LhsChild.Evaluate() + this.RhsChild.Evaluate(),
                BinaryOperator.Subtract => this.LhsChild.Evaluate() - this.RhsChild.Evaluate(),
                _ => throw new InvalidExpressionTreeException("Invalid operator in expression!"),
            };
        }
    }
}
