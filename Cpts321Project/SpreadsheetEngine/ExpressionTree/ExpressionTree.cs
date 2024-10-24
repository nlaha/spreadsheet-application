// <copyright file="ExpressionTree.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System.Text.RegularExpressions;
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
        /// The node factory instance used to create nodes
        /// </summary>
        private readonly NodeFactory _nodeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        public ExpressionTree()
        {
            this._expression = string.Empty;
            this._nodeFactory = new NodeFactory();

            this.Variables = new Dictionary<string, double>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">the expression to generate the tree from</param>
        public ExpressionTree(string expression)
        {
            this._expression = expression;
            this._nodeFactory = new NodeFactory();

            this.Variables = new Dictionary<string, double>();

            // construct tree from postfix expression
            var stack = new Stack<Node>();
            foreach (object obj in this.PerformShuntingYardAlgorithm(expression))
            {
                if (obj is NodeVariable || obj is NodeNumericConstant)
                {
                    var node = obj as Node ??
                        throw new InvalidExpressionTreeException("Invalid expression tree node");

                    stack.Push(node);
                }
                else if (obj is NodeBinaryOperator op)
                {
                    if (stack.Count < 2)
                    {
                        throw new InvalidExpressionTreeException("Binary operators must have two operands");
                    }

                    op.Right = stack.Pop();
                    op.Left = stack.Pop();
                    stack.Push(op);
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidExpressionTreeException("Invalid expression tree");
            }

            this._tree = stack.Pop();
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
        private List<object> PerformShuntingYardAlgorithm(string infixExpression)
        {
            if (string.IsNullOrWhiteSpace(infixExpression))
            {
                throw new InvalidExpressionTreeException("Expression cannot be empty");
            }

            // temporary data structures
            var output = new List<object>();
            var stack = new Stack<object>();
            var nextExpression = infixExpression;

            while (true)
            {
                char nextChar = nextExpression.FirstOrDefault();

                // skip spaces
                if (nextChar == ' ')
                {
                    nextExpression = nextExpression[1..];
                    continue;
                }

                // process open parentheses
                while (nextChar == '(')
                {
                    stack.Push(nextChar);
                    nextExpression = nextExpression[1..];
                    nextChar = nextExpression.FirstOrDefault();
                }

                // process close parentheses
                while (nextChar == ')')
                {
                    // Extra parentheses
                    if (stack.Count == 0)
                    {
                        break;
                    }

                    // pop until we see a left parenthesis
                    while (stack.Count > 1 && !(stack.Peek() is char c && c == '('))
                    {
                        output.Add(stack.Pop());
                    }

                    stack.Pop();
                    nextExpression = nextExpression[1..];
                    nextChar = nextExpression.FirstOrDefault();
                }

                // break if we have no more expression
                if (string.IsNullOrWhiteSpace(nextExpression))
                {
                    break;
                }

                // process and create nodes
                Node node;
                nextExpression = this._nodeFactory.CreateNode(this.Variables, nextExpression, out node);

                // process operators by precedence and associativity
                if (node is NodeBinaryOperator op)
                {
                    var opPrevious = stack.Count > 0 ? stack.Peek() as NodeBinaryOperator : null;
                    while (opPrevious != null && op.Precedence <= opPrevious.Precedence && op.Associativity == EAssociativity.Left)
                    {
                        output.Add(stack.Pop());
                        opPrevious = stack.Count > 0 ? stack.Peek() as NodeBinaryOperator : null;
                    }

                    stack.Push(op);
                }

                // push operands to the output
                else if (node is NodeVariable variable)
                {
                    output.Add(variable);
                }
                else if (node is NodeNumericConstant numeric)
                {
                    output.Add(numeric);
                }
            }

            // pop remaining operators
            while (stack.Count > 0)
            {
                // pop any left-hand parentheses that are left over
                if (stack.Peek() as char? == '(')
                {
                    throw new InvalidExpressionTreeException("Mismatched Parentheses");
                }

                output.Add(stack.Pop());
            }

            return output;
        }
    }
}
