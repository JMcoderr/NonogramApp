namespace NonogramApp.Views
{
    partial class PuzzleForm
    {
        private System.ComponentModel.IContainer components = null;

        // This cleans up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // This method sets up all the controls and layout for the form.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnCheckSolution = new Button();
            btnShowSolution = new Button();
            btnHint = new Button();
            lblTimer = new Label();
            btnHideMistakes = new Button();
            btnReset = new Button();
            lblScore = new Label();
            btnSettings = new Button();
            settingsPanel = new Panel();
            chkEnableScore = new CheckBox();
            chkAllowHints = new CheckBox();
            rightPanel = new Panel();
            lblHintsUsed = new Label();

            SuspendLayout();

            //  Main Form settings 
            // Sets the size, background color, and other main properties of the window.
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.ClientSize = new Size(1000, 700);
            this.MinimumSize = new Size(900, 600);
            this.Name = "PuzzleForm";
            this.Text = "Nonogram Puzzle";
            this.Load += PuzzleForm_Load_1;

            // -Right panel 
            // This panel holds all the controls (buttons, labels, etc.) on the right side.
            rightPanel.BackColor = Color.FromArgb(235, 238, 245);
            rightPanel.Location = new Point(820, 0);
            rightPanel.Size = new Size(180, 700);
            rightPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            // Timer label
            // Shows the elapsed time.
            lblTimer.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            lblTimer.ForeColor = Color.FromArgb(60, 60, 60);
            lblTimer.Location = new Point(20, 30);
            lblTimer.Size = new Size(140, 35);
            lblTimer.Text = "Time: 00:00";
            lblTimer.Click += lblTimer_Click;
            lblTimer.TextAlign = ContentAlignment.MiddleLeft;

            // Score label 
            // Shows the current score.
            lblScore.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            lblScore.ForeColor = Color.FromArgb(60, 60, 60);
            lblScore.Location = new Point(20, 70);
            lblScore.Size = new Size(140, 35);
            lblScore.Text = "Score: 0";
            lblScore.TextAlign = ContentAlignment.MiddleLeft;

            // Hints used label 
            // Shows how many hints the player has used.
            lblHintsUsed.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point);
            lblHintsUsed.ForeColor = Color.FromArgb(120, 120, 120);
            lblHintsUsed.Location = new Point(20, 110);
            lblHintsUsed.Size = new Size(140, 25);
            lblHintsUsed.Text = "Hints used: 0";
            lblHintsUsed.TextAlign = ContentAlignment.MiddleLeft;

            // Settings button
            // Opens the settings panel.
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.BackColor = Color.FromArgb(230, 230, 240);
            btnSettings.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnSettings.ForeColor = Color.FromArgb(60, 60, 60);
            btnSettings.Location = new Point(20, 140);
            btnSettings.Size = new Size(140, 40);
            btnSettings.Text = "⚙ Settings";
            btnSettings.TabIndex = 8;
            btnSettings.Click += BtnSettings_Click;

            // Settings panel
            // This panel pops up when you click the settings button.
            settingsPanel.BackColor = Color.White;
            settingsPanel.BorderStyle = BorderStyle.FixedSingle;
            settingsPanel.Location = new Point(10, 190);
            settingsPanel.Size = new Size(160, 100);
            settingsPanel.Visible = false;
            settingsPanel.Padding = new Padding(10);

            // Score checkbox
            // Lets the user turn scoring on or off.
            chkEnableScore.Text = "Enable Score";
            chkEnableScore.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkEnableScore.Location = new Point(20, 15);
            chkEnableScore.Size = new Size(120, 28);
            chkEnableScore.Checked = false;
            chkEnableScore.Enabled = false;

            // Hints checkbox
            // Lets the user turn hints on or off.
            chkAllowHints.Text = "Allow Hints";
            chkAllowHints.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chkAllowHints.Location = new Point(20, 55);
            chkAllowHints.Size = new Size(120, 28);
            chkAllowHints.Checked = false;
            chkAllowHints.Enabled = false;

            // Add checkboxes to the settings panel
            settingsPanel.Controls.Add(chkEnableScore);
            settingsPanel.Controls.Add(chkAllowHints);

            // Main action buttons
            // These buttons let the user interact with the puzzle 
            int buttonStartY = 350; // Start lower so settings panel has space
            int buttonSpacing = 55;

            // Check solution button
            btnCheckSolution.BackColor = Color.FromArgb(80, 180, 120);
            btnCheckSolution.FlatStyle = FlatStyle.Flat;
            btnCheckSolution.FlatAppearance.BorderSize = 0;
            btnCheckSolution.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnCheckSolution.ForeColor = Color.White;
            btnCheckSolution.Location = new Point(20, buttonStartY);
            btnCheckSolution.Size = new Size(140, 40);
            btnCheckSolution.Text = "Check";
            btnCheckSolution.TabIndex = 1;
            btnCheckSolution.Click += btnCheckSolution_Click;

            // Show solution button
            btnShowSolution.BackColor = Color.FromArgb(80, 120, 200);
            btnShowSolution.FlatStyle = FlatStyle.Flat;
            btnShowSolution.FlatAppearance.BorderSize = 0;
            btnShowSolution.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnShowSolution.ForeColor = Color.White;
            btnShowSolution.Location = new Point(20, buttonStartY + buttonSpacing);
            btnShowSolution.Size = new Size(140, 40);
            btnShowSolution.Text = "Show Solution";
            btnShowSolution.TabIndex = 2;
            btnShowSolution.Click += btnShowSolution_Click;

            // Hint button
            btnHint.BackColor = Color.FromArgb(240, 180, 60);
            btnHint.FlatStyle = FlatStyle.Flat;
            btnHint.FlatAppearance.BorderSize = 0;
            btnHint.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnHint.ForeColor = Color.White;
            btnHint.Location = new Point(20, buttonStartY + buttonSpacing * 2);
            btnHint.Size = new Size(140, 40);
            btnHint.Text = "Hint";
            btnHint.TabIndex = 3;
            btnHint.Click += btnHint_Click;
            btnHint.Enabled = false;

            // Hide mistakes button
            btnHideMistakes.BackColor = Color.FromArgb(220, 80, 80);
            btnHideMistakes.FlatStyle = FlatStyle.Flat;
            btnHideMistakes.FlatAppearance.BorderSize = 0;
            btnHideMistakes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnHideMistakes.ForeColor = Color.White;
            btnHideMistakes.Location = new Point(20, buttonStartY + buttonSpacing * 3);
            btnHideMistakes.Size = new Size(140, 40);
            btnHideMistakes.Text = "Hide Mistakes";
            btnHideMistakes.TabIndex = 5;
            btnHideMistakes.Click += btnHideMistakes_Click;

            // Reset button
            btnReset.BackColor = Color.FromArgb(120, 120, 120);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(20, buttonStartY + buttonSpacing * 4);
            btnReset.Size = new Size(140, 40);
            btnReset.Text = "Reset";
            btnReset.TabIndex = 6;
            btnReset.Click += btnReset_Click;

            // Add everything to the right panel
            rightPanel.Controls.Add(lblTimer);
            rightPanel.Controls.Add(lblScore);
            rightPanel.Controls.Add(lblHintsUsed);
            rightPanel.Controls.Add(btnSettings);
            rightPanel.Controls.Add(settingsPanel);
            rightPanel.Controls.Add(btnCheckSolution);
            rightPanel.Controls.Add(btnShowSolution);
            rightPanel.Controls.Add(btnHint);
            rightPanel.Controls.Add(btnHideMistakes);
            rightPanel.Controls.Add(btnReset);

            // Add the right panel to the form
            Controls.Add(rightPanel);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // These are all the controls used in the form.
        private Button btnCheckSolution;
        private Button btnShowSolution;
        private Button btnHint;
        private Label lblTimer;
        private Button btnHideMistakes;
        private Button btnReset;
        private Label lblScore;
        private Button btnSettings;
        private Panel settingsPanel;
        private CheckBox chkEnableScore;
        private CheckBox chkAllowHints;
        private Panel rightPanel;
        private Label lblHintsUsed;
    }
}