// <copyright file="AdditionOperatorNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree.Operators
{
    /// <summary>
    /// Addition operator (+)
    /// </summary>
    internal class AdditionOperatorNode : NodeBinaryOperator
    {
        /// <inheritdoc/>
        public override EAssociativity Associativity => EAssociativity.Left;

        /// <inheritdoc/>
        public override int Precedence => 0;

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            if (this.Left == null || this.Right == null)
            {
                throw new InvalidOperationException("Binary operator has one or more null operands");
            }

            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
