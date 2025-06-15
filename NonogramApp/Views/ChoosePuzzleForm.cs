using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NonogramApp
{
    public partial class ChoosePuzzleForm : Form
    {
        // Stores the selected grid size for the puzzle
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedGridSize { get; private set; } = 10;

        public ChoosePuzzleForm()
        {
            InitializeComponent();

            // Fill the difficulty dropdown with options
            cmbDifficulty.Items.Clear();
            cmbDifficulty.Items.Add("Easy (5x5)");
            cmbDifficulty.Items.Add("Medium (10x10)");
            cmbDifficulty.Items.Add("Hard (15x15)");
            cmbDifficulty.SelectedIndex = 1; // Default to Medium

            SetButtonHoverEffects();
        }

        // Start button: open the puzzle with the chosen size
        private void btnStartPuzzle_Click(object sender, EventArgs e)
        {
            switch (cmbDifficulty.SelectedIndex)
            {
                case 0: SelectedGridSize = 5; break;
                case 1: SelectedGridSize = 10; break;
                case 2: SelectedGridSize = 15; break;
                default: SelectedGridSize = 10; break;
            }

            // Open the puzzle form and close this one when done
            this.Hide();
            var puzzleForm = new Views.PuzzleForm(SelectedGridSize);
            puzzleForm.FormClosed += (s, args) => this.Close();
            puzzleForm.Show();
        }

        // Cancel button: just close the form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Called when the difficulty selection changes (not used here)
        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // You can add logic here if needed
        }

        // Remove focus from controls when the form loads
        private void ChoosePuzzleForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Adds a light effect when hovering over the buttons
        private void SetButtonHoverEffects()
        {
            foreach (var btn in new[] { btnStartPuzzle, btnCancel })
            {
                btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(btn.BackColor, 0.15f);
                btn.MouseLeave += (s, e) => btn.BackColor = GetButtonBaseColor(btn);
            }
        }

        // Returns the base color for each button
        private Color GetButtonBaseColor(Button btn)
        {
            if (btn == btnStartPuzzle) return Color.FromArgb(80, 180, 120);
            if (btn == btnCancel) return Color.FromArgb(120, 120, 120);
            return btn.BackColor;
        }
    }
}