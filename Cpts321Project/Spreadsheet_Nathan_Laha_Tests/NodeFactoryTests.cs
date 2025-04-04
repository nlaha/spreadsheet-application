﻿// <copyright file="NodeFactoryTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree;
    using SpreadsheetEngine.ExpressionTree.Operators;

    /// <summary>
    /// Test the expression tree class and related classes
    /// </summary>
    internal class NodeFactoryTests
    {
        /// <summary>
        /// Test operator node creation
        /// </summary>
        /// <param name="op">operator</param>
        /// <param name="expectedType">expected node</param>
        [Test]
        [TestCase('+', typeof(AdditionOperatorNode))]
        [TestCase('-', typeof(SubtractionOperatorNode))]
        [TestCase('*', typeof(MultiplicationOperatorNode))]
        [TestCase('/', typeof(DivisionOperatorNode))]
        public void NodeFactory_CreateBinaryOperator(char op, Type expectedType)
        {
            // arrange
            var opInstance = Activator.CreateInstance(expectedType);

            // act
            var nodeFactory = NodeFactorySingleton.Instance;
            var node = nodeFactory.CreateBinaryOperator(op);

            // assert
            Assert.That(node, Is.Not.Null);
            Assert.That(node.GetType(), Is.EqualTo(expectedType));
        }

        /// <summary>
        /// Tests node creation
        /// </summary>
        /// <param name="expression">expression</param>
        /// <param name="expectedType">expected node</param>
        [Test]
        [TestCase("+", typeof(AdditionOperatorNode))]
        [TestCase("-", typeof(SubtractionOperatorNode))]
        [TestCase("*", typeof(MultiplicationOperatorNode))]
        [TestCase("/", typeof(DivisionOperatorNode))]
        [TestCase("^", typeof(PowerOperatorNode))]
        [TestCase("test", typeof(NodeVariable))]
        [TestCase("20", typeof(NodeNumericConstant))]
        [TestCase("20.0", typeof(NodeNumericConstant))]
        public void NodeFactory_CreateNode(string expression, Type expectedType)
        {
            // arrange
            // act
            var node = null as Node;
            var nodeFactory = NodeFactorySingleton.Instance;
            var str = nodeFactory.CreateNode((name) => { return 0.0; }, expression, out node);

            // assert
            Assert.That(node, Is.Not.Null);
            Assert.That(node.GetType(), Is.EqualTo(expectedType));
            Assert.That(str, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests if create node can accept expressions with parentheses
        /// </summary>
        /// <param name="expression">expression</param>
        [Test]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("...")]
        public void NodeFactory_ThrowsOnInvalid(string expression)
        {
            // arrange
            // act
            // assert
            var node = null as Node;
            var nodeFactory = NodeFactorySingleton.Instance;
            Assert.Throws<InvalidExpressionTreeException>(() =>
            {
                nodeFactory.CreateNode((name) => { return 0.0; }, expression, out node);
            });
        }
    }
}
