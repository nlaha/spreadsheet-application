// <copyright file="ExpressionTreeNodeTests.cs" company="Nathan Laha">
// 11762135
// </copyright>
namespace Spreadsheet_Nathan_Laha_Tests
{
    using SpreadsheetEngine.ExpressionTree;

    /// <summary>
    /// Tests for nodes in the expression tree
    /// </summary>
    internal class ExpressionTreeNodeTests
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
            var nodeFactory = new NodeFactory();
            var node = nodeFactory.CreateBinaryOperator('+');

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
            var expressionTree = new ExpressionTree();
            expressionTree.SetVariable("test", 123.0);
            var node = new NodeVariable(expressionTree.Variables, "test");

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
    }
}
