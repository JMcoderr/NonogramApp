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
            btnCheckSolution = new Button();
            btnShowSolution = new Button();
            btnHint = new Button();
            lblTimer = new Label();
            btnHideMistakes = new Button();
            btnReset = new Button();
            lblScore = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnCheckSolution
            // 
            btnCheckSolution.Location = new Point(574, 233);
            btnCheckSolution.Margin = new Padding(3, 2, 3, 2);
            btnCheckSolution.Name = "btnCheckSolution";
            btnCheckSolution.Size = new Size(93, 22);
            btnCheckSolution.TabIndex = 1;
            btnCheckSolution.Text = "Check";
            btnCheckSolution.UseVisualStyleBackColor = true;
            btnCheckSolution.Click += btnCheckSolution_Click;
            // 
            // btnShowSolution
            // 
            btnShowSolution.Location = new Point(628, 312);
            btnShowSolution.Name = "btnShowSolution";
            btnShowSolution.Size = new Size(93, 23);
            btnShowSolution.TabIndex = 2;
            btnShowSolution.Text = "Show Solution";
            btnShowSolution.UseVisualStyleBackColor = true;
            btnShowSolution.Click += btnShowSolution_Click;
            // 
            // btnHint
            // 
            btnHint.Location = new Point(673, 273);
            btnHint.Name = "btnHint";
            btnHint.Size = new Size(90, 23);
            btnHint.TabIndex = 3;
            btnHint.Text = "Hint";
            btnHint.UseVisualStyleBackColor = true;
            btnHint.Click += btnHint_Click;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(628, 177);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(67, 15);
            lblTimer.TabIndex = 4;
            lblTimer.Text = "Time: 00:00";
            lblTimer.Click += lblTimer_Click;
            // 
            // btnHideMistakes
            // 
            btnHideMistakes.Location = new Point(673, 232);
            btnHideMistakes.Name = "btnHideMistakes";
            btnHideMistakes.Size = new Size(90, 23);
            btnHideMistakes.TabIndex = 5;
            btnHideMistakes.Text = "Hide Mistakes";
            btnHideMistakes.UseVisualStyleBackColor = true;
            btnHideMistakes.Click += btnHideMistakes_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(574, 273);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(93, 23);
            btnReset.TabIndex = 6;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(638, 192);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(48, 15);
            lblScore.TabIndex = 7;
            lblScore.Text = "Score: 0";
            // 
            // button1
            // 
            button1.Location = new Point(704, 14);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // PuzzleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(button1);
            Controls.Add(lblScore);
            Controls.Add(btnReset);
            Controls.Add(btnHideMistakes);
            Controls.Add(lblTimer);
            Controls.Add(btnHint);
            Controls.Add(btnShowSolution);
            Controls.Add(btnCheckSolution);
            Name = "PuzzleForm";
            Text = "PuzzleForm";
            Load += PuzzleForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCheckSolution;
        private Button btnShowSolution;
        private Button btnHint;
        private Label lblTimer;
        private Button btnHideMistakes;
        private Button btnReset;
        private Label lblScore;
        private Button button1;
    }
}