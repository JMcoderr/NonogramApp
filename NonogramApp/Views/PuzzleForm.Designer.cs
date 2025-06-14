namespace NonogramApp.Views
{
    partial class PuzzleForm
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
            btnReset = new Button();
            btnCheckSolution = new Button();
            btnShowSolution = new Button();
            SuspendLayout();
            // 
            // btnReset
            // 
            btnReset.Location = new Point(534, 105);
            btnReset.Margin = new Padding(3, 2, 3, 2);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(82, 22);
            btnReset.TabIndex = 0;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnCheckSolution
            // 
            btnCheckSolution.Location = new Point(534, 152);
            btnCheckSolution.Margin = new Padding(3, 2, 3, 2);
            btnCheckSolution.Name = "btnCheckSolution";
            btnCheckSolution.Size = new Size(82, 22);
            btnCheckSolution.TabIndex = 1;
            btnCheckSolution.Text = "Check";
            btnCheckSolution.UseVisualStyleBackColor = true;
            btnCheckSolution.Click += btnCheckSolution_Click;
            // 
            // btnShowSolution
            // 
            btnShowSolution.Location = new Point(534, 206);
            btnShowSolution.Name = "btnShowSolution";
            btnShowSolution.Size = new Size(93, 23);
            btnShowSolution.TabIndex = 2;
            btnShowSolution.Text = "Show Solution";
            btnShowSolution.UseVisualStyleBackColor = true;
            btnShowSolution.Click += btnShowSolution_Click;
            // 
            // PuzzleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(btnShowSolution);
            Controls.Add(btnCheckSolution);
            Controls.Add(btnReset);
            Name = "PuzzleForm";
            Text = "PuzzleForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnReset;
        private Button btnCheckSolution;
        private Button btnShowSolution;
    }
}