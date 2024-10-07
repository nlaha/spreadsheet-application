// <copyright file="ExpressionTree.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using SpreadsheetEngine.Exceptions;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents an expression tree that will be evaluated to
    /// numeric values
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Current expression string
        /// </summary>
        private readonly string _expression;

        /// <summary>
        /// The root node of the expression tree
        /// </summary>
        private readonly Node _tree;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">the expression to generate the tree from</param>
        public ExpressionTree(string expression)
        {
            this._expression = expression;
            this.Variables = new Dictionary<string, double>();

            // get the next expression node
            // expressions need to start with a variable node, or constant node
            var isNumericConstant = Regex.Match(expression, @"^\d+(\.+\d+)*");
            var isVariable = Regex.Match(expression, @"");

            if (isNumericConstant.Success || isVariable.Success)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new InvalidExpressionTreeException("Expressions must start with a variable or constant");
            }
        }

        /// <summary>
        /// Gets dictionary of variables for this expression
        /// </summary>
        public Dictionary<string, double> Variables { get; }

        /// <summary>
        /// Sets the specified variable within the expression tree variables dictionary
        /// </summary>
        /// <param name="variableName">the name of the variable</param>
        /// <param name="variableValue">the variable value</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.Variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluate the expression tree value
        /// </summary>
        /// <returns>the evaluated value</returns>
        public double Evaluate()
        {
            return 0.0;
        }
    }
}
