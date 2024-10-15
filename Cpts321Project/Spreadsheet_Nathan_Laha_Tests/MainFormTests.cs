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
        /// Main form instance
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
            MethodInfo methodInfo = TestHelpers.GetMethod(this._mainForm, "InitializeDataGrid");

            // act
            methodInfo.Invoke(this._mainForm, new object[] { dataGridView });

            // assert
            Assert.That(dataGridView.Columns.Count, Is.EqualTo(26));
            Assert.That(dataGridView.Rows.Count, Is.EqualTo(50));
        }
    }
}
