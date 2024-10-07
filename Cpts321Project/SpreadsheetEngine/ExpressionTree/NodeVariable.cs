// <copyright file="NodeVariable.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    /// <summary>
    /// Represents an expression tree variable node
    /// </summary>
    internal class NodeVariable : Node
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
        /// Gets or sets the variable name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
