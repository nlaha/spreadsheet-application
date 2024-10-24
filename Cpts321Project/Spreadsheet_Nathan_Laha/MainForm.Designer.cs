namespace Spreadsheet_Nathan_Laha
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGrid = new DataGridView();
            demoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
            SuspendLayout();
            // 
            // dataGrid
            // 
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.Location = new Point(0, 0);
            dataGrid.Name = "dataGrid";
            dataGrid.Size = new Size(800, 450);
            dataGrid.TabIndex = 0;
            dataGrid.CellBeginEdit += OnCellBeginEdit;
            dataGrid.CellEndEdit += OnCellEndEdit;
            // 
            // demoButton
            // 
            demoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            demoButton.Location = new Point(713, 415);
            demoButton.Name = "demoButton";
            demoButton.Size = new Size(75, 23);
            demoButton.TabIndex = 1;
            demoButton.Text = "Demo";
            demoButton.UseVisualStyleBackColor = true;
            demoButton.Click += DemoButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(demoButton);
            Controls.Add(dataGrid);
            Name = "MainForm";
            Text = "Spreadsheet Application CPTS_321";
            ((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGrid;
        private Button demoButton;
    }
}
