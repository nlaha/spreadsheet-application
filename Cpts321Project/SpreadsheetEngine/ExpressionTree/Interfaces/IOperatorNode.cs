// <copyright file="IOperatorNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    /// <summary>
    /// Interface for nodes that are operators
    /// </summary>
    internal interface IOperatorNode
    {
        /// <summary>
        /// Gets the precedence for this node, used when building the tree
        /// Nodes with a lower precedence will be added first
        /// </summary>
        public int Precedence { get; }
    }
}
