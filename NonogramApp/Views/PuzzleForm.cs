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
        private int clueMargin = 70; // space around grid for clues

        // Cell states: 0 = empty, 1 = filled, 2 = marked with X
        private int[,] cellStates;
        private int[][] solution;

        // Store wrong and correct cells after check to highlight them
        private HashSet<(int x, int y)> wrongCells = new HashSet<(int x, int y)>();
        private HashSet<(int x, int y)> correctCells = new HashSet<(int x, int y)>();


        // Clues to help solve puzzle
        private List<List<int>> rowClues = new List<List<int>>();
        private List<List<int>> colClues = new List<List<int>>();

        private bool showSolutionOverlay = false;

        // Timer to track elapsed time
        private System.Windows.Forms.Timer gameTimer; // this will tick every second
        private int elapsedSeconds = 0; // Keep track of time passed

        public PuzzleForm()
        {
            InitializeComponent();
            this.ActiveControl = null; // remove initial focus

            // Initialize the grid with empty cells
            cellStates = new int[gridSize, gridSize];

            // Initialize the solution array
            solution = new int[gridSize][];
            for (int i = 0; i < gridSize; i++)
            {
                solution[i] = new int[gridSize];
            }

            // Generate a random solution and clues
            RandomizeSolution();
            GenerateClues();

            // Start the timer 
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // Tick every second
            gameTimer.Tick += GameTimer_Tick; 
            gameTimer.Start(); 
        }

        // Update the timer label every second
        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            elapsedSeconds++; // Add a second
            int minutes = elapsedSeconds / 60;
            int seconds = elapsedSeconds % 60;

            // Update the label with current time
            lblTimer.Text = $"Time: {minutes:D2}:{seconds:D2}";
        }

        // Generate a random puzzle solution grid
        private void RandomizeSolution()
        {
            Random rand = new Random();
            // Generate a random solution and clues
            solution = new int[gridSize][]; // 
            for (int row = 0; row < gridSize; row++)
            {
                solution[row] = new int[gridSize];
                for (int col = 0; col < gridSize; col++)
                {
                    // Random fill with 30% chance of being filled 
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
                if (clues.Count == 0) clues.Add(0); // No filled cells in row
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
                if (clues.Count == 0) clues.Add(0); // No filled cells in column
                colClues.Add(clues);
            }
        }

        private void PuzzleForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
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

                    // Check if this cell is wrong or correct after check, and highlight colors
                    if (wrongCells.Contains((col, row)))
                    {
                        Rectangle rect = new Rectangle(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.Red, rect);
                        using (Pen pen = new Pen(Color.DarkRed, 2))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                        continue; // Skip further drawing this cell
                    }
                    else if (correctCells.Contains((col, row)))
                    {
                        Rectangle rect = new Rectangle(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.LightGreen, rect);
                        using (Pen pen = new Pen(Color.Green, 2))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                        continue; // Skip further drawing this cell
                    }


                    if (state == 1)
                    {
                        // Draw filled cell with white border for better visibility
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
                        int y = clueMargin - (clues.Count - i) * lineHeight - 5; // Fix y position to start above the grid

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

                Invalidate(); // Redraw the form to show changes
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

            this.ActiveControl = null; // Remove initial focus
            wrongCells.Clear(); // Clear previous highlights
            correctCells.Clear(); // Clear correct cells
            showSolutionOverlay = false; // Zorg dat oplossing niet zichtbaar blijft
            Invalidate(); // Refresh the screen after reset
            elapsedSeconds = 0; // Reset timer
        }

        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            CheckSolution();
        }

        // Button to clear the red/green mistake highlights but keep player input
        private void btnHideMistakes_Click(object sender, EventArgs e)
        {
            // Clear both sets that highlight mistakes and correct answers
            wrongCells.Clear();
            correctCells.Clear();

            // Refresh the screen to remove highlights
            Invalidate(); // Redraws the form
        }


        private async void btnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate(); // Show solution overlay on grid
            await Task.Delay(4000); // Wait 4 seconds showing solution
            showSolutionOverlay = false;
            Invalidate(); // Hide overlay and return to player view
        }

        //  Hint button click handler 
        private void btnHint_Click(object sender, EventArgs e)
        {
            // Find the first empty cell that should be filled according to the solution
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (cellStates[col, row] != 1 && solution[row][col] == 1)
                    {
                        cellStates[col, row] = 1; // Fill that cell as hint
                        Invalidate(); // Redraw to show change
                        return; // Only one hint at a time
                    }
                }
            }
            // If no hints available tells the user
            MessageBox.Show("No more hints available!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CheckSolution()
        {
            // Clear previous highlights
            wrongCells.Clear();
            correctCells.Clear();

            bool hasError = false;

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (cellStates[x, y] == 1 && solution[y][x] != 1)
                    {
                        // This cell is wrongly filled -> mark red
                        wrongCells.Add((x, y));
                        hasError = true;
                    }
                    else if (cellStates[x, y] == 1 && solution[y][x] == 1)
                    {
                        // Correctly filled cell -> mark green
                        correctCells.Add((x, y));
                    }
                    else if (cellStates[x, y] != 1 && solution[y][x] == 1)
                    {
                        // Cell should be filled but isn't -> mark red
                        wrongCells.Add((x, y));
                        hasError = true;
                    }
                }
            }

            // Redraw grid to show highlights
            Invalidate();

            if (hasError)
            {
                MessageBox.Show("There are mistakes in your solution. Check red cells!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Well done! Puzzle solved correctly!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
