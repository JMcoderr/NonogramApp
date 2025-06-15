using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NonogramApp.Views
{
    public partial class PuzzleForm : Form
    {
        private int gridSize;
        private int cellSize;
        private int clueMargin;
        private int[,] cellStates;
        private int[][] solution;
        private HashSet<(int x, int y)> wrongCells = new();
        private HashSet<(int x, int y)> correctCells = new();
        private HashSet<(int x, int y)> hintedCells = new(); // Track cells filled by hint
        private List<List<int>> rowClues = new();
        private List<List<int>> colClues = new();
        private bool showSolutionOverlay = false;
        private int score = 0;
        private System.Windows.Forms.Timer gameTimer;
        private int elapsedSeconds = 0;
        private bool scoreEnabled = false;
        private bool hintsEnabled = false;
        private int selectedRow = -1;
        private int selectedCol = -1;
        private int hintsUsed = 0;

        public PuzzleForm(int gridSize)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            InitializeSettingsPanelLogic();

            this.gridSize = gridSize;
            cellStates = new int[gridSize, gridSize];
            solution = new int[gridSize][];
            for (int i = 0; i < gridSize; i++)
                solution[i] = new int[gridSize];

            if (gridSize == 5)
                this.ClientSize = new Size(1000, 600);
            else if (gridSize == 10)
                this.ClientSize = new Size(1000, 700);
            else if (gridSize == 15)
                this.ClientSize = new Size(1000, 900);
            else
                this.ClientSize = new Size(1000, 700);

            this.MinimumSize = new Size(900, 600);
            this.ActiveControl = null;

            CalculateResponsiveSizes();
            RandomizeSolution();
            GenerateClues();

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.Resize += PuzzleForm_Resize;
            this.KeyPreview = true;
            this.KeyDown += PuzzleForm_KeyDown;
            SetToolTips();
            AddButtonHoverEffects();

            // Make checkboxes unchecked and enabled by default
            chkEnableScore.Checked = false;
            chkEnableScore.Enabled = true;
            chkAllowHints.Checked = false;
            chkAllowHints.Enabled = true;
            scoreEnabled = false;
            hintsEnabled = false;
            btnHint.Enabled = false;
        }

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

        private void AddButtonHoverEffects()
        {
            foreach (var btn in new[] { btnCheckSolution, btnShowSolution, btnHint, btnHideMistakes, btnReset, btnSettings })
            {
                btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(btn.BackColor, 0.15f);
                btn.MouseLeave += (s, e) => btn.BackColor = GetButtonBaseColor(btn);
            }
        }

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

        private void InitializeSettingsPanelLogic()
        {
            // Enable Score checkbox logic
            chkEnableScore.CheckedChanged += (s, e) =>
            {
                scoreEnabled = chkEnableScore.Checked;
                if (!scoreEnabled)
                {
                    lblScore.Text = "Score: 0";
                }
                else
                {
                    // Show current score if enabled
                    lblScore.Text = $"Score: {score}";
                }
            };

            // Allow Hints checkbox logic
            chkAllowHints.CheckedChanged += (s, e) =>
            {
                hintsEnabled = chkAllowHints.Checked;
                btnHint.Enabled = hintsEnabled;
            };
        }

        private void CalculateResponsiveSizes()
        {
            int availableWidth = this.ClientSize.Width - 180;
            int availableHeight = this.ClientSize.Height;
            clueMargin = Math.Clamp(gridSize * 5 + 60, 60, 140);
            int maxCellWidth = (availableWidth - clueMargin - 20) / gridSize;
            int maxCellHeight = (availableHeight - clueMargin - 40) / gridSize;
            cellSize = Math.Max(18, Math.Min(maxCellWidth, maxCellHeight));
        }

        private void PuzzleForm_Resize(object? sender, EventArgs e)
        {
            CalculateResponsiveSizes();
            Invalidate();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            elapsedSeconds++; // Timer always runs
            int minutes = elapsedSeconds / 60;
            int seconds = elapsedSeconds % 60;
            lblTimer.Text = $"Time: {minutes:D2}:{seconds:D2}";
        }

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

        private void GenerateClues()
        {
            rowClues = new();
            colClues = new();

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen gridPen = new(Color.FromArgb(120, 120, 120));

            // Draw puzzle area background (no outer border)
            Rectangle puzzleArea = new(clueMargin - 4, clueMargin - 4, gridSize * cellSize + 8, gridSize * cellSize + 8);
            g.FillRectangle(new SolidBrush(Color.White), puzzleArea);

            // Draw cells with updated responsive positions and sizes
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int x = clueMargin + col * cellSize + 1;
                    int y = clueMargin + row * cellSize + 1;

                    int state = showSolutionOverlay ? solution[row][col] : cellStates[col, row];

                    // Highlight selected cell for keyboard navigation
                    if (row == selectedRow && col == selectedCol)
                    {
                        Rectangle selRect = new(x - 2, y - 2, cellSize + 3, cellSize + 3);
                        g.DrawRectangle(new Pen(Color.Orange, 3), selRect);
                    }

                    // Draw wrong cells with a strong red highlight
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
                    // Draw correct cells with a green highlight
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

                    // Draw filled cell (black)
                    if (state == 1)
                    {
                        Rectangle rect = new(x, y, cellSize - 1, cellSize - 1);
                        g.FillRectangle(Brushes.Black, rect);
                        using (Pen whitePen = new(Color.White, 2))
                        {
                            g.DrawRectangle(whitePen, rect);
                        }
                    }
                    // Draw X mark for marked cell
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

            // Draw vertical and horizontal grid lines dynamically
            for (int i = 0; i <= gridSize; i++)
            {
                g.DrawLine(gridPen,
                    clueMargin + i * cellSize, clueMargin,
                    clueMargin + i * cellSize, clueMargin + gridSize * cellSize);

                g.DrawLine(gridPen,
                    clueMargin, clueMargin + i * cellSize,
                    clueMargin + gridSize * cellSize, clueMargin + i * cellSize);
            }

            // Draw row clues with responsive spacing
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

            DrawColumnClues(g);
        }

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

        // Mouse click toggles cell states in order: 0 -> 1 -> 2 -> 0
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

                int currentState = cellStates[col, row];
                int newState = (currentState + 1) % 3;
                cellStates[col, row] = newState;

                selectedRow = row;
                selectedCol = col;

                // If a cell is filled by hint, don't allow user to change it
                if (hintedCells.Contains((col, row)))
                {
                    cellStates[col, row] = 1;
                }
                else if (scoreEnabled)
                {
                    // Only update score for user actions, not hints
                    if (newState == 1 && solution[row][col] == 1)
                    {
                        score++;
                        lblScore.Text = $"Score: {score:N0}";
                    }
                    else if (currentState == 1 && newState == 2 && solution[row][col] == 1)
                    {
                        // If user un-fills a correct cell, decrement score
                        score = Math.Max(0, score - 1);
                        lblScore.Text = $"Score: {score:N0}";
                    }
                    else if (currentState == 1 && newState == 0 && solution[row][col] == 1)
                    {
                        // If user un-fills a correct cell, decrement score
                        score = Math.Max(0, score - 1);
                        lblScore.Text = $"Score: {score:N0}";
                    }
                }

                Invalidate();

                wrongCells.Remove((col, row));
                correctCells.Remove((col, row));
            }
        }

        // Keyboard navigation for accessibility
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
                    int currentState = cellStates[selectedCol, selectedRow];
                    int newState = (currentState + 1) % 3;
                    cellStates[selectedCol, selectedRow] = newState;
                    // If a cell is filled by hint, don't allow user to change it
                    if (hintedCells.Contains((selectedCol, selectedRow)))
                    {
                        cellStates[selectedCol, selectedRow] = 1;
                    }
                    else if (scoreEnabled)
                    {
                        if (newState == 1 && solution[selectedRow][selectedCol] == 1)
                        {
                            score++;
                            lblScore.Text = $"Score: {score:N0}";
                        }
                        else if (currentState == 1 && (newState == 2 || newState == 0) && solution[selectedRow][selectedCol] == 1)
                        {
                            score = Math.Max(0, score - 1);
                            lblScore.Text = $"Score: {score:N0}";
                        }
                    }
                    wrongCells.Remove((selectedCol, selectedRow));
                    correctCells.Remove((selectedCol, selectedRow));
                    break;
            }
            Invalidate();
        }

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            settingsPanel.Visible = !settingsPanel.Visible;
            if (settingsPanel.Visible)
                settingsPanel.BringToFront();
        }

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
                        if (!hintedCells.Contains((col, row)))
                            userCorrectCells++;
                    }
                    else if (cellStates[col, row] != 1 && solution[row][col] == 1)
                    {
                        allCorrect = false;
                    }
                }
            }

            Invalidate();

            if (allCorrect)
            {
                gameTimer.Stop();
                if (scoreEnabled)
                {
                    int basePoints = userCorrectCells * 100;
                    int speedBonus = Math.Max(0, 100_000 - (elapsedSeconds * gridSize * 100));
                    int hintPenalty = hintsUsed * 2500;
                    int perfectBonus = (hintsUsed == 0 && userMistakes == 0) ? 10_000 : 0;
                    score = Math.Max(0, basePoints + speedBonus + perfectBonus - hintPenalty);
                    lblScore.Text = $"Score: {score:N0}";
                }
                else
                {
                    lblScore.Text = "Score: 0";
                }
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
                    if (cellStates[col, row] != 1 && solution[row][col] == 1)
                    {
                        cellStates[col, row] = 1;
                        hintedCells.Add((col, row));
                        hintsUsed++;
                        lblHintsUsed.Text = $"Hints used: {hintsUsed}";
                        lblHintsUsed.ForeColor = hintsUsed > 0 ? Color.FromArgb(220, 80, 80) : Color.FromArgb(120, 120, 120);
                        selectedRow = row;
                        selectedCol = col;
                        Invalidate();
                        return;
                    }
                }
            }
            MessageBox.Show("No more hints available!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {
            // You could show a tooltip or do nothing
        }

        private void btnHideMistakes_Click(object sender, EventArgs e)
        {
            wrongCells.Clear();
            correctCells.Clear();
            Invalidate();
        }

        private void PuzzleForm_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void btnCheckSolution_Click(object sender, EventArgs e)
        {
            if (showSolutionOverlay)
            {
                MessageBox.Show("Disable 'Show Solution' to check your answer.");
                return;
            }
            CheckSolution();
        }

        private void btnShowSolution_Click(object sender, EventArgs e)
        {
            showSolutionOverlay = true;
            Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < gridSize; col++)
                for (int row = 0; row < gridSize; row++)
                    cellStates[col, row] = 0;

            wrongCells.Clear();
            correctCells.Clear();
            hintedCells.Clear();

            elapsedSeconds = 0;
            score = 0;
            hintsUsed = 0;
            lblScore.Text = $"Score: {score:N0}";
            lblHintsUsed.Text = "Hints used: 0";
            lblTimer.Text = "Time: 00:00";

            showSolutionOverlay = false;
            selectedRow = -1;
            selectedCol = -1;

            // Make checkboxes unchecked and enabled after reset
            scoreEnabled = false;
            hintsEnabled = false;
            chkEnableScore.Checked = false;
            chkEnableScore.Enabled = true;
            chkAllowHints.Checked = false;
            chkAllowHints.Enabled = true;
            btnHint.Enabled = false;

            Invalidate();
            gameTimer.Start();
        }
    }
}