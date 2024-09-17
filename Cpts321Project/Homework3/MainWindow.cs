// <copyright file="MainWindow.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Homework3
{
    /// <summary>
    /// The main window for the application
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Writes the current text content to a <see cref="TextWriter"/>
        /// </summary>
        /// <param name="sw">the text writer</param>
        private void SaveText(TextWriter sw)
        {
            sw.WriteLine(this.mainTextBox.Text);
        }

        /// <summary>
        /// Reads from a <see cref="TextReader"/> and saves
        /// it in the current text content
        /// </summary>
        /// <param name="sr">the text reader</param>
        private void LoadText(TextReader sr)
        {
            this.mainTextBox.Text = sr.ReadToEnd();
        }

        /// <summary>
        /// Called when the open button is clicked
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">event arguments</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var filePath = this.openFileDialog.FileName;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    this.LoadText(reader);
                }
            }
        }

        /// <summary>
        /// Called when the save button is clicked
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">event arguments</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.saveFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var filePath = this.saveFileDialog.FileName;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    this.SaveText(writer);
                }
            }
        }

        /// <summary>
        /// Called when the "Generate 50 Fibonacci Numbers" button is clicked
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">event arguments</param>
        private void Generate50FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadText(new FibonacciTextReader(50));
        }

        /// <summary>
        /// Called when the "Generate 50 Fibonacci Numbers" button is clicked
        /// </summary>
        /// <param name="sender">the sender object</param>
        /// <param name="e">event arguments</param>
        private void Generate100FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadText(new FibonacciTextReader(100));
        }
    }
}
