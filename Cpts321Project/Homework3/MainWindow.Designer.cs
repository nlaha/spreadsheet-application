﻿namespace Homework3
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            generate50FibonacciNumbersToolStripMenuItem = new ToolStripMenuItem();
            generate100FibonacciNumbersToolStripMenuItem = new ToolStripMenuItem();
            mainTextBox = new TextBox();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, toolStripSeparator1, generate50FibonacciNumbersToolStripMenuItem, generate100FibonacciNumbersToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(248, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(248, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(245, 6);
            // 
            // generate50FibonacciNumbersToolStripMenuItem
            // 
            generate50FibonacciNumbersToolStripMenuItem.Name = "generate50FibonacciNumbersToolStripMenuItem";
            generate50FibonacciNumbersToolStripMenuItem.Size = new Size(248, 22);
            generate50FibonacciNumbersToolStripMenuItem.Text = "Generate 50 Fibonacci Numbers";
            generate50FibonacciNumbersToolStripMenuItem.Click += Generate50FibonacciNumbersToolStripMenuItem_Click;
            // 
            // generate100FibonacciNumbersToolStripMenuItem
            // 
            generate100FibonacciNumbersToolStripMenuItem.Name = "generate100FibonacciNumbersToolStripMenuItem";
            generate100FibonacciNumbersToolStripMenuItem.Size = new Size(248, 22);
            generate100FibonacciNumbersToolStripMenuItem.Text = "Generate 100 Fibonacci Numbers";
            generate100FibonacciNumbersToolStripMenuItem.Click += Generate100FibonacciNumbersToolStripMenuItem_Click;
            // 
            // mainTextBox
            // 
            mainTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTextBox.Location = new Point(0, 24);
            mainTextBox.Multiline = true;
            mainTextBox.Name = "mainTextBox";
            mainTextBox.ScrollBars = ScrollBars.Vertical;
            mainTextBox.Size = new Size(800, 427);
            mainTextBox.TabIndex = 1;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "Text|*.txt|All|*.*";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(mainTextBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "Notepad--";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem generate50FibonacciNumbersToolStripMenuItem;
        private ToolStripMenuItem generate100FibonacciNumbersToolStripMenuItem;
        private TextBox mainTextBox;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
    }
}