using NonogramApp.Views;
using NonogramApp.Data;
using NonogramApp.Models;


namespace NonogramApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Session.CurrentUser != null)
            {
                label1.Text = $"Logged in as: {Session.CurrentUser.Username}";
            }
            else
            {
                label1.Text = "Not logged in.";
            }
        }

        public void UpdateAfterLoginOrLogout()
        {
            // Change label to display username
            if (Session.CurrentUser != null)
            {
                label1.Text = $"Logged in as: {Session.CurrentUser.Username}";
            }
            else
            {
                label1.Text = "Not logged in.";
            }

            // Change login/register buttons
            bool loggedIn = Session.CurrentUser != null;

            login.Visible = !loggedIn;
            register.Visible = !loggedIn;

            label1.Visible = loggedIn;
            logout.Visible = loggedIn;
            stats.Visible = loggedIn;

        }

        private void title1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide menu

            ChoosePuzzleForm choosePuzzleForm = new ChoosePuzzleForm();

            choosePuzzleForm.FormClosed += (s, args) => this.Show(); // Show menu again when ChoosePuzzleForm closes
            choosePuzzleForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Go to login form
            this.Hide(); // Hide MainForm
            Loginform loginForm = new Loginform(this);

            loginForm.FormClosed += (s, args) => this.Show(); // Show MainForm again when LoginForm closes
            loginForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Go to register form
            this.Hide(); // Hide MainForm
            Register registerFrom = new Register();

            registerFrom.FormClosed += (s, args) => this.Show(); // Show MainForm again when LoginForm closes
            registerFrom.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {
            Session.CurrentUser = null;
            UpdateAfterLoginOrLogout();
            label1.Text = "Not logged in.";
            MessageBox.Show("You have been logged out.");
        }

        private void stats_Click(object sender, EventArgs e)
        {

        }
    }
}
