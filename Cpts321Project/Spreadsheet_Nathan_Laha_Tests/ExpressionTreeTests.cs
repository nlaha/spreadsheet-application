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
        /// Expression tree instance
        /// </summary>
        /// <remarks>This contains a dummy expression so it doesn't throw an exception</remarks>
        private ExpressionTree _expressionTree = new ();

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
        /// Test a more complex expression with parentheses
        /// </summary>
        [Test]
        public void ExpressionTree_ComplexExpressionParentheses()
        {
            // arrange
            var tree = new ExpressionTree("12+12*(2+2)");

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(96));
        }

        /// <summary>
        /// Test operator precedence with expression tree
        /// </summary>
        [Test]
        public void ExpressionTree_TestPrecedence()
        {
            // arrange
            var tree = new ExpressionTree("12-2*2");

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(8));
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
        public void ExpressionTree_PerformShuntingYardAlgorithm()
        {
            // arrange
            var methodInfo = TestHelpers.GetMethod(this._expressionTree, "PerformShuntingYardAlgorithm");

            // act
            var result = methodInfo.Invoke(this._expressionTree, new object[] { "12+12*2-(12/2)" });

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result as List<object>, Has.Count.EqualTo(9));
        }
    }
}
