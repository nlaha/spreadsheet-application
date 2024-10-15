// <copyright file="ExpressionTreeTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine.Exceptions;
    using SpreadsheetEngine.ExpressionTree;
    using System.Windows.Forms;

    /// <summary>
    /// Test the expression tree class and related classes
    /// </summary>
    internal class ExpressionTreeTests
    {
        /// <summary>
        /// Expression tree instance
        /// </summary>
        private ExpressionTree _expressionTree = new (string.Empty);

        /// <summary>
        /// Test binary operator evaluation
        /// </summary>
        [Test]
        public void NodeBinaryOperator_Evaluate()
        {
            // arrange
            var lhs = new NodeNumericConstant("10.0");
            var rhs = new NodeNumericConstant("5.0");
            var node = OperatorNodeFactory.CreateBinaryOperator('+');

            // extra assert
            Assert.That(node, Is.Not.Null);

            node.Left = lhs;
            node.Right = rhs;

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

        /// <summary>
        /// Tests the shunting yard algorithm
        /// </summary>
        /// <param name="infixExpression">the infix expression</param>
        /// <param name="postfixExpression">the expected postfix expression</param>
        [Test]
        [TestCase("12+12", new string[] { "12", "12", "+" })]
        [TestCase("12+12+24", new string[] { "12", "12", "+", "24", "+" })]
        [TestCase("12+12*24", new string[] { "12", "12", "24", "*", "+" })]
        [TestCase("12+12*24/2", new string[] { "12", "12", "24", "*", "2", "/", "+" })]
        [TestCase("12+12*24/2-1", new string[] { "12", "12", "24", "*", "2", "/", "+", "1", "-" })]
        [TestCase("(12+12)*24/2-1", new string[] { "12", "12", "+", "24", "*", "2", "/", "1", "-" })]
        public void ExpressionTree_PerformShuntingYardAlgorithm(string infixExpression, string[] postfixExpression)
        {
            // arrange
            var methodInfo = TestHelpers.GetMethod(this._expressionTree, "PerformShuntingYardAlgorithm");

            // act
            var result = methodInfo.Invoke(this._expressionTree, new object[] { infixExpression });

            // assert
            Assert.That(result, Is.EqualTo(postfixExpression));
        }
    }
}
