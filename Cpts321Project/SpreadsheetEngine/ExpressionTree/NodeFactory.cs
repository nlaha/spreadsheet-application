// <copyright file="NodeFactory.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Text.RegularExpressions;
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree.Operators;

    /// <summary>
    /// Constructs operators
    /// </summary>
    internal class NodeFactory
    {
        /// <summary>
        /// Creates a binary operator node based on the operator character
        /// </summary>
        /// <param name="op">the operator character, i.e. +, -</param>
        /// <returns>the binary operator node</returns>
        public static NodeBinaryOperator? CreateBinaryOperator(char op)
        {
            switch (op)
            {
                case '+':
                    return new AdditionOperatorNode();
                case '-':
                    return new SubtractionOperatorNode();
                case '*':
                    return new MultiplicationOperatorNode();
                case '/':
                    return new DivisionOperatorNode();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates a node from a string token
        /// </summary>
        /// <param name="variables">the parent expression tree's variables dictionary</param>
        /// <param name="expression">the expression</param>
        /// <param name="node">the created node</param>
        /// <returns>the expression minus the tokens used to create the node</returns>
        public static string CreateNode(Dictionary<string, double> variables, string expression, out Node node)
        {
            // make operator node
            var op = NodeFactory.CreateBinaryOperator(expression.FirstOrDefault());
            if (op is not null)
            {
                node = op;

                // skip the operator character
                return expression[1..];
            }

            // make variable node
            var variableMatch = Regex.Match(expression, NodeVariable.NodeRegex);
            if (variableMatch.Success)
            {
                node = new NodeVariable(variables, variableMatch.Value);

                // skip the variable name
                return expression[variableMatch.Length..];
            }

            // make numeric constant node
            var numericMatch = Regex.Match(expression, NodeNumericConstant.NodeRegex);
            if (numericMatch.Success)
            {
                node = new NodeNumericConstant(numericMatch.Value);

                // skip the numeric constant
                return expression[numericMatch.Length..];
            }

            throw new InvalidExpressionTreeException($"Expression contains invalid syntax");
        }
    }
}
