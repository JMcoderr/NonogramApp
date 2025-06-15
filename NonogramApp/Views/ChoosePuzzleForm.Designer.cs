namespace NonogramApp
{
    partial class ChoosePuzzleForm
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
            this.btnStartPuzzle = new System.Windows.Forms.Button();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnStartPuzzle
            // 
            this.btnStartPuzzle.Location = new System.Drawing.Point(346, 153);
            this.btnStartPuzzle.Name = "btnStartPuzzle";
            this.btnStartPuzzle.Size = new System.Drawing.Size(86, 23);
            this.btnStartPuzzle.TabIndex = 0;
            this.btnStartPuzzle.Text = "Start Puzzle";
            this.btnStartPuzzle.UseVisualStyleBackColor = true;
            this.btnStartPuzzle.Click += new System.EventHandler(this.btnStartPuzzle_Click);
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Easy (5x5)",
            "Medium (10x10)",
            "Hard (15x15)"});
            this.cmbDifficulty.Location = new System.Drawing.Point(333, 178);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(121, 23);
            this.cmbDifficulty.TabIndex = 1;
            this.cmbDifficulty.SelectedIndexChanged += new System.EventHandler(this.cmbDifficulty_SelectedIndexChanged);
            // 
            // ChoosePuzzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.btnStartPuzzle);
            this.Name = "ChoosePuzzleForm";
            this.Text = "Choose Puzzle Difficulty";
            this.Load += new System.EventHandler(this.ChoosePuzzleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartPuzzle;
        private System.Windows.Forms.ComboBox cmbDifficulty;
    }
}
