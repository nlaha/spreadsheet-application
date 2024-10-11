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
            var lhs = new NodeNumericConstant("10.0");
            var rhs = new NodeNumericConstant("5.0");
            var node = new NodeBinaryOperator("+");

            node.LhsChild = lhs;
            node.RhsChild = rhs;

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
            var node = new NodeNumericConstant("123.0");

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

        /// <summary>
        /// Test a more complex expression
        /// </summary>
        [Test]
        public void ExpressionTree_ComplexExpression()
        {
            // arrange
            var tree = new ExpressionTree("12+12+24");

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(48));
        }

        /// <summary>
        /// Tests expression tree with variables
        /// </summary>
        [Test]
        public void ExpressionTree_Variables()
        {
            // arrange
            var tree = new ExpressionTree("B4+A2+24");
            tree.SetVariable("B4", 2);
            tree.SetVariable("A2", 3);

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(29));
        }
    }
}
