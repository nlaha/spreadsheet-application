// <copyright file="EAssociativity.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    /// <summary>
    /// Associativity of an operator
    /// </summary>
    public enum EAssociativity
    {
        /// <summary>
        /// No associativity
        /// </summary>
        None,

        /// <summary>
        /// Left -> right associativity
        /// </summary>
        Left,

        /// <summary>
        /// Right -> left associativity
        /// </summary>
        Right
    }
}
