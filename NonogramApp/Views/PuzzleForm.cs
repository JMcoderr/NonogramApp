using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NonogramApp.Views
{
    public partial class PuzzleForm : Form
    {
        // Basic puzzle settings
        private int gridSize = 10; // 10x10 grid
        private int cellSize = 30; // pixel size of each square
        private int clueMargin = 50; // space around grid for clues

        // Cell states: 0 = empty, 1 = filled, 2 = marked with X
        private int[,] cellStates;
        private int[][] solution;

        // Clues to help solve puzzle
        private List<List<int>> rowClues = new List<List<int>>();
        private List<List<int>> colClues = new List<List<int>>();

        private bool showSolutionOverlay = false;

        public PuzzleForm()
        {
            InitializeComponent();

            // Initialize the grid with empty cells
            cellStates = new int[gridSize, gridSize];

            // Hardcoded solution for now
            solution = new int[][]
            {
                new int[] {0,0,1,1,0,0,1,0,0,0},
                new int[] {1,1,1,0,0,0,0,0,0,0},
                new int[] {1,1,0,1,0,0,0,0,0,0},
                new int[] {1,1,1,1,0,0,0,0,0,0},
                new int[] {1,0,1,0,0,0,0,0,0,0},
                new int[] {1,1,1,0,0,0,0,0,0,0},
                new int[] {1,1,0,1,0,0,0,0,0,0},
                new int[] {1,0,0,0,0,0,0,0,0,0},
                new int[] {1,1,1,1,1,0,0,0,0,0},
                new int[] {1,1,0,0,0,0,0,0,0,0}
            };

            // Just some example clues, not exact
            rowClues = new List<List<int>>
            {
                new List<int> {1, 2},
                new List<int> {3},
                new List<int> {2, 1},
                new List<int> {4},
                new List<int> {1, 1},
                new List<int> {3},
                new List<int> {2, 2},
                new List<int> {1},
                new List<int> {5},
                new List<int> {2}
            };

            colClues = new List<List<int>>
            {
                new List<int> {2},
                new List<int> {1, 1},
                new List<int> {3},
                new List<int> {1, 2},
                new List<int> {2},
                new List<int> {4},
                new List<int> {1, 1},
                new List<int> {3},
                new List<int> {2},
                new List<int> {1}
            };
        }

        private void PuzzleForm_Load(object sender, EventArgs e)
        {
            // (Optional) code when the form first loads
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.Black);

            // Loop through each cell
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int x = clueMargin + col * cellSize + 1;
                    int y = clueMargin + row * cellSize + 1;

                    int state = showSolutionOverlay ? solution[row][col] : cellStates[col, row];

                    if (state == 1)
                    {
                        // Draw filled cell (black)
                        g.FillRectangle(Brushes.Black, x, y, cellSize - 1, cellSize - 1);
                    }
                    else if (state == 2)
                    {
                        // Draw red X
                        g.DrawLine(Pens.Red, x, y, x + cellSize - 2, y + cellSize - 2);
                        g.DrawLine(Pens.Red, x + cellSize - 2, y, x, y + cellSize - 2);
                    }
                    // state == 0 is empty, so we draw nothing
                }
            }

            // Draw grid lines
            for (int i = 0; i <= gridSize; i++)
            {
                // Vertical
                g.DrawLine(gridPen,
                    clueMargin + i * cellSize, clueMargin,
                    clueMargin + i * cellSize, clueMargin + gridSize * cellSize);

                // Horizontal
                g.DrawLine(gridPen,
                    clueMargin, clueMargin + i * cellSize,
                    clueMargin + gridSize * cellSize, clueMargin + i * cellSize);
            }

            // Draw row clues
            for (int row = 0; row < gridSize; row++)
            {
                if (row < rowClues.Count)
                {
                    string clue = string.Join(" ", rowClues[row]);
                    g.DrawString(clue, this.Font, Brushes.Black,
                        5, clueMargin + row * cellSize + 5);
                }
            }

            // Draw column clues
            for (int col = 0; col < gridSize; col++)
            {
                if (col < colClues.Count)
                {
                    string clue = string.Join("\n", colClues[col]);
                    g.DrawString(clue, this.Font, Brushes.Black,
                        clueMargin + col * cellSize + 5, 5);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Only allow clicks inside the grid
            if (e.X < clueMargin || e.Y < clueMargin)
                return;

            int x = (e.X - clueMargin) / cellSize;
            int y = (e.Y - clueMargin) / cellSize;

            if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Left click = toggle between empty and filled
                    cellStates[x, y] = (cellStates[x, y] == 1) ? 0 : 1;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // Right click = toggle X mark
                    cellStates[x, y] = (cellStates[x, y] == 2) ? 0 : 2;
                }

                Invalidate(); // Redraw form
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Set all cells back to empty
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    cellStates[x, y] = 0;
                }
            }

            Invalidate(); // Refresh screen
        }

        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            CheckSolution();
        }

        private async void btnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate(); // Show solution overlay
            await Task.Delay(4000); // Wait a few seconds
            showSolutionOverlay = false;
            Invalidate(); // Hide overlay again
        }

        private void CheckSolution()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    // Check if student filled a wrong cell
                    if (cellStates[x, y] == 1 && solution[y][x] != 1)
                    {
                        MessageBox.Show("Incorrect: wrong cells are filled.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check if student missed a correct cell
                    if (cellStates[x, y] != 1 && solution[y][x] == 1)
                    {
                        MessageBox.Show("Incorrect: some filled cells are missing.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // If nothing is wrong, student solved the puzzle
            MessageBox.Show("Well done! Puzzle solved correctly!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
