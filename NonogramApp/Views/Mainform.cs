using NonogramApp.Views;
using System;
using System.Windows.Forms;

namespace NonogramApp
{
    public partial class Form1 : Form
    {
        private int selectedGridSize;

        public Form1()
        {
            InitializeComponent();

            // Add options to the combobox
            cmbDifficulty.Items.Clear();
            cmbDifficulty.Items.Add("Easy (5x5)");
            cmbDifficulty.Items.Add("Medium (10x10)");
            cmbDifficulty.Items.Add("Hard (15x15)");

            // Set default selected index
            cmbDifficulty.SelectedIndex = 1; // Medium as default
        }

        private void btnStartPuzzle_Click(object sender, EventArgs e)
        {
            string? difficulty = cmbDifficulty.SelectedItem?.ToString();

            if (difficulty == "Easy (5x5)")
                selectedGridSize = 5;
            else if (difficulty == "Medium (10x10)")
                selectedGridSize = 10;
            else if (difficulty == "Hard (15x15)")
                selectedGridSize = 15;
            else
                selectedGridSize = 10; // fallback

            // Create a new PuzzleForm with the selected grid size and show it
            PuzzleForm puzzleForm = new PuzzleForm(selectedGridSize);
            puzzleForm.Show();
        }

        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: do something when the user selects a different difficulty
        }
    }
}
