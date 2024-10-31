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
    internal class NodeVariable : Node
    {
        /// <summary>
        /// Reference to parent expression tree instance
        /// </summary>
        private readonly Func<string, double> _variableGetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVariable"/> class.
        /// </summary>
        /// <param name="variableGetter">the variable getter function used to lookup variable values</param>
        /// <param name="name">the variable name</param>
        public NodeVariable(Func<string, double> variableGetter, string name)
        {
            this._variableGetter = variableGetter;
            this.Name = name;
        }

        /// <summary>
        /// Gets node regex
        /// </summary>
        public static string NodeRegex => @"^[A-Za-z]\w+";

        /// <summary>
        /// Gets or sets the variable name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            return this._variableGetter.Invoke(this.Name);
        }
    }
}
