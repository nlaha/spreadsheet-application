using Spreadsheet_Nathan_Laha;
using System.Reflection;
using System.Windows.Forms;

namespace Spreadsheet_Nathan_Laha_Tests
{
    internal class MainFormTests
    {
        private MainForm _mainForm = new();

        [Test]
        public void DataGrid_IsInitialized()
        {
            // arrange
            DataGridView dataGridView = new DataGridView();
            MethodInfo methodInfo = this.GetMethod("InitializeDataGrid");

            // act
            methodInfo.Invoke(this, new object[] { dataGridView });

            // assert
            Assert.That(dataGridView.Columns.Count, Is.EqualTo(26));
            Assert.That(dataGridView.Rows.Count , Is.EqualTo(50));
        }

        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
                Assert.Fail("methodName cannot be null or whitespace");
            var method = this._mainForm.GetType()
                .GetMethod(methodName,
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
                Assert.Fail(string.Format("{0} method not found", methodName));
            return method!;
        }
    }
}
