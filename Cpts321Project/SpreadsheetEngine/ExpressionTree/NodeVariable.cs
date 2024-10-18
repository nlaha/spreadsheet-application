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
        private readonly Dictionary<string, double> _variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVariable"/> class.
        /// </summary>
        /// <param name="variables">the parent expression tree variables dictionary</param>
        /// <param name="name">the variable name</param>
        public NodeVariable(Dictionary<string, double> variables, string name)
        {
            this._variables = variables;
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
            if (this._variables.ContainsKey(this.Name))
            {
                return this._variables[this.Name];
            }
            else
            {
                throw new InvalidExpressionTreeException($"Referenced variable: \"{this.Name}\" cannot be found!");
            }
        }
    }
}
