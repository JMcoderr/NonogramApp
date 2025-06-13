using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NonogramApp.Views
{
    public partial class PuzzleForm : Form
    {
        private int gridSize = 10;                        // Number of cells per row/column
        private int cellSize = 30;                        // Size of each cell in pixels
        private int clueMargin = 50;                        // Space reserved for clues (top and left)
        private int[,] cellStates;                          // 0 = empty, 1 = filled, 2 = cross

        private List<List<int>> rowClues = new List<List<int>>();  // Clues for each row
        private List<List<int>> colClues = new List<List<int>>();  // Clues for each column

        public PuzzleForm()
        {
            InitializeComponent();
            cellStates = new int[gridSize, gridSize];

            // Example row clues
            rowClues = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 3 },
                new List<int> { 2, 1 },
                new List<int> { 4 },
                new List<int> { 1, 1 },
                new List<int> { 3 },
                new List<int> { 2, 2 },
                new List<int> { 1 },
                new List<int> { 5 },
                new List<int> { 2 }
            };

            // Example column clues
            colClues = new List<List<int>>
            {
                new List<int> { 2 },
                new List<int> { 1, 1 },
                new List<int> { 3 },
                new List<int> { 1, 2 },
                new List<int> { 2 },
                new List<int> { 4 },
                new List<int> { 1, 1 },
                new List<int> { 3 },
                new List<int> { 2 },
                new List<int> { 1 }
            };
        }

        private void PuzzleForm_Load(object sender, EventArgs e)
        {
            // Runs when the PuzzleForm is loaded
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            // Draw filled and crossed cells
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    int state = cellStates[col, row];
                    int x = clueMargin + col * cellSize + 1;
                    int y = clueMargin + row * cellSize + 1;

                    if (state == 1)
                    {
                        // Filled black cell
                        g.FillRectangle(Brushes.Black, x, y, cellSize - 1, cellSize - 1);
                    }
                    else if (state == 2)
                    {
                        // Red cross
                        g.DrawLine(Pens.Red, x, y, x + cellSize - 2, y + cellSize - 2);
                        g.DrawLine(Pens.Red, x + cellSize - 2, y, x, y + cellSize - 2);
                    }
                }
            }

            // Draw grid lines
            for (int i = 0; i <= gridSize; i++)
            {
                // Vertical lines
                g.DrawLine(pen,
                    clueMargin + i * cellSize, clueMargin,
                    clueMargin + i * cellSize, clueMargin + gridSize * cellSize);

                // Horizontal lines
                g.DrawLine(pen,
                    clueMargin, clueMargin + i * cellSize,
                    clueMargin + gridSize * cellSize, clueMargin + i * cellSize);
            }

            // Draw row clues (left side)
            for (int row = 0; row < gridSize; row++)
            {
                string clueText = string.Join(" ", rowClues[row]);
                g.DrawString(clueText, this.Font, Brushes.Black,
                    5, clueMargin + row * cellSize + cellSize / 4);
            }

            // Draw column clues (top side)
            for (int col = 0; col < gridSize; col++)
            {
                string clueText = string.Join("\n", colClues[col]);
                g.DrawString(clueText, this.Font, Brushes.Black,
                    clueMargin + col * cellSize + 5, 5);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Only act if clicked inside the grid
            if (e.X < clueMargin || e.Y < clueMargin) return;

            int adjustedX = e.X - clueMargin;
            int adjustedY = e.Y - clueMargin;

            int x = adjustedX / cellSize;
            int y = adjustedY / cellSize;

            if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Left click: toggle filled <-> empty
                    cellStates[x, y] = (cellStates[x, y] == 1) ? 0 : 1;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // Right click: toggle cross <-> empty
                    cellStates[x, y] = (cellStates[x, y] == 2) ? 0 : 2;
                }

                Invalidate(); // Redraw grid
            }
        }
    }
}
