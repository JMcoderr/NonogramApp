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
using NonogramApp.Utility;

namespace NonogramApp.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // go back button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // submit button
        private void button2_Click(object sender, EventArgs e)
        {
            string username = this.username.Text.Trim();
            string password = this.password.Text;
            string repeatPassword = confirmPassword.Text;

            // validate input
            if (string.IsNullOrWhiteSpace(username) || username.Length < 6 ||
                string.IsNullOrWhiteSpace(password) || password.Length < 6 ||
                string.IsNullOrWhiteSpace(repeatPassword))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // check if username is already taken
            if (DataManager.GetUserByUsername(username) != null)
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            // password hash
            string passwordHash = Hash.PassHasher(password).ToString();

            // create new user
            User newUser = new User
            {
                Username = username,
                Password = passwordHash
            };

            // save user
            DataManager.AddUser(newUser);

            MessageBox.Show("Registration successful!");

            // close register
            this.Close();
            // {logic for going to login}

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
