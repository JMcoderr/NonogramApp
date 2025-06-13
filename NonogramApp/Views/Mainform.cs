using NonogramApp.Views;

namespace NonogramApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            PuzzleForm puzzleForm = new PuzzleForm();
            puzzleForm.Show();  // Hiermee opent de PuzzleForm als een nieuw venster
        }
    }
}
