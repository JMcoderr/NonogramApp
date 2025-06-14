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
        private int clueMargin = 70; // space around grid for clues (increased from 50 to 70 to prevent overlap)

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

            // Initialize the solution array to avoid nullability issues
            solution = new int[gridSize][];
            for (int i = 0; i < gridSize; i++)
            {
                solution[i] = new int[gridSize];
            }

            // Generate a random solution and corresponding clues
            RandomizeSolution();
            GenerateClues();
        }

        // Generate a random puzzle solution grid
        private void RandomizeSolution()
        {
            Random rand = new Random();
            // Generate a random solution and corresponding clues
            solution = new int[gridSize][]; // Ensure 'solution' is initialized
            for (int row = 0; row < gridSize; row++)
            {
                solution[row] = new int[gridSize];
                for (int col = 0; col < gridSize; col++)
                {
                    // Random fill with 30% chance of being filled (1)
                    solution[row][col] = (rand.NextDouble() < 0.3) ? 1 : 0;
                }
            }
        }

        // Generate row and column clues based on current solution
        private void GenerateClues()
        {
            rowClues = new List<List<int>>();
            colClues = new List<List<int>>();

            // Generate row clues
            for (int row = 0; row < gridSize; row++)
            {
                List<int> clues = new List<int>();
                int count = 0;
                for (int col = 0; col < gridSize; col++)
                {
                    if (solution[row][col] == 1)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            clues.Add(count);
                            count = 0;
                        }
                    }
                }
                if (count > 0) clues.Add(count);
                if (clues.Count == 0) clues.Add(0); // no filled cells in row
                rowClues.Add(clues);
            }

            // Generate column clues
            for (int col = 0; col < gridSize; col++)
            {
                List<int> clues = new List<int>();
                int count = 0;
                for (int row = 0; row < gridSize; row++)
                {
                    if (solution[row][col] == 1)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            clues.Add(count);
                            count = 0;
                        }
                    }
                }
                if (count > 0) clues.Add(count);
                if (clues.Count == 0) clues.Add(0); // no filled cells in column
                colClues.Add(clues);
            }
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

                    // Choose state: solution overlay or player input
                    int state = showSolutionOverlay ? solution[row][col] : cellStates[col, row];

                    if (state == 1)
                    {
                        // Draw filled cell (black) with white border for better visibility
                        Rectangle rect = new Rectangle(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.Black, rect);
                        using (Pen whitePen = new Pen(Color.White, 2))
                        {
                            g.DrawRectangle(whitePen, rect);
                        }
                    }
                    else if (state == 2)
                    {
                        // Draw red X mark
                        g.DrawLine(Pens.Red, x, y, x + cellSize - 2, y + cellSize - 2);
                        g.DrawLine(Pens.Red, x + cellSize - 2, y, x, y + cellSize - 2);
                    }
                    // state == 0 is empty, so no fill or marks
                }
            }

            // Draw grid lines
            for (int i = 0; i <= gridSize; i++)
            {
                // Vertical grid lines
                g.DrawLine(gridPen,
                    clueMargin + i * cellSize, clueMargin,
                    clueMargin + i * cellSize, clueMargin + gridSize * cellSize);

                // Horizontal grid lines
                g.DrawLine(gridPen,
                    clueMargin, clueMargin + i * cellSize,
                    clueMargin + gridSize * cellSize, clueMargin + i * cellSize);
            }

            // Draw row clues on the left
            for (int row = 0; row < gridSize; row++)
            {
                if (row < rowClues.Count)
                {
                    string clue = string.Join(" ", rowClues[row]);
                    g.DrawString(clue, this.Font, Brushes.Black,
                        5, clueMargin + row * cellSize + 5);
                }
            }

            // Draw column clues on top vertically, with vertical stacking
            DrawColumnClues(g);
        }

        // Draw column clues, each number stacked vertically
        private void DrawColumnClues(Graphics g)
        {
            int lineHeight = (int)g.MeasureString("A", this.Font).Height;

            for (int col = 0; col < gridSize; col++)
            {
                if (col < colClues.Count)
                {
                    List<int> clues = colClues[col];
                    for (int i = 0; i < clues.Count; i++)
                    {
                        string clueStr = clues[i].ToString();
                        int x = clueMargin + col * cellSize + 5;

                        // Adjust y to start from the top margin, stack clues downwards
                        int y = clueMargin - (clues.Count - i) * lineHeight - 5; // fixed so clues sit above grid without overlap

                        g.DrawString(clueStr, this.Font, Brushes.Black, x, y);
                    }
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Only allow clicks inside the grid area
            if (e.X < clueMargin || e.Y < clueMargin)
                return;

            int x = (e.X - clueMargin) / cellSize;
            int y = (e.Y - clueMargin) / cellSize;

            if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Left click toggles between empty and filled
                    cellStates[x, y] = (cellStates[x, y] == 1) ? 0 : 1;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // Right click toggles X mark
                    cellStates[x, y] = (cellStates[x, y] == 2) ? 0 : 2;
                }

                Invalidate(); // Redraw the form to update visuals
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset all cells to empty state
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    cellStates[x, y] = 0;
                }
            }

            Invalidate(); // Refresh the screen after reset
        }

        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            CheckSolution();
        }

        private async void btnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate(); // Show solution overlay on grid
            await Task.Delay(4000); // Wait 4 seconds showing solution
            showSolutionOverlay = false;
            Invalidate(); // Hide overlay and return to player view
        }

        private void CheckSolution()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    // If player filled a cell that should be empty, show warning
                    if (cellStates[x, y] == 1 && solution[y][x] != 1)
                    {
                        MessageBox.Show("Incorrect: wrong cells are filled.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // If player missed a cell that should be filled, show warning
                    if (cellStates[x, y] != 1 && solution[y][x] == 1)
                    {
                        MessageBox.Show("Incorrect: some filled cells are missing.", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // If no issues found, player solved the puzzle correctly
            MessageBox.Show("Well done! Puzzle solved correctly!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
