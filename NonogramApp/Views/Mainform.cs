using NonogramApp.Views;

namespace NonogramApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void title1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // (Optional) Added puzzle logic for the page

            // Redirect to puzzle page

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
    }
}
