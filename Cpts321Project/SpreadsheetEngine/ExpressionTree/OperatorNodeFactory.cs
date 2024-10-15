// <copyright file="OperatorNodeFactory.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using SpreadsheetEngine.ExpressionTree.Operators;

    /// <summary>
    /// Constructs operators
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Creates a binary operator node based on the operator character
        /// </summary>
        /// <param name="op">the operator character, i.e. +, -</param>
        /// <returns>the binary operator node</returns>
        public static NodeBinaryOperator? CreateBinaryOperator(char op)
        {
            switch (op)
            {
                case '+':
                    return new AdditionOperatorNode();
                case '-':
                    return new SubtractionOperatorNode();
                case '*':
                    return new MultiplicationOperatorNode();
                case '/':
                    return new DivisionOperatorNode();
                default:
                    return null;
            }
        }
    }
}
