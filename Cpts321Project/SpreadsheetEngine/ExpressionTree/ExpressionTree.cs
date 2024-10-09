// <copyright file="ExpressionTree.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Collections;
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
            this._expression = expression;
            this.Variables = new Dictionary<string, double>();

            // Source: https://stackoverflow.com/a/26750/24202835
            var nodeInterface = typeof(IMatchableExpressionNode);
            var operatorInterface = typeof(IOperatorNode);

            var nodeTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => nodeInterface.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            var operatorTypes = nodeTypes.Where(t => operatorInterface.IsAssignableFrom(t));

            // build token regex
            var tokenRegexStr = new List<string>();
            foreach (var type in nodeTypes)
            {
                var typeRegex = type.GetProperty("NodeRegex")?.GetValue(null) as string;
                if (typeRegex != null)
                {
                    tokenRegexStr.Add(typeRegex);
                }
            }

            MatchCollection tokens = Regex.Matches(expression, string.Join("|", tokenRegexStr));

            var operands = new Stack<Node>();
            var operators = new Stack<Node>();

            // Based on: https://algo.monster/liteproblems/1597
            // construct tree
            foreach (string token in tokens.Select(m => m.Value))
            {
                foreach (var type in nodeTypes)
                {
                    var typeRegex = type.GetProperty("NodeRegex")?.GetValue(null) as string;
                    if (typeRegex != null && Regex.IsMatch(token, typeRegex))
                    {
                        // is it an operator or operand?
                        if (operatorTypes.Contains(type))
                        {
                                                        var newOperator = new NodeBinaryOperator(token);

                            // process binary operators
                            while (operators.Count > 0 && (operators.Peek() as IOperatorNode)!.Precedence >= newOperator.Precedence)
                            {
                                var op = operators.Pop() as NodeBinaryOperator;
                                if (op != null)
                                {
                                    op.RhsChild = operands.Pop();
                                    op.LhsChild = operands.Pop();
                                    operands.Push(op);
                                }
                            }

                            operators.Push(newOperator);
                        }
                        else
                        {
                            // it's an operand
                            switch (type)
                            {
                                case Type t when t == typeof(NodeNumericConstant):
                                    operands.Push(new NodeNumericConstant(token));
                                    break;
                                case Type t when t == typeof(NodeVariable):
                                    operands.Push(new NodeVariable(this, token));
                                    break;
                            }
                        }
                    }
                }
            }

            // post process binary operators
            while (operators.Count > 0)
            {
                var operatorNode = operators.Pop() as NodeBinaryOperator;
                if (operatorNode != null)
                {
                    if (operands.Count > 1)
                    {
                        operatorNode.RhsChild = operands.Pop();
                        operatorNode.LhsChild = operands.Pop();
                        operands.Push(operatorNode);
                    } else
                    {
                        throw new InvalidExpressionTreeException("Binary operator malformed, ensure operands exist!");
                    }
                }
            }

            if (operators.Count > 0)
            {
                this._tree = operands.Peek();
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
            } else
            {
                throw new InvalidExpressionTreeException("Expression tree is empty");
            }
        }
    }
}
