// <copyright file="MainFormTests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using System.Reflection;
    using System.Windows.Forms;
    using Spreadsheet_Nathan_Laha;

    /// <summary>
    /// Main form test class
    /// </summary>
    internal class MainFormTests
    {
        /// <summary>
        /// main form class
        /// </summary>
        private MainForm _mainForm = new ();

        /// <summary>
        /// Tests the data grid initialization
        /// </summary>
        [Test]
        public void DataGrid_IsInitialized()
        {
            // arrange
            DataGridView dataGridView = new DataGridView();
            MethodInfo methodInfo = this.GetMethod("InitializeDataGrid");

            // act
            methodInfo.Invoke(this._mainForm, new object[] { dataGridView });

            // assert
            Assert.That(dataGridView.Columns.Count, Is.EqualTo(26));
            Assert.That(dataGridView.Rows.Count, Is.EqualTo(50));
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

            var method = this._mainForm.GetType()
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
