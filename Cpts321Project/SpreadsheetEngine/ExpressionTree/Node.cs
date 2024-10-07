// <copyright file="ExpressionTreeNode.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using SpreadsheetEngine.Exceptions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Base class for expression tree nodes
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Gets regex run to construct the correct node type from an input
        /// </summary>
        public static string? ValidationRegex { get; }

        /// <summary>
        /// Gets the value of the node (and evaluates children if they exist)
        /// </summary>
        public double Value { get => this.Evaluate(); }

        /// <summary>
        /// Evaluates the value of this node and all child nodes
        /// </summary>
        /// <returns>the evaluated value</returns>
        internal abstract double Evaluate();
    }
}
