namespace AI_lab1
{
    partial class FormL
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
            buttonStart = new Button();
            numericL = new NumericUpDown();
            labelL = new Label();
            ((System.ComponentModel.ISupportInitialize)numericL).BeginInit();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(152, 86);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // numericL
            // 
            numericL.Location = new Point(107, 43);
            numericL.Name = "numericL";
            numericL.Size = new Size(120, 23);
            numericL.TabIndex = 1;
            // 
            // labelL
            // 
            labelL.AutoSize = true;
            labelL.Location = new Point(27, 45);
            labelL.Name = "labelL";
            labelL.Size = new Size(47, 15);
            labelL.TabIndex = 2;
            labelL.Text = "L value:";
            // 
            // FormL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 121);
            Controls.Add(labelL);
            Controls.Add(numericL);
            Controls.Add(buttonStart);
            Name = "FormL";
            Text = "Choose max L value";
            ((System.ComponentModel.ISupportInitialize)numericL).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private NumericUpDown numericL;
        private Label labelL;
    }
}