using NonogramApp.Views; // Zorg dat dit klopt met jouw mapstructuur

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
            // Title clicked - no action
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open ChoosePuzzleForm
            this.Hide(); // Hide MainForm
            ChoosePuzzleForm choosePuzzleForm = new ChoosePuzzleForm();
            choosePuzzleForm.FormClosed += (s, args) => this.Show(); // Show MainForm again when ChoosePuzzleForm closes
            choosePuzzleForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Continue puzzle (nog te implementeren)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Open settings (nog te implementeren)
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Exit app
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Profile picture click (nog te implementeren)
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Optional extra knop
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form load
        }
    }
}
