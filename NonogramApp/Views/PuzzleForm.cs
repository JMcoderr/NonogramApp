using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NonogramApp.Views
{
    // This is the main window for the Nonogram puzzle game.
    public partial class PuzzleForm : Form
    {
        // Puzzle data 
        private int gridSize; // How many rows/columns the puzzle has
        private int cellSize; // Size of each cell in pixels
        private int clueMargin; // Space for clues on the top/left
        private int[,] cellStates; // Stores the state of each cell: 0 = empty, 1 = filled, 2 = X
        private int[][] solution; // The correct answer for the puzzle

        // Tracking cell status 
        private HashSet<(int x, int y)> wrongCells = new();   // Cells the user filled wrong
        private HashSet<(int x, int y)> correctCells = new(); // Cells the user filled right
        private HashSet<(int x, int y)> hintedCells = new();  // Cells filled by using a hint
        private HashSet<(int x, int y)> scoredCells = new();  // Tracks which correct cells have been scored

        // Clues for the puzzle 
        private List<List<int>> rowClues = new(); // Clues for each row
        private List<List<int>> colClues = new(); // Clues for each column

        // Game state 
        private bool showSolutionOverlay = false; // If true, show the solution on top of the grid
        private int score = 0;                    // The player's score
        private System.Windows.Forms.Timer gameTimer; // Timer for tracking how long the player takes
        private int elapsedSeconds = 0;           // How many seconds have passed
        private bool scoreEnabled = false;        // If true, scoring is turned on
        private bool hintsEnabled = false;        // If true, hints are allowed
        private int selectedRow = -1;             // For keyboard navigation: which row is selected
        private int selectedCol = -1;             // For keyboard navigation: which column is selected
        private int hintsUsed = 0;                // How many hints the player has used

        // Constructor: sets up the form and puzzle 
        public PuzzleForm(int gridSize)
        {
            InitializeComponent();

            // Enable double buffering for smooth drawing (no flicker)
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            InitializeSettingsPanelLogic();

            this.gridSize = gridSize;
            cellStates = new int[gridSize, gridSize];
            solution = new int[gridSize][];
            for (int i = 0; i < gridSize; i++)
                solution[i] = new int[gridSize];

            // Set the window size based on the puzzle size
            if (gridSize == 5)
                this.ClientSize = new Size(1000, 600);
            else if (gridSize == 10)
                this.ClientSize = new Size(1000, 700);
            else if (gridSize == 15)
                this.ClientSize = new Size(1000, 900);
            else
                this.ClientSize = new Size(1000, 700);

            this.MinimumSize = new Size(900, 600);
            this.ActiveControl = null; // No control is focused at start

            // Calculate cell sizes and margins, then make a new puzzle
            CalculateResponsiveSizes();
            RandomizeSolution();
            GenerateClues();

            // Set up the timer for the game
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // 1 second
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Handle resizing and keyboard input
            this.Resize += PuzzleForm_Resize;
            this.KeyPreview = true;
            this.KeyDown += PuzzleForm_KeyDown;

            // Set up tooltips and button hover effects for better UX
            SetToolTips();
            AddButtonHoverEffects();

            // By default, scoring and hints are ON (change this!)
            scoreEnabled = true;
            chkEnableScore.Checked = true;
            chkEnableScore.Enabled = true;
            chkAllowHints.Checked = false;
            chkAllowHints.Enabled = true;
            hintsEnabled = false;
            btnHint.Enabled = false;

            // Always show the score label at start
            UpdateScoreLabel();

            scoredCells.Clear();
        }

        // Adds tooltips to the buttons so the user knows what they do
        private void SetToolTips()
        {
            ToolTip tip = new();
            tip.SetToolTip(btnSettings, "Open settings panel");
            tip.SetToolTip(btnCheckSolution, "Check your solution");
            tip.SetToolTip(btnShowSolution, "Show the solution overlay");
            tip.SetToolTip(btnReset, "Reset the puzzle");
            tip.SetToolTip(btnHint, "Get a hint (if enabled)");
            tip.SetToolTip(btnHideMistakes, "Hide mistake highlights");
        }

        // Adds a light color effect when you hover over buttons
        private void AddButtonHoverEffects()
        {
            foreach (var btn in new[] { btnCheckSolution, btnShowSolution, btnHint, btnHideMistakes, btnReset, btnSettings })
            {
                btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(btn.BackColor, 0.15f);
                btn.MouseLeave += (s, e) => btn.BackColor = GetButtonBaseColor(btn);
            }
        }

        // Returns the original color for each button (used when mouse leaves)
        private Color GetButtonBaseColor(Button btn)
        {
            if (btn == btnCheckSolution) return Color.FromArgb(80, 180, 120);
            if (btn == btnShowSolution) return Color.FromArgb(80, 120, 200);
            if (btn == btnHint) return Color.FromArgb(240, 180, 60);
            if (btn == btnHideMistakes) return Color.FromArgb(220, 80, 80);
            if (btn == btnReset) return Color.FromArgb(120, 120, 120);
            if (btn == btnSettings) return Color.FromArgb(230, 230, 240);
            return btn.BackColor;
        }

        // Handles the logic for the settings panel checkboxes
        private void InitializeSettingsPanelLogic()
        {
            // When the score checkbox is changed, update the scoreEnabled flag and label
            chkEnableScore.CheckedChanged += (s, e) =>
            {
                scoreEnabled = chkEnableScore.Checked;
                UpdateScoreLabel(); // Always recalculate and update the score label
            };

            // When the hints checkbox is changed, enable or disable the hint button
            chkAllowHints.CheckedChanged += (s, e) =>
            {
                hintsEnabled = chkAllowHints.Checked;
                btnHint.Enabled = hintsEnabled;
            };
        }

        // Handles the click event for the settings button
        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            // Show or hide the settings panel when the settings button is clicked
            settingsPanel.Visible = !settingsPanel.Visible;
            if (settingsPanel.Visible)
                settingsPanel.BringToFront();
        }

        // Calculates the size of each cell and the margin for clues, based on the window size
        private void CalculateResponsiveSizes()
        {
            int availableWidth = this.ClientSize.Width - 180; // 180 is the right panel
            int availableHeight = this.ClientSize.Height;
            clueMargin = Math.Clamp(gridSize * 5 + 60, 60, 140);
            int maxCellWidth = (availableWidth - clueMargin - 20) / gridSize;
            int maxCellHeight = (availableHeight - clueMargin - 40) / gridSize;
            cellSize = Math.Max(18, Math.Min(maxCellWidth, maxCellHeight));
        }

        // When the window is resized, recalculate everything and redraw
        private void PuzzleForm_Resize(object? sender, EventArgs e)
        {
            CalculateResponsiveSizes();
            Invalidate();
        }

        // Called every second by the timer, updates the time label
        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            elapsedSeconds++;
            int minutes = elapsedSeconds / 60;
            int seconds = elapsedSeconds % 60;
            lblTimer.Text = $"Time: {minutes:D2}:{seconds:D2}";
        }

        // Makes a random solution for the puzzle (about 30% of cells are filled)
        private void RandomizeSolution()
        {
            Random rand = new();
            solution = new int[gridSize][];
            for (int row = 0; row < gridSize; row++)
            {
                solution[row] = new int[gridSize];
                for (int col = 0; col < gridSize; col++)
                {
                    solution[row][col] = (rand.NextDouble() < 0.3) ? 1 : 0;
                }
            }
        }

        // Generates the clues for each row and column based on the solution
        private void GenerateClues()
        {
            rowClues = new();
            colClues = new();

            // Row clues: count runs of filled cells
            for (int row = 0; row < gridSize; row++)
            {
                List<int> clues = new();
                int count = 0;
                for (int col = 0; col < gridSize; col++)
                {
                    if (solution[row][col] == 1)
                        count++;
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
                if (clues.Count == 0) clues.Add(0);
                rowClues.Add(clues);
            }

            // Column clues: same as above, but for columns
            for (int col = 0; col < gridSize; col++)
            {
                List<int> clues = new();
                int count = 0;
                for (int row = 0; row < gridSize; row++)
                {
                    if (solution[row][col] == 1)
                        count++;
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
                if (clues.Count == 0) clues.Add(0);
                colClues.Add(clues);
            }
        }

        // Draws the grid, clues, and all highlights
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen gridPen = new(Color.FromArgb(120, 120, 120));

            // Draw the background for the puzzle area
            Rectangle puzzleArea = new(clueMargin - 4, clueMargin - 4, gridSize * cellSize + 8, gridSize * cellSize + 8);
            g.FillRectangle(new SolidBrush(Color.White), puzzleArea);

            // Draw each cell in the grid
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int x = clueMargin + col * cellSize + 1;
                    int y = clueMargin + row * cellSize + 1;

                    // If solution overlay is on, show the solution, else show the user's state
                    int state = showSolutionOverlay ? solution[row][col] : cellStates[col, row];

                    // Draw an orange border if this cell is selected (for keyboard navigation)
                    if (row == selectedRow && col == selectedCol)
                    {
                        Rectangle selRect = new(x - 2, y - 2, cellSize + 3, cellSize + 3);
                        g.DrawRectangle(new Pen(Color.Orange, 3), selRect);
                    }

                    // Wrong cells are red, correct cells are green
                    if (wrongCells.Contains((col, row)))
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.IndianRed, rect);
                        using (Pen pen = new(Color.DarkRed, 2))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                        continue;
                    }
                    else if (correctCells.Contains((col, row)))
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.LightGreen, rect);
                        using (Pen pen = new(Color.Green, 2))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                        continue;
                    }

                    // Draw filled cells as black squares
                    if (state == 1)
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.Black, rect);
                        using (Pen whitePen = new(Color.White, 2))
                        {
                            g.DrawRectangle(whitePen, rect);
                        }
                    }
                    // Draw X for marked cells
                    else if (state == 2)
                    {
                        using (Pen xPen = new(Color.Crimson, 2))
                        {
                            g.DrawLine(xPen, x + 3, y + 3, x + cellSize - 5, y + cellSize - 5);
                            g.DrawLine(xPen, x + cellSize - 5, y + 3, x + 3, y + cellSize - 5);
                        }
                    }
                }
            }

            // Draw the grid lines
            for (int i = 0; i <= gridSize; i++)
            {
                g.DrawLine(gridPen,
                    clueMargin + i * cellSize, clueMargin,
                    clueMargin + i * cellSize, clueMargin + gridSize * cellSize);

                g.DrawLine(gridPen,
                    clueMargin, clueMargin + i * cellSize,
                    clueMargin + gridSize * cellSize, clueMargin + i * cellSize);
            }

            // Draw the clues for each row
            for (int row = 0; row < gridSize; row++)
            {
                if (row < rowClues.Count)
                {
                    string clue = string.Join(" ", rowClues[row]);
                    using (Font clueFont = new(this.Font.FontFamily, this.Font.Size + 2, FontStyle.Bold))
                    {
                        g.DrawString(clue, clueFont, Brushes.Navy,
                            10, clueMargin + row * cellSize + (cellSize / 6));
                    }
                }
            }

            // Draw the clues for each column
            DrawColumnClues(g);
        }

        // Draws the clues above each column
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
                        int x = clueMargin + col * cellSize + cellSize / 4;
                        int y = clueMargin - (clues.Count - i) * lineHeight - 5;
                        using (Font clueFont = new(this.Font.FontFamily, this.Font.Size + 2, FontStyle.Bold))
                        {
                            g.DrawString(clueStr, clueFont, Brushes.Navy, x, y);
                        }
                    }
                }
            }
        }

        // Handles mouse clicks to toggle cell state (empty -> filled -> X -> empty)
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            int mouseX = e.X;
            int mouseY = e.Y;

            // Only allow clicks inside the puzzle area
            if (mouseX < clueMargin || mouseX > this.ClientSize.Width - 180 || mouseY < clueMargin)
                return;

            int col = (mouseX - clueMargin) / cellSize;
            int row = (mouseY - clueMargin) / cellSize;

            if (col >= 0 && col < gridSize && row >= 0 && row < gridSize)
            {
                if (showSolutionOverlay)
                    return;

                // Don't let user change cells that were filled by a hint
                if (hintedCells.Contains((col, row)))
                {
                    cellStates[col, row] = 1;
                    Invalidate();
                    return;
                }

                int currentState = cellStates[col, row];
                int newState = (currentState + 1) % 3;
                cellStates[col, row] = newState;

                selectedRow = row;
                selectedCol = col;

                wrongCells.Remove((col, row));
                correctCells.Remove((col, row));

                // Score logic: only add score if this is a new correct cell
                if (scoreEnabled && newState == 1 && solution[row][col] == 1 && !hintedCells.Contains((col, row)) && !scoredCells.Contains((col, row)))
                {
                    scoredCells.Add((col, row));
                    score += 100;
                }
                // If user removes a correct cell, remove from scoredCells and update score
                else if (scoreEnabled && currentState == 1 && newState != 1 && solution[row][col] == 1 && scoredCells.Contains((col, row)))
                {
                    scoredCells.Remove((col, row));
                    score = Math.Max(0, score - 100);
                }

                UpdateScoreLabel();
                Invalidate();
            }
        }

        // Handles keyboard navigation and toggling cells (arrows, space, enter)
        private void PuzzleForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (showSolutionOverlay) return;

            if (selectedRow == -1 || selectedCol == -1)
            {
                selectedRow = 0;
                selectedCol = 0;
                Invalidate();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (selectedRow > 0) selectedRow--;
                    break;
                case Keys.Down:
                    if (selectedRow < gridSize - 1) selectedRow++;
                    break;
                case Keys.Left:
                    if (selectedCol > 0) selectedCol--;
                    break;
                case Keys.Right:
                    if (selectedCol < gridSize - 1) selectedCol++;
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Don't let user change cells that were filled by a hint
                    if (hintedCells.Contains((selectedCol, selectedRow)))
                    {
                        cellStates[selectedCol, selectedRow] = 1;
                        Invalidate();
                        return;
                    }
                    int currentState = cellStates[selectedCol, selectedRow];
                    int newState = (currentState + 1) % 3;
                    cellStates[selectedCol, selectedRow] = newState;
                    wrongCells.Remove((selectedCol, selectedRow));
                    correctCells.Remove((selectedCol, selectedRow));

                    // Score logic: only add score if this is a new correct cell
                    if (scoreEnabled && newState == 1 && solution[selectedRow][selectedCol] == 1 && !hintedCells.Contains((selectedCol, selectedRow)) && !scoredCells.Contains((selectedCol, selectedRow)))
                    {
                        scoredCells.Add((selectedCol, selectedRow));
                        score += 100;
                    }
                    // If user removes a correct cell, remove from scoredCells and update score
                    else if (scoreEnabled && currentState == 1 && newState != 1 && solution[selectedRow][selectedCol] == 1 && scoredCells.Contains((selectedCol, selectedRow)))
                    {
                        scoredCells.Remove((selectedCol, selectedRow));
                        score = Math.Max(0, score - 100);
                    }

                    UpdateScoreLabel();
                    break;
            }
            Invalidate();
        }

        // Checks if the puzzle is solved, and calculates the score
        private void CheckSolution()
        {
            wrongCells.Clear();
            correctCells.Clear();

            bool allCorrect = true;
            int userCorrectCells = 0;
            int userMistakes = 0;
            for (int col = 0; col < gridSize; col++)
            {
                for (int row = 0; row < gridSize; row++)
                {
                    if (cellStates[col, row] == 1 && solution[row][col] != 1)
                    {
                        wrongCells.Add((col, row));
                        allCorrect = false;
                        userMistakes++;
                    }
                    else if (cellStates[col, row] == 1 && solution[row][col] == 1)
                    {
                        correctCells.Add((col, row));
                        // Only count for points if not filled by hint
                        if (!hintedCells.Contains((col, row)))
                            userCorrectCells++;
                    }
                    else if (cellStates[col, row] != 1 && solution[row][col] == 1)
                    {
                        allCorrect = false;
                    }
                }
            }

            // Always update the score to match the calculated value
            UpdateScoreLabel();

            Invalidate();

            if (allCorrect)
            {
                // Add perfect bonus if no hints and no mistakes
                if (hintsUsed == 0 && userMistakes == 0)
                {
                    score += 10_000;
                    UpdateScoreLabel();
                }

                gameTimer.Stop();
                MessageBox.Show(
                    $"🎉 Puzzle solved! 🎉\n\n" +
                    $"Score: {score:N0}\n" +
                    $"Time: {elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}\n" +
                    $"Hints used: {hintsUsed}\n" +
                    (hintsUsed == 0 && userMistakes == 0 ? "Perfect Bonus: +10,000!" : ""),
                    "Success"
                );
            }
        }

        // Fills the next correct cell with a hint (if available)
        private void btnHint_Click(object sender, EventArgs e)
        {
            if (!hintsEnabled)
            {
                MessageBox.Show("Hints are disabled in settings.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    // Only fill cells that are not already filled and not filled by a hint
                    if (cellStates[col, row] != 1 && solution[row][col] == 1 && !hintedCells.Contains((col, row)))
                    {
                        cellStates[col, row] = 1;
                        hintedCells.Add((col, row));
                        hintsUsed++;
                        lblHintsUsed.Text = $"Hints used: {hintsUsed}";
                        lblHintsUsed.ForeColor = hintsUsed > 0 ? Color.FromArgb(220, 80, 80) : Color.FromArgb(120, 120, 120);
                        selectedRow = row;
                        selectedCol = col;
                        Invalidate();
                        UpdateScoreLabel();
                        return;
                    }
                }
            }
            MessageBox.Show("No more hints available!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Not used, but could be used for a tooltip on the timer
        private void lblTimer_Click(object sender, EventArgs e)
        {
            // Not used
        }

        // Hides the mistake highlights
        private void btnHideMistakes_Click(object sender, EventArgs e)
        {
            wrongCells.Clear();
            correctCells.Clear();
            Invalidate();
        }

        // Removes focus from controls when the form loads
        private void PuzzleForm_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        // Checks the solution when the check button is clicked
        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            if (showSolutionOverlay)
            {
                MessageBox.Show("Disable 'Show Solution' to check your answer.");
                return;
            }
            CheckSolution();
        }

        // Shows the solution overlay
        private void btnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate();
        }

        // Resets the puzzle and all game state
        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < gridSize; col++)
                for (int row = 0; row < gridSize; row++)
                    cellStates[col, row] = 0;

            wrongCells.Clear();
            correctCells.Clear();
            hintedCells.Clear();
            scoredCells.Clear();

            elapsedSeconds = 0;
            score = 0;
            hintsUsed = 0;
            lblHintsUsed.Text = "Hints used: 0";
            lblTimer.Text = "Time: 00:00";

            showSolutionOverlay = false;
            selectedRow = -1;
            selectedCol = -1;

            // Reset checkboxes and hint button
            scoreEnabled = false;
            hintsEnabled = false;
            chkEnableScore.Checked = false;
            chkEnableScore.Enabled = true;
            chkAllowHints.Checked = false;
            chkAllowHints.Enabled = true;
            btnHint.Enabled = false;

            Invalidate();
            gameTimer.Start();
            UpdateScoreLabel();
        }

        // Handles the Back to Menu button click
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            // Close this form and show the puzzle selection screen
            this.Hide();
            var chooseForm = new NonogramApp.ChoosePuzzleForm();
            chooseForm.FormClosed += (s, args) => this.Close();
            chooseForm.Show();
        }

        // Recalculates the score and updates the label, but does not show a message box
        private void UpdateScoreLabel()
        {
            if (scoreEnabled)
            {
                lblScore.Text = $"Score: {score:N0}";
            }
            else
            {
                lblScore.Text = "Score: 0";
            }
        }
    }
}