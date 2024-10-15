// <copyright file="ExpressionTree.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections;
    using System.Linq.Expressions;
    using System.Text;
    using System.Text.RegularExpressions;
    using SpreadsheetEngine.Attributes;
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree.Interfaces;

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
        private readonly Node? _tree;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">the expression to generate the tree from</param>
        public ExpressionTree(string expression)
        {
            this._expression = PerformShuntingYardAlgorithm(expression);
            this.Variables = new Dictionary<string, double>();

            foreach (char c in this._expression)
            {

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
            if (this._tree != null)
            {
                return this._tree.Evaluate();
            }
            else
            {
                throw new InvalidExpressionTreeException("Expression tree is empty");
            }
        }

        /// <summary>
        /// Performs the shunting yard algorithm to convert the infix expression
        /// to a postfix expression
        /// </summary>
        /// <param name="infixExpression">the infix expression</param>
        /// <returns>the postfix expression</returns>
        private static string PerformShuntingYardAlgorithm(string infixExpression)
        {
            throw new NotImplementedException();
        }
    }
}
