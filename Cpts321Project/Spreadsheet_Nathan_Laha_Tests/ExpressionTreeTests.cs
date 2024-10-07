// <copyright file="ExpressionTreeTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Test the expression tree class and related classes
    /// </summary>
    internal class ExpressionTreeTests
    {
        /// <summary>
        /// Test binary operator evaluation
        /// </summary>
        [Test]
        public void NodeBinaryOperator_Evaluate()
        {
            // arrange
            var lhs = new NodeNumericConstant(10.0);
            var rhs = new NodeNumericConstant(5.0);
            var node = new NodeBinaryOperator(lhs, rhs, BinaryOperator.Add);

            // act
            var result = node.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(15.0));
        }

        /// <summary>
        /// Tests variable nodes
        /// </summary>
        [Test]
        public void NodeVariable_Evaluate()
        {
            // arrange
            var expressionTree = new ExpressionTree(string.Empty);
            expressionTree.SetVariable("test", 123.0);
            var node = new NodeVariable(expressionTree, "test");

            // act
            var result = node.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(123.0));
        }

        /// <summary>
        /// Tests numeric constant nodes
        /// </summary>
        [Test]
        public void NodeNumericConstant_Evaluate()
        {
            // arrange
            var node = new NodeNumericConstant(123.0);

            // act
            var result = node.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(123.0));
        }

        /// <summary>
        /// Evaluate expression tree test
        /// </summary>
        [Test]
        public void ExpressionTree_Evaluate()
        {
            // arrange
            var tree = new ExpressionTree("12+12");

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(24));
        }

        /// <summary>
        /// Test invalid expression on expression tree
        /// </summary>
        [Test]
        public void ExpressionTree_InvalidExpression()
        {
            // arrange
            var expression = "12+";

            // act & assert
            Assert.Throws<InvalidExpressionTreeException>(() =>
            {
                var expressionTree = new ExpressionTree(expression);
                expressionTree.Evaluate();
            });
        }
    }
}
