namespace NonogramApp
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
            btnStartPuzzle = new CheckBox();
            cmbDifficulty = new ComboBox();
            SuspendLayout();
            // 
            // btnStartPuzzle
            // 
            btnStartPuzzle.AutoSize = true;
            btnStartPuzzle.Location = new Point(346, 153);
            btnStartPuzzle.Name = "btnStartPuzzle";
            btnStartPuzzle.Size = new Size(86, 19);
            btnStartPuzzle.TabIndex = 0;
            btnStartPuzzle.Text = "Start Puzzle";
            btnStartPuzzle.UseVisualStyleBackColor = true;
            btnStartPuzzle.Click += btnStartPuzzle_Click;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Items.AddRange(new object[] { "Easy (5x5)", "Medium (10x10)", "Hard (15x15)" });
            cmbDifficulty.Location = new Point(333, 178);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(121, 23);
            cmbDifficulty.TabIndex = 1;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbDifficulty);
            Controls.Add(btnStartPuzzle);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox btnStartPuzzle;
        private ComboBox cmbDifficulty;
    }
}
