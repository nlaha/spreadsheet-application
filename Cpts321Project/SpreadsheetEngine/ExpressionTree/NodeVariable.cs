// <copyright file="NodeVariable.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree.Interfaces;

    /// <summary>
    /// Represents an expression tree variable node
    /// </summary>
    internal class NodeVariable : Node, IMatchableExpressionNode
    {
        /// <summary>
        /// Reference to parent expression tree instance
        /// </summary>
        private readonly ExpressionTree _expressionTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVariable"/> class.
        /// </summary>
        /// <param name="expressionTree">the parent expression tree</param>
        /// <param name="name">the variable name</param>
        public NodeVariable(ExpressionTree expressionTree, string name)
        {
            this._expressionTree = expressionTree;
            this.Name = name;
        }

        /// <summary>
        /// Gets node regex
        /// </summary>
        public static string NodeRegex => @"[A-Za-z]\w+";

        /// <summary>
        /// Gets or sets the variable name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            if (this._expressionTree.Variables.ContainsKey(this.Name))
            {
                return this._expressionTree.Variables[this.Name];
            }
            else
            {
                throw new InvalidExpressionTreeException($"Referenced variable: \"{this.Name}\" cannot be found!");
            }
        }
    }
}
