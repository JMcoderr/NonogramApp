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

            // Voeg opties toe aan de combobox
            cmbDifficulty.Items.Clear();
            cmbDifficulty.Items.Add("Easy (5x5)");
            cmbDifficulty.Items.Add("Medium (10x10)");
            cmbDifficulty.Items.Add("Hard (15x15)");

            // Zet standaard geselecteerde index
            cmbDifficulty.SelectedIndex = 1; // Medium als standaard
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

            // Maak nieuw PuzzleForm aan met geselecteerde grid size en toon deze
            PuzzleForm puzzleForm = new PuzzleForm(selectedGridSize);
            puzzleForm.Show();
        }

        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optioneel: iets doen als de gebruiker een andere moeilijkheid kiest
        }
    }
}
