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
        /// The variable value
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVariable"/> class.
        /// </summary>
        /// <param name="name">the variable name</param>
        /// <param name="value">the variable value</param>
        public NodeVariable(string name, double value)
        {
            this.Name = name;
            this._value = value;
        }

        /// <summary>
        /// Gets or sets the variable name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        internal override double Evaluate()
        {
            return this._value;
        }
    }
}
