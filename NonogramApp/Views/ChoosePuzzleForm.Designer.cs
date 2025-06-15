namespace NonogramApp
{
    partial class ChoosePuzzleForm
    {
        private System.ComponentModel.IContainer components = null;

        // Clean up any resources being used
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Sets up the layout and controls for the form
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainPanel = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblDifficulty = new Label();
            cmbDifficulty = new ComboBox();
            btnStartPuzzle = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // Main form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.MinimumSize = new System.Drawing.Size(420, 320);
            this.Name = "ChoosePuzzleForm";
            this.Text = "Choose Puzzle";
            this.Load += ChoosePuzzleForm_Load;

            // Main panel for content
            mainPanel.BackColor = System.Drawing.Color.FromArgb(235, 238, 245);
            mainPanel.Size = new System.Drawing.Size(400, 260);
            mainPanel.Location = new System.Drawing.Point(50, 40);
            mainPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Title at the top
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            lblTitle.Location = new System.Drawing.Point(0, 10);
            lblTitle.Size = new System.Drawing.Size(400, 40);
            lblTitle.Text = "Nonogram";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Subtitle under the title
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            lblSubtitle.Location = new System.Drawing.Point(0, 50);
            lblSubtitle.Size = new System.Drawing.Size(400, 30);
            lblSubtitle.Text = "Choose your puzzle difficulty";
            lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Label for the difficulty dropdown
            lblDifficulty.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblDifficulty.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            lblDifficulty.Location = new System.Drawing.Point(60, 95);
            lblDifficulty.Size = new System.Drawing.Size(120, 30);
            lblDifficulty.Text = "Difficulty:";
            lblDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Dropdown for selecting difficulty
            cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDifficulty.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cmbDifficulty.Location = new System.Drawing.Point(180, 95);
            cmbDifficulty.Size = new System.Drawing.Size(160, 32);
            cmbDifficulty.TabIndex = 0;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged;

            // Start button
            btnStartPuzzle.BackColor = System.Drawing.Color.FromArgb(80, 180, 120);
            btnStartPuzzle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnStartPuzzle.FlatAppearance.BorderSize = 0;
            btnStartPuzzle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnStartPuzzle.ForeColor = System.Drawing.Color.White;
            btnStartPuzzle.Location = new System.Drawing.Point(60, 170);
            btnStartPuzzle.Size = new System.Drawing.Size(120, 40);
            btnStartPuzzle.Text = "Start";
            btnStartPuzzle.TabIndex = 1;
            btnStartPuzzle.Click += btnStartPuzzle_Click;

            // Cancel button
            btnCancel.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(220, 170);
            btnCancel.Size = new System.Drawing.Size(120, 40);
            btnCancel.Text = "Cancel";
            btnCancel.TabIndex = 2;
            btnCancel.Click += btnCancel_Click;

            // Add controls to the main panel
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(lblSubtitle);
            mainPanel.Controls.Add(lblDifficulty);
            mainPanel.Controls.Add(cmbDifficulty);
            mainPanel.Controls.Add(btnStartPuzzle);
            mainPanel.Controls.Add(btnCancel);

            // Add the main panel to the form
            Controls.Add(mainPanel);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel mainPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblDifficulty;
        private ComboBox cmbDifficulty;
        private Button btnStartPuzzle;
        private Button btnCancel;
    }
}