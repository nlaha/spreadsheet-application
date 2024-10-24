// <copyright file="ExpressionTreeTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using System.Linq.Expressions;
    using System.Reflection;
    using SpreadsheetEngine;
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
        /// <param name="expression">the expression</param>
        [Test]
        [TestCase("12+(")]
        [TestCase("")]
        [TestCase("12(12")]
        [TestCase("12+")]
        public void ExpressionTree_InvalidExpressionParentheses(string expression)
        {
            // arrange, act & assert
            Assert.Throws<InvalidExpressionTreeException>(() =>
            {
                var expressionTree = new ExpressionTree(expression);
                expressionTree.Evaluate();
            });
        }

        /// <summary>
        /// Tests a variety of expressions
        /// </summary>
        /// <param name="expression">the expression</param>
        /// <param name="expectedValue">the expected value</param>
        [Test]
        [TestCase("2+2", 4)]
        [TestCase("2-2", 0)]
        [TestCase("2*2", 4)]
        [TestCase("2/2", 1)]
        [TestCase("(3+4)/2", 3.5)]
        [TestCase("2*(3+4)", 14)]
        [TestCase("2*(3+4)/2", 7)]
        [TestCase("2+3*3", 11)]
        [TestCase("12+12*(2+2)", 60)]
        [TestCase("12+12*2-2/2", 35)]
        [TestCase("1+(1+(1+1))", 4)]
        [TestCase("2^2+4", 8)]
        [TestCase("(2+2)^2", 16)]
        public void ExpressionTree_EvaluatesExpressionCorrectly(string expression, double expectedValue)
        {
            // arrange
            var tree = new ExpressionTree(expression);

            // act
            var result = tree.Evaluate();

            // assert
            Assert.That(result, Is.EqualTo(expectedValue));
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
        /// Tests the shunting yard algorithm
        /// </summary>
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

        /// <summary>
        /// Tests an expression with lots of parentheses
        /// </summary>
        [Test]
        public void ExpressionTree_TestLotsOfParentheses()
        {
            // arrange
            // act
            var expression = new ExpressionTree("(((3+3)+(2+4)))");
            var res = expression.Evaluate();

            // assert
            Assert.That(res, Is.EqualTo(12));
        }

        /// <summary>
        /// Tests variable lookup from spreadsheet
        /// </summary>
        [Test]
        public void ExpressionTree_TestVariableLookup()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet(50, 50);
            TextCell cell = new TextCell(0, 0);
            cell.Text = "=A3+A2+2";

            spreadsheet.SetCellValue(0, 2, "1");
            spreadsheet.SetCellValue(0, 1, "1");

            // act
            var expression = new ExpressionTree(cell, spreadsheet);
            var res = expression.Evaluate();

            // assert
            Assert.That(res, Is.EqualTo(4));
        }

        /// <summary>
        /// Tests the expression tree with non-numeric variables
        /// </summary>
        [Test]
        public void ExpressionTree_ThrowsNonNumericVariable()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet(50, 50);
            TextCell cell = new TextCell(0, 0);
            cell.Text = "=A3+A2+2";

            spreadsheet.SetCellValue(0, 2, "hello");
            spreadsheet.SetCellValue(0, 1, "1");

            var expression = new ExpressionTree(cell, spreadsheet);

            // act & assert
            Assert.Throws<InvalidExpressionTreeException>(() =>
            {
                expression.Evaluate();
            });
        }
    }
}
