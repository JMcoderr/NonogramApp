using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NonogramApp.Views
{
    // This is the main window for the Nonogram puzzle game.
    public partial class PuzzleForm : Form
    {
        // Puzzle data (logic fields only, no controls here)
        private readonly int gridSize; // How many rows/columns the puzzle has
        private int cellSize; // Size of each cell in pixels
        private int clueMargin; // Space for clues on the top/left
        private readonly Cell[,] cellStates; // Stores the state of each cell
        private int[][] solution; // The correct answer for the puzzle

        // Tracking cell status 
        private readonly HashSet<(int x, int y)> wrongCells = [];   // Cells the user filled wrong
        private readonly HashSet<(int x, int y)> correctCells = []; // Cells the user filled right
        private readonly HashSet<(int x, int y)> hintedCells = [];  // Cells filled by using a hint
        private readonly HashSet<(int x, int y)> scoredCells = [];  // Tracks which correct cells have been scored

        // Clues for the puzzle 
        private List<List<int>> rowClues = []; // Clues for each row
        private List<List<int>> colClues = []; // Clues for each column

        // Game state 
        private bool showSolutionOverlay = false; // If true, show the solution on top of the grid
        private int score = 0;                    // The player's score
        private readonly System.Windows.Forms.Timer gameTimer; // Timer for tracking how long the player takes
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
            cellStates = new Cell[gridSize, gridSize];
            solution = new int[gridSize][];
            for (int i = 0; i < gridSize; i++)
                solution[i] = new int[gridSize];

            // Initialize all cells as EmptyCell objects
            for (int col = 0; col < gridSize; col++)
                for (int row = 0; row < gridSize; row++)
                    cellStates[col, row] = new EmptyCell();

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

            // Calculate cell sizes and margins, then make a new puzzle in the background
            CalculateResponsiveSizes();
            GeneratePuzzleInBackground();

            // Set up the timer for the game
            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second
            };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // Handle resizing and keyboard input
            this.Resize += PuzzleForm_Resize;
            this.KeyPreview = true;
            this.KeyDown += PuzzleForm_KeyDown;
            SetToolTips();
            AddButtonHoverEffects();

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
            rowClues = [];
            colClues = [];

            // Row clues: count runs of filled cells
            for (int row = 0; row < gridSize; row++)
            {
                rowClues.Add(GetRuns(solution[row]));
            }

            // Column clues: same as above, but for columns
            for (int col = 0; col < gridSize; col++)
            {
                int[] column = new int[gridSize];
                for (int row = 0; row < gridSize; row++)
                {
                    column[row] = solution[row][col];
                }
                colClues.Add(GetRuns(column));
            }
        }

        // Generates the puzzle in the background using a Task (multithreading)
        private void GeneratePuzzleInBackground()
        {
            Task.Run(() =>
            {
                RandomizeSolution();
                GenerateClues();
                this.Invoke(new Action(() =>
                {
                    Invalidate();
                }));
            });
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

            // Loop through all cells and draw them
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int x = clueMargin + col * cellSize + 1;
                    int y = clueMargin + row * cellSize + 1;

                    int state = showSolutionOverlay ? solution[row][col] : cellStates[col, row].State;

                    if (row == selectedRow && col == selectedCol)
                    {
                        Rectangle selRect = new(x - 2, y - 2, cellSize + 3, cellSize + 3);
                        g.DrawRectangle(new Pen(Color.Orange, 3), selRect);
                    }

                    if (wrongCells.Contains((col, row)))
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.IndianRed, rect);
                        using Pen pen = new(Color.DarkRed, 2);
                        g.DrawRectangle(pen, rect);
                        continue;
                    }
                    else if (correctCells.Contains((col, row)))
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.LightGreen, rect);
                        using Pen pen = new(Color.Green, 2);
                        g.DrawRectangle(pen, rect);
                        continue;
                    }

                    if (state == 1)
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.Black, rect);
                        using Pen whitePen = new(Color.White, 2);
                        g.DrawRectangle(whitePen, rect);
                    }
                    else if (state == 2)
                    {
                        using Pen xPen = new(Color.Crimson, 2);
                        g.DrawLine(xPen, x + 3, y + 3, x + cellSize - 5, y + cellSize - 5);
                        g.DrawLine(xPen, x + cellSize - 5, y + 3, x + 3, y + cellSize - 5);
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
                    using Font clueFont = new(this.Font.FontFamily, this.Font.Size + 2, FontStyle.Bold);
                    g.DrawString(clue, clueFont, Brushes.Navy,
                        10, clueMargin + row * cellSize + (cellSize / 6));
                }
            }

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
                        using Font clueFont = new(this.Font.FontFamily, this.Font.Size + 2, FontStyle.Bold);
                        g.DrawString(clueStr, clueFont, Brushes.Navy, x, y);
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

            if (mouseX < clueMargin || mouseX > this.ClientSize.Width - 180 || mouseY < clueMargin)
                return;

            int col = (mouseX - clueMargin) / cellSize;
            int row = (mouseY - clueMargin) / cellSize;

            if (col >= 0 && col < gridSize && row >= 0 && row < gridSize)
            {
                if (showSolutionOverlay)
                    return;

                if (hintedCells.Contains((col, row)))
                {
                    cellStates[col, row] = new FilledCell();
                    Invalidate();
                    return;
                }

                int currentState = cellStates[col, row].State;
                int newState = (currentState + 1) % 3;
                cellStates[col, row] = newState switch
                {
                    0 => new EmptyCell(),
                    1 => new FilledCell(),
                    2 => new XCell(),
                    _ => cellStates[col, row]
                };

                selectedRow = row;
                selectedCol = col;

                wrongCells.Remove((col, row));
                correctCells.Remove((col, row));

                if (scoreEnabled && newState == 1 && solution[row][col] == 1 && !hintedCells.Contains((col, row)) && !scoredCells.Contains((col, row)))
                {
                    scoredCells.Add((col, row));
                    score += 100;
                }
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
                    if (hintedCells.Contains((selectedCol, selectedRow)))
                    {
                        cellStates[selectedCol, selectedRow] = new FilledCell();
                        Invalidate();
                        return;
                    }
                    int currentState = cellStates[selectedCol, selectedRow].State;
                    int newState = (currentState + 1) % 3;
                    cellStates[selectedCol, selectedRow] = newState switch
                    {
                        0 => new EmptyCell(),
                        1 => new FilledCell(),
                        2 => new XCell(),
                        _ => cellStates[selectedCol, selectedRow]
                    };
                    wrongCells.Remove((selectedCol, selectedRow));
                    correctCells.Remove((selectedCol, selectedRow));

                    if (scoreEnabled && newState == 1 && solution[selectedRow][selectedCol] == 1 && !hintedCells.Contains((selectedCol, selectedRow)) && !scoredCells.Contains((selectedCol, selectedRow)))
                    {
                        scoredCells.Add((selectedCol, selectedRow));
                        score += 100;
                    }
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

            bool allCorrect = IsUserSolutionValid();

            UpdateScoreLabel();
            Invalidate();

            if (allCorrect)
            {
                if (hintsUsed == 0)
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
                    (hintsUsed == 0 ? "Perfect Bonus: +10,000!" : ""),
                    "Success"
                );

                // Go back to menu after solving
                this.Hide();
                var chooseForm = new NonogramApp.ChoosePuzzleForm();
                chooseForm.FormClosed += (s, args) => this.Close();
                chooseForm.Show();
            }
        }

        // Fills the next correct cell with a hint (if available)
        private void BtnHint_Click(object sender, EventArgs e)
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
                    if (cellStates[col, row].State != 1 && solution[row][col] == 1 && !hintedCells.Contains((col, row)))
                    {
                        cellStates[col, row] = new FilledCell();
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
        private void LblTimer_Click(object sender, EventArgs e)
        {
            // Not used
        }

        // Hides the mistake highlights
        private void BtnHideMistakes_Click(object sender, EventArgs e)
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
        private void BtnCheckSolution_Click(object sender, EventArgs e)
        {
            if (showSolutionOverlay)
            {
                MessageBox.Show("Disable 'Show Solution' to check your answer.", "Check Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check if the puzzle is solved
            bool wasSolved = IsUserSolutionValid();
            CheckSolution();

            // If not solved, show a friendly message
            if (!wasSolved)
            {
                MessageBox.Show(
                    "🧩 The puzzle is not solved yet!\n\n" +
                    "Keep going, you're getting closer.\n" +
                    "Tip: Double-check the clues on the side and top.",
                    "Not Solved Yet",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        // Shows the solution overlay
        private void BtnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate();
        }

        // Resets the puzzle and all game state
        private void BtnReset_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < gridSize; col++)
                for (int row = 0; row < gridSize; row++)
                    cellStates[col, row] = new EmptyCell();

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
        private void BtnBackToMenu_Click(object sender, EventArgs e)
        {
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

        // Extracts runs of filled cells from a line (row or column)
        private static List<int> GetRuns(int[] line)
        {
            List<int> runs = [];
            int count = 0;
            foreach (var cell in line)
            {
                if (cell == 1)
                    count++;
                else if (count > 0)
                {
                    runs.Add(count);
                    count = 0;
                }
            }
            if (count > 0)
                runs.Add(count);
            if (runs.Count == 0)
                runs.Add(0);
            return runs;
        }

        private bool IsUserSolutionValid()
        {
            // Check rows
            for (int row = 0; row < gridSize; row++)
            {
                int[] userRow = new int[gridSize];
                for (int col = 0; col < gridSize; col++)
                    userRow[col] = cellStates[col, row].State;
                var userRuns = GetRuns(userRow);
                if (!userRuns.SequenceEqual(rowClues[row]))
                    return false;
            }
            // Check columns
            for (int col = 0; col < gridSize; col++)
            {
                int[] userCol = new int[gridSize];
                for (int row = 0; row < gridSize; row++)
                    userCol[row] = cellStates[col, row].State;
                var userRuns = GetRuns(userCol);
                if (!userRuns.SequenceEqual(colClues[col]))
                    return false;
            }
            return true;
        }

        private Cell[,] GetCellObjects()
        {
            var cells = new Cell[gridSize, gridSize];
            for (int col = 0; col < gridSize; col++)
            {
                for (int row = 0; row < gridSize; row++)
                {
                    cells[col, row] = cellStates[col, row].State switch
                    {
                        1 => new FilledCell(),
                        2 => new XCell(),
                        _ => new EmptyCell(),
                    };
                }
            }
            return cells;
        }
    }

    // Polymorphic cell types
    public abstract class Cell
    {
        public abstract int State { get; set; } // 0 = empty, 1 = filled, 2 = X
    }

    public class EmptyCell : Cell
    {
        public override int State { get; set; } = 0;
    }

    public class FilledCell : Cell
    {
        public override int State { get; set; } = 1;
    }

    public class XCell : Cell
    {
        public override int State { get; set; } = 2;
    }
}