using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using NonogramApp.Models;
using NonogramApp.Data;
using static System.Collections.Specialized.BitVector32;


namespace NonogramApp.Views
{
    public partial class Loginform : Form
    {
        private Form1 _mainForm;
        public Loginform(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        // back
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // submit
        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = this.username.Text.Trim();
            string password = this.password.Text;

            // validate input
            if (string.IsNullOrWhiteSpace(username) || username.Length < 6 ||
                string.IsNullOrWhiteSpace(password) || password.Length < 6) 
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            // check if username exists 
            if (DataManager.GetUserByUsername(username) == null)
            {
                MessageBox.Show("Username doesn't exist.");
                return;
            }

            // check if password matches username
            User user = DataManager.GetUserByUsername(username);
            string enteredHash = ComputeSha256Hash(password);

            if (user.Password != enteredHash)
            {
                MessageBox.Show("Incorrect password.");
                return;
            }

            // SUCCESS!
            //Session.LoggedInUser = user;

            MessageBox.Show("Login successful!");

            // Proceed to next form or main app window
            _mainForm.Show();
            this.Hide();
        }

        // hash method
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
