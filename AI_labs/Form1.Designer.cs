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
            buttonClear = new Button();
            radioButtonBFS = new RadioButton();
            radioButtonDFS = new RadioButton();
            radioButtonIDDFS = new RadioButton();
            radioButtonBiBFS = new RadioButton();
            buttonRestore = new Button();
            buttonSkip = new Button();
            radioButtonAStar = new RadioButton();
            comboBoxHeuristic = new ComboBox();
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
            // buttonClear
            // 
            buttonClear.Location = new Point(531, 101);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(206, 23);
            buttonClear.TabIndex = 7;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // radioButtonBFS
            // 
            radioButtonBFS.AutoSize = true;
            radioButtonBFS.Location = new Point(543, 160);
            radioButtonBFS.Name = "radioButtonBFS";
            radioButtonBFS.Size = new Size(44, 19);
            radioButtonBFS.TabIndex = 8;
            radioButtonBFS.TabStop = true;
            radioButtonBFS.Text = "BFS";
            radioButtonBFS.UseVisualStyleBackColor = true;
            // 
            // radioButtonDFS
            // 
            radioButtonDFS.AutoSize = true;
            radioButtonDFS.Location = new Point(655, 160);
            radioButtonDFS.Name = "radioButtonDFS";
            radioButtonDFS.Size = new Size(45, 19);
            radioButtonDFS.TabIndex = 9;
            radioButtonDFS.TabStop = true;
            radioButtonDFS.Text = "DFS";
            radioButtonDFS.UseVisualStyleBackColor = true;
            // 
            // radioButtonIDDFS
            // 
            radioButtonIDDFS.AutoSize = true;
            radioButtonIDDFS.Location = new Point(543, 185);
            radioButtonIDDFS.Name = "radioButtonIDDFS";
            radioButtonIDDFS.Size = new Size(56, 19);
            radioButtonIDDFS.TabIndex = 10;
            radioButtonIDDFS.TabStop = true;
            radioButtonIDDFS.Text = "IDDFS";
            radioButtonIDDFS.UseVisualStyleBackColor = true;
            // 
            // radioButtonBiBFS
            // 
            radioButtonBiBFS.AutoSize = true;
            radioButtonBiBFS.Location = new Point(655, 185);
            radioButtonBiBFS.Name = "radioButtonBiBFS";
            radioButtonBiBFS.Size = new Size(59, 19);
            radioButtonBiBFS.TabIndex = 11;
            radioButtonBiBFS.TabStop = true;
            radioButtonBiBFS.Text = "Bi-BFS";
            radioButtonBiBFS.UseVisualStyleBackColor = true;
            // 
            // buttonRestore
            // 
            buttonRestore.Location = new Point(531, 130);
            buttonRestore.Name = "buttonRestore";
            buttonRestore.Size = new Size(206, 23);
            buttonRestore.TabIndex = 12;
            buttonRestore.Text = "Restore last state";
            buttonRestore.UseVisualStyleBackColor = true;
            buttonRestore.Click += buttonRestore_Click;
            // 
            // buttonSkip
            // 
            buttonSkip.Location = new Point(531, 72);
            buttonSkip.Name = "buttonSkip";
            buttonSkip.Size = new Size(206, 23);
            buttonSkip.TabIndex = 13;
            buttonSkip.Text = "Skip";
            buttonSkip.UseVisualStyleBackColor = true;
            buttonSkip.Click += buttonSkip_Click;
            // 
            // radioButtonAStar
            // 
            radioButtonAStar.AutoSize = true;
            radioButtonAStar.Location = new Point(655, 210);
            radioButtonAStar.Name = "radioButtonAStar";
            radioButtonAStar.Size = new Size(38, 19);
            radioButtonAStar.TabIndex = 14;
            radioButtonAStar.TabStop = true;
            radioButtonAStar.Text = "A*";
            radioButtonAStar.UseVisualStyleBackColor = true;
            // 
            // comboBoxHeuristic
            // 
            comboBoxHeuristic.FormattingEnabled = true;
            comboBoxHeuristic.Location = new Point(531, 209);
            comboBoxHeuristic.Name = "comboBoxHeuristic";
            comboBoxHeuristic.Size = new Size(118, 23);
            comboBoxHeuristic.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxHeuristic);
            Controls.Add(radioButtonAStar);
            Controls.Add(buttonSkip);
            Controls.Add(buttonRestore);
            Controls.Add(radioButtonBiBFS);
            Controls.Add(radioButtonIDDFS);
            Controls.Add(radioButtonDFS);
            Controls.Add(radioButtonBFS);
            Controls.Add(buttonClear);
            Controls.Add(tableLayoutPanelField);
            Controls.Add(buttonStart);
            Controls.Add(buttonAbyss);
            Controls.Add(buttonMark);
            Controls.Add(buttonCube);
            Name = "Form1";
            Text = "Edging the cube";
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
        private Button buttonClear;
        private RadioButton radioButtonBFS;
        private RadioButton radioButtonDFS;
        private RadioButton radioButtonIDDFS;
        private RadioButton radioButtonBiBFS;
        private Button buttonRestore;
        private Button buttonSkip;
        private RadioButton radioButtonAStar;
        private ComboBox comboBoxHeuristic;
    }
}
