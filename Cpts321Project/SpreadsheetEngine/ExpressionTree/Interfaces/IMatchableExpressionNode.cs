// <copyright file="IMatchableExpressionNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree.Interfaces
{
    /// <summary>
    /// Interface for nodes that can be associated with text
    /// </summary>
    internal interface IMatchableExpressionNode
    {
        /// <summary>
        /// Gets a regex string used for matching string
        /// representations of this node
        /// </summary>
        /// <returns>the regex string</returns>
        public static abstract string NodeRegex { get; }
    }
}
