﻿// <copyright file="NodeFactory.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.ExpressionTree
{
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree.Operators;

    /// <summary>
    /// Constructs operators
    /// </summary>
    internal class NodeFactory
    {
        /// <summary>
        /// Internal dictionary of operators and their types, populated
        /// when the node factory is constructed
        /// </summary>
        private Dictionary<char, Type> _operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeFactory"/> class.
        /// </summary>
        public NodeFactory()
        {
            this._operators = new Dictionary<char, Type>();

            // populate operators
            this.TraverseAvailableOperators((op, type) => this._operators.Add(op, type));
        }

        /// <summary>
        /// Delegate for the TraverseAvailableOperators function
        /// </summary>
        /// <param name="op">the operator character</param>
        /// <param name="type">the operator type</param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Creates a binary operator node based on the operator character
        /// </summary>
        /// <param name="op">the operator character, i.e. +, -</param>
        /// <returns>the binary operator node</returns>
        public NodeBinaryOperator? CreateBinaryOperator(char op)
        {
            if (this._operators.TryGetValue(op, out var opType))
            {
                object? operatorNodeObject = System.Activator.CreateInstance(opType);
                if (operatorNodeObject is NodeBinaryOperator)
                {
                    return (NodeBinaryOperator)operatorNodeObject;
                }
            }

            throw new InvalidOperatorException($"Operator \"{op}\" is not supported");
        }

        /// <summary>
        /// Creates a node from a string token
        /// </summary>
        /// <param name="variables">the parent expression tree's variables dictionary</param>
        /// <param name="expression">the expression</param>
        /// <param name="node">the created node</param>
        /// <returns>the expression minus the tokens used to create the node</returns>
        public string CreateNode(Dictionary<string, double> variables, string expression, out Node node)
        {
            // make operator node
            try
            {
                var op = this.CreateBinaryOperator(expression.FirstOrDefault());
                if (op is not null)
                {
                    node = op;

                    // skip the operator character
                    return expression[1..];
                }
            }
            catch (InvalidOperatorException)
            {
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
            }

            throw new InvalidExpressionTreeException($"Expression contains invalid syntax");
        }

        /// <summary>
        /// Traverses available operator types and calls the delegate
        /// with the symbol and the type
        /// </summary>
        /// <param name="onOperator">a function</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of NodeBinaryOperator
            Type operatorNodeType = typeof(NodeBinaryOperator);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our NodeBinaryOperator class using LINQ
                IEnumerable<Type> operatorTypes =
                    assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of NodeBinaryOperator
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the node regex property, for operators this will be
                    // a single character representing the operator symbol
                    PropertyInfo? operatorField = type.GetProperty("NodeRegex");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object? value = operatorField.GetValue(type);

                        if (value is string str && !string.IsNullOrEmpty(str))
                        {
                            char operatorSymbol = str.FirstOrDefault();

                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
