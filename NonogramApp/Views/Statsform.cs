using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using NonogramApp.Models;
using NonogramApp.Data;


namespace NonogramApp.Views
{
    public partial class Statsform : Form
    {
        public Statsform()
        {
            InitializeComponent();
            this.Load += Statsform_Load;
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void title1_Click(object sender, EventArgs e)
        {

        }

        private void myStats_Click(object sender, EventArgs e)
        {

        }

        private void rankings_Click(object sender, EventArgs e)
        {

        }

        // load datagrid data
        private void Statsform_Load(object sender, EventArgs e)
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\users.json");

            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("users.json not found.");
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonContent);

            var top10 = users
                .OrderByDescending(u => u.Score)
                .Take(10)
                .Select((user, index) => new
                {
                    Rank = index + 1,
                    Username = user.Username,
                    Score = user.Score
                })
                .ToList();

            dataGridViewLeaderboard.DataSource = top10;

            // Optional: formatting
            dataGridViewLeaderboard.Columns[0].HeaderText = "Rank";
            dataGridViewLeaderboard.Columns[1].HeaderText = "Username";
            dataGridViewLeaderboard.Columns[2].HeaderText = "Score";
            dataGridViewLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLeaderboard.ReadOnly = true;
            dataGridViewLeaderboard.AllowUserToAddRows = false;
            dataGridViewLeaderboard.AllowUserToDeleteRows = false;
            dataGridViewLeaderboard.RowHeadersVisible = false;
        }
    }
}
