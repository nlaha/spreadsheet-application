// <copyright file="SpreadsheetTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using System.Reflection;
    using Spreadsheet_Nathan_Laha;
    using SpreadsheetEngine;
    using SpreadsheetEngine.Exceptions;

    /// <summary>
    /// Tests for the spreadsheet class
    /// </summary>
    internal class SpreadsheetTests
    {
        /// <summary>
        /// main form class
        /// </summary>
        private Spreadsheet _spreadsheet = new ();

        /// <summary>
        /// Test to make sure spreadsheet is initialized
        /// </summary>
        [Test]
        public void Spreadsheet_Initializes_CellArray()
        {
            // arrange
            // act
            Spreadsheet spreadsheet = new Spreadsheet();

            // assert
            Assert.That(spreadsheet, Is.Not.Null);
            Assert.That(spreadsheet.GetCell(Constants.NUMCOLUMNS - 1, Constants.NUMROWS - 1), Is.Not.Null);
        }

        /// <summary>
        /// Edge case test for GetCell
        /// </summary>
        [Test]
        public void Spreadsheet_ThrowsExceptionWhen_GetCell_OutOfRange()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            // act & assert
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                spreadsheet.GetCell(-1, 0);
            });
        }

        /// <summary>
        /// Tests column name to index functionality
        /// </summary>
        [Test]
        public void Spreadsheet_GetsColumnIndex_FromName()
        {
            // arrange
            char column = 'A';
            MethodInfo methodInfo = this.GetMethod("ColumnToIndex");

            // act
            var result = methodInfo.Invoke(this._spreadsheet, new object[] { column });

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Test the basic reference formaula
        /// </summary>
        [Test]
        public void Spreadsheet_Evaluate_BasicReferenceFormula()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            // act
            spreadsheet.SetCellText(0, 0, "10");
            spreadsheet.SetCellText(0, 1, "=A1");

            // assert
            Assert.That(spreadsheet.GetCell(0, 1).Value, Is.EqualTo("10"));
        }

        /// <summary>
        /// Test the self-references
        /// </summary>
        [Test]
        public void Spreadsheet_Errors_SelfRefrenceFormula()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            // act
            spreadsheet.SetCellText(0, 0, "=A1");

            // assert
            Assert.That(spreadsheet.GetCell(0, 0).ErrorText, Is.EqualTo("Circular reference detected"));
        }

        /// <summary>
        /// Test the self-references
        /// </summary>
        [Test]
        public void Spreadsheet_Errors_SelfRefrenceFormulaComplex()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            spreadsheet.GetCell(0, 0).Value = "12";
            spreadsheet.GetCell(0, 1).Value = "34";

            // act
            spreadsheet.SetCellText(0, 1, "=A1");
            spreadsheet.SetCellText(0, 0, "=A2");

            // assert
            Assert.That(spreadsheet.GetCell(0, 0).ErrorText, Is.EqualTo("Circular reference detected"));
        }

        /// <summary>
        /// Test the self-references
        /// </summary>
        [Test]
        public void Spreadsheet_Errors_SelfRefrenceFormulaComplex2()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            spreadsheet.GetCell(0, 0).Value = "12";
            spreadsheet.GetCell(0, 1).Value = "34";

            // act
            spreadsheet.SetCellText(0, 0, "=A2");
            spreadsheet.SetCellText(0, 1, "=A1");

            Assert.That(spreadsheet.GetCell(0, 1).ErrorText, Is.EqualTo("Circular reference detected"));

            spreadsheet.SetCellText(0, 0, "123");

            // assert
            Assert.That(spreadsheet.GetCell(0, 1).ErrorText, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test the self-references
        /// </summary>
        [Test]
        public void Spreadsheet_Errors_SelfRefrenceFormulaComplex3()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();

            spreadsheet.GetCell(0, 0).Value = "12";
            spreadsheet.GetCell(0, 1).Value = "34";

            // act
            spreadsheet.SetCellText(0, 0, "=A2");
            spreadsheet.SetCellText(0, 1, "=B1");
            spreadsheet.SetCellText(1, 0, "=A1");

            Assert.That(spreadsheet.GetCell(1, 0).ErrorText, Is.EqualTo("Circular reference detected"));

            spreadsheet.SetCellText(0, 0, "123");

            // assert
            Assert.That(spreadsheet.GetCell(1, 0).ErrorText, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test getting a cell by it's name i.e. 'A0'
        /// </summary>
        [Test]
        public void Spreadsheet_GetCellByName()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetCellText(0, 0, "10");
            var name = "A1";

            // act
            Cell cell = spreadsheet.GetCellByName(name);

            // assert
            Assert.That(cell, Is.EqualTo(spreadsheet.GetCell(0, 0)));
        }

        /// <summary>
        /// Test getting an invalid cell name results in null
        /// </summary>
        [Test]
        public void Spreadsheet_GetCellByName_ThrowsException()
        {
            // arrange
            Spreadsheet spreadsheet = new Spreadsheet();
            var name = "Bad Name";

            // act & assert
            Assert.Throws<NullReferenceException>(() => spreadsheet.GetCellByName(name));
        }

        /// <summary>
        /// Gets a method on the main form object using reflection
        /// </summary>
        /// <param name="methodName">the name of the method</param>
        /// <returns>the method info</returns>
        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            var method = this._spreadsheet.GetType()
                .GetMethod(
                    methodName,
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            return method!;
        }
    }
}
