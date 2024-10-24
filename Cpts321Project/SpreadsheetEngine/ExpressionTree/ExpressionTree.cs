// <copyright file="ExpressionTree.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.ComponentModel;
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
        /// The cell that this expression tree references, optional
        /// </summary>
        private readonly Cell? _cell;

        /// <summary>
        /// Set of cells this expression references
        /// </summary>
        private readonly HashSet<Cell> _referencedCells;

        /// <summary>
        /// The node factory instance used to create nodes
        /// </summary>
        private readonly NodeFactory _nodeFactory;

        /// <summary>
        /// The root node of the expression tree
        /// </summary>
        private Node? _tree;

        /// <summary>
        /// Optional spreadsheet used for lookup up variables
        /// </summary>
        private Spreadsheet? _spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        public ExpressionTree()
        {
            this._nodeFactory = new NodeFactory();
            this._referencedCells = new HashSet<Cell>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">the expression to generate the tree from</param>
        public ExpressionTree(string expression)
        {
            this._nodeFactory = new NodeFactory();
            this._referencedCells = new HashSet<Cell>();
            this.ConstructTree(expression);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="cell">the cell containing the expression</param>
        /// <param name="spreadsheet">spreadsheet used for looking up variable values</param>
        public ExpressionTree(Cell cell, Spreadsheet spreadsheet)
        {
            // skipping the '=' character
            this._cell = cell;
            this._nodeFactory = new NodeFactory();
            this._spreadsheet = spreadsheet;
            this._referencedCells = new HashSet<Cell>();
            this.ConstructTree(cell.Text[1..]);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        ~ExpressionTree()
        {
            // unsubscribe from events
            foreach (var cell in this._referencedCells)
            {
                cell.PropertyChanged -= this.OnReferencedCellPropertyChanged;
            }
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
        /// Gets the value of a variable from the spreadsheet if it exists
        /// </summary>
        /// <param name="variableName">the variable name, i.e. A2</param>
        /// <returns>the value</returns>
        /// <exception cref="InvalidExpressionTreeException">thrown when the varible could not be found or is invalid</exception>
        public double GetVariableValue(string variableName)
        {
            if (this._spreadsheet != null)
            {
                var cell = this._spreadsheet.GetCellByName(variableName);
                if (cell == null)
                {
                    throw new InvalidExpressionTreeException($"Referenced variable: \"{variableName}\" cannot be found!");
                }

                this._referencedCells.Add(cell);

                // When any of the referenced cells change, we want to re-evaluate the expression
                // when we set the evaluated value back to the cell, it will trigger the cell's property changed event
                // and will update the UI
                cell.PropertyChanged += this.OnReferencedCellPropertyChanged;

                double value;
                var success = double.TryParse(cell.Value, out value);
                if (!success)
                {
                    throw new InvalidExpressionTreeException($"Referenced variable \"{variableName}\" is not numeric");
                }

                return value;
            }
            else
            {
                throw new InvalidExpressionTreeException("Spreadsheet is not set but variables were referenced");
            }
        }

        /// <summary>
        /// Called when any of the referenced cells change
        /// </summary>
        /// <param name="sender">the referenced cell that changed</param>
        /// <param name="e">the event args</param>
        private void OnReferencedCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (this._cell != null)
            {
                this._cell.Value = this.Evaluate().ToString();
                this._cell.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Constructs the expression tree from the expression
        /// </summary>
        /// <param name="expression">expression</param>
        /// <exception cref="InvalidExpressionTreeException">thrown when the expression is invalid</exception>
        private void ConstructTree(string expression)
        {
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
                nextExpression = this._nodeFactory.CreateNode(this.GetVariableValue, nextExpression, out node);

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
