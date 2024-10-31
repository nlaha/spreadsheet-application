// <copyright file="MultiplicationOperatorNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree.Operators
{
    /// <summary>
    /// Multiplication Operator (*)
    /// </summary>
    internal class MultiplicationOperatorNode : NodeBinaryOperator
    {
        /// <summary>
        /// Gets node regex
        /// </summary>
        public static string NodeRegex => @"*";

        /// <inheritdoc/>
        public override EAssociativity Associativity => EAssociativity.Left;

        /// <inheritdoc/>
        public override int Precedence => 1;

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            if (this.Left == null || this.Right == null)
            {
                throw new InvalidOperationException("Binary operator has one or more null operands");
            }

            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
