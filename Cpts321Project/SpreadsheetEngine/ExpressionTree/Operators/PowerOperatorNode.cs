// <copyright file="PowerOperatorNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree.Operators
{
    /// <summary>
    /// Subtraction Operator (^)
    /// </summary>
    internal class PowerOperatorNode : NodeBinaryOperator
    {
        /// <summary>
        /// Gets node regex
        /// </summary>
        public static string NodeRegex => @"^";

        /// <inheritdoc/>
        public override EAssociativity Associativity => EAssociativity.Right;

        /// <inheritdoc/>
        public override int Precedence => 0;

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            if (this.Left == null || this.Right == null)
            {
                throw new InvalidOperationException("Binary operator has one or more null operands");
            }

            return Math.Pow(this.Left.Evaluate(), this.Right.Evaluate());
        }
    }
}
