namespace NonogramApp.Views
{
    partial class Statsform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            back = new Button();
            title1 = new Label();
            myStats = new Label();
            rankings = new Label();
            dataGridViewLeaderboard = new DataGridView();
            usernameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            scoreDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            userBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewLeaderboard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userBindingSource).BeginInit();
            SuspendLayout();
            // 
            // back
            // 
            back.BackColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderSize = 2;
            back.FlatStyle = FlatStyle.Flat;
            back.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            back.ForeColor = Color.White;
            back.Location = new Point(520, 550);
            back.Name = "back";
            back.Size = new Size(160, 60);
            back.TabIndex = 4;
            back.Text = "Terug";
            back.UseVisualStyleBackColor = false;
            back.Click += back_Click;
            // 
            // title1
            // 
            title1.AutoSize = true;
            title1.Font = new Font("Verdana", 37.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title1.ForeColor = Color.White;
            title1.Location = new Point(405, 20);
            title1.Name = "title1";
            title1.Size = new Size(385, 61);
            title1.TabIndex = 0;
            title1.Text = "Statestieken";
            title1.Click += title1_Click;
            // 
            // myStats
            // 
            myStats.AutoSize = true;
            myStats.Font = new Font("Verdana", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            myStats.ForeColor = Color.White;
            myStats.Location = new Point(152, 110);
            myStats.Name = "myStats";
            myStats.Size = new Size(214, 29);
            myStats.TabIndex = 5;
            myStats.Text = "Mijn statestieken";
            myStats.Click += myStats_Click;
            // 
            // rankings
            // 
            rankings.AutoSize = true;
            rankings.Font = new Font("Verdana", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            rankings.ForeColor = Color.White;
            rankings.Location = new Point(877, 110);
            rankings.Name = "rankings";
            rankings.Size = new Size(116, 29);
            rankings.TabIndex = 6;
            rankings.Text = "Ranglijst";
            rankings.Click += rankings_Click;
            // 
            // dataGridViewLeaderboard
            // 
            dataGridViewLeaderboard.AllowUserToAddRows = false;
            dataGridViewLeaderboard.AllowUserToDeleteRows = false;
            dataGridViewLeaderboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLeaderboard.Location = new Point(726, 171);
            dataGridViewLeaderboard.Name = "dataGridViewLeaderboard";
            dataGridViewLeaderboard.ReadOnly = true;
            dataGridViewLeaderboard.Size = new Size(411, 325);
            dataGridViewLeaderboard.TabIndex = 7;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            usernameDataGridViewTextBoxColumn.DataPropertyName = "Username";
            usernameDataGridViewTextBoxColumn.HeaderText = "Username";
            usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scoreDataGridViewTextBoxColumn
            // 
            scoreDataGridViewTextBoxColumn.DataPropertyName = "Score";
            scoreDataGridViewTextBoxColumn.HeaderText = "Score";
            scoreDataGridViewTextBoxColumn.Name = "scoreDataGridViewTextBoxColumn";
            scoreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userBindingSource
            // 
            userBindingSource.DataSource = typeof(Models.User);
            // 
            // Statsform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 92, 99);
            ClientSize = new Size(1184, 636);
            Controls.Add(dataGridViewLeaderboard);
            Controls.Add(rankings);
            Controls.Add(myStats);
            Controls.Add(title1);
            Controls.Add(back);
            MaximumSize = new Size(1200, 675);
            MinimumSize = new Size(1200, 675);
            Name = "Statsform";
            Text = "Statsform";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLeaderboard).EndInit();
            ((System.ComponentModel.ISupportInitialize)userBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button back;
        private Label title1;
        private Label myStats;
        private Label rankings;
        private DataGridView dataGridViewLeaderboard;
        private DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn scoreDataGridViewTextBoxColumn;
        private BindingSource userBindingSource;
    }
}