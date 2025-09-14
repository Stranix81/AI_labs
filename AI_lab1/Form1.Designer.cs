namespace AI_lab1
{
    partial class Form1
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
            tableLayoutPanelField = new TableLayoutPanel();
            buttonCube = new Button();
            buttonMark = new Button();
            buttonAbyss = new Button();
            buttonStart = new Button();
            checkBoxBFS = new CheckBox();
            checkBoxDFS = new CheckBox();
            buttonClear = new Button();
            SuspendLayout();
            // 
            // tableLayoutPanelField
            // 
            tableLayoutPanelField.ColumnCount = 8;
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.Location = new Point(12, 12);
            tableLayoutPanelField.Name = "tableLayoutPanelField";
            tableLayoutPanelField.RowCount = 8;
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanelField.Size = new Size(458, 426);
            tableLayoutPanelField.TabIndex = 0;
            // 
            // buttonCube
            // 
            buttonCube.Location = new Point(476, 12);
            buttonCube.Name = "buttonCube";
            buttonCube.Size = new Size(100, 23);
            buttonCube.TabIndex = 1;
            buttonCube.Text = "Cube";
            buttonCube.UseVisualStyleBackColor = true;
            buttonCube.Click += buttonCube_Click;
            // 
            // buttonMark
            // 
            buttonMark.Location = new Point(582, 12);
            buttonMark.Name = "buttonMark";
            buttonMark.Size = new Size(100, 23);
            buttonMark.TabIndex = 2;
            buttonMark.Text = "Mark";
            buttonMark.UseVisualStyleBackColor = true;
            buttonMark.Click += buttonMark_Click;
            // 
            // buttonAbyss
            // 
            buttonAbyss.Location = new Point(688, 12);
            buttonAbyss.Name = "buttonAbyss";
            buttonAbyss.Size = new Size(100, 23);
            buttonAbyss.TabIndex = 3;
            buttonAbyss.Text = "Abyss";
            buttonAbyss.UseVisualStyleBackColor = true;
            buttonAbyss.Click += buttonAbyss_Click;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(531, 41);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(206, 23);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // checkBoxBFS
            // 
            checkBoxBFS.AutoSize = true;
            checkBoxBFS.Location = new Point(582, 99);
            checkBoxBFS.Name = "checkBoxBFS";
            checkBoxBFS.Size = new Size(45, 19);
            checkBoxBFS.TabIndex = 5;
            checkBoxBFS.Text = "BFS";
            checkBoxBFS.UseVisualStyleBackColor = true;
            // 
            // checkBoxDFS
            // 
            checkBoxDFS.AutoSize = true;
            checkBoxDFS.Location = new Point(636, 99);
            checkBoxDFS.Name = "checkBoxDFS";
            checkBoxDFS.Size = new Size(46, 19);
            checkBoxDFS.TabIndex = 6;
            checkBoxDFS.Text = "DFS";
            checkBoxDFS.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(531, 70);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(206, 23);
            buttonClear.TabIndex = 7;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonClear);
            Controls.Add(tableLayoutPanelField);
            Controls.Add(checkBoxDFS);
            Controls.Add(checkBoxBFS);
            Controls.Add(buttonStart);
            Controls.Add(buttonAbyss);
            Controls.Add(buttonMark);
            Controls.Add(buttonCube);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelField;
        private Button buttonCube;
        private Button buttonMark;
        private Button buttonAbyss;
        private Button buttonStart;
        private CheckBox checkBoxBFS;
        private CheckBox checkBoxDFS;
        private Button buttonClear;
    }
}
