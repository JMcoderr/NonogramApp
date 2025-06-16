namespace NonogramApp.Views
{
    partial class Register
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
            label1 = new Label();
            username = new TextBox();
            label2 = new Label();
            label3 = new Label();
            password = new TextBox();
            label4 = new Label();
            confirmPassword = new TextBox();
            back = new Button();
            submit = new Button();
            login = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 64F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.ForeColor = Color.White;
            label1.Location = new Point(51, 28);
            label1.Name = "label1";
            label1.Size = new Size(401, 78);
            label1.TabIndex = 0;
            label1.Text = "Registreren";
            label1.Click += label1_Click;
            // 
            // username
            // 
            username.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            username.Location = new Point(85, 187);
            username.Name = "username";
            username.Size = new Size(300, 27);
            username.TabIndex = 1;
            username.TextChanged += textBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label2.ForeColor = Color.White;
            label2.Location = new Point(85, 166);
            label2.Name = "label2";
            label2.Size = new Size(147, 18);
            label2.TabIndex = 2;
            label2.Text = "Gebruikersnaam:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.ForeColor = Color.White;
            label3.Location = new Point(85, 246);
            label3.Name = "label3";
            label3.Size = new Size(115, 18);
            label3.TabIndex = 4;
            label3.Text = "Wachtwoord:";
            // 
            // password
            // 
            password.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            password.Location = new Point(85, 267);
            password.Name = "password";
            password.Size = new Size(300, 27);
            password.TabIndex = 3;
            password.TextChanged += password_TextChanged;
            password.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label4.ForeColor = Color.White;
            label4.Location = new Point(85, 326);
            label4.Name = "label4";
            label4.Size = new Size(181, 18);
            label4.TabIndex = 6;
            label4.Text = "Herhaal wachtwoord:";
            label4.Click += label4_Click;
            // 
            // confirmPassword
            // 
            confirmPassword.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            confirmPassword.Location = new Point(85, 347);
            confirmPassword.Name = "confirmPassword";
            confirmPassword.Size = new Size(300, 27);
            confirmPassword.TabIndex = 5;
            confirmPassword.UseSystemPasswordChar= true;
            // 
            // back
            // 
            back.BackColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderSize = 2;
            back.FlatStyle = FlatStyle.Flat;
            back.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            back.ForeColor = Color.White;
            back.Location = new Point(85, 455);
            back.Name = "back";
            back.Size = new Size(120, 50);
            back.TabIndex = 7;
            back.Text = "Terug";
            back.UseVisualStyleBackColor = false;
            back.Click += button1_Click;
            // 
            // submit
            // 
            submit.BackColor = Color.FromArgb(79, 178, 134);
            submit.FlatAppearance.BorderColor = Color.FromArgb(79, 178, 134);
            submit.FlatAppearance.BorderSize = 2;
            submit.FlatStyle = FlatStyle.Flat;
            submit.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            submit.ForeColor = Color.White;
            submit.Location = new Point(265, 455);
            submit.Name = "submit";
            submit.Size = new Size(120, 50);
            submit.TabIndex = 8;
            submit.Text = "Ok";
            submit.UseVisualStyleBackColor = false;
            submit.Click += button2_Click;
            // 
            // login
            // 
            login.AutoSize = true;
            login.LinkColor = Color.White;
            login.Location = new Point(160, 403);
            login.Name = "login";
            login.Size = new Size(153, 15);
            login.TabIndex = 9;
            login.TabStop = true;
            login.Text = "Al een account? Log in hier!";
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 92, 99);
            ClientSize = new Size(464, 601);
            Controls.Add(login);
            Controls.Add(submit);
            Controls.Add(back);
            Controls.Add(label4);
            Controls.Add(confirmPassword);
            Controls.Add(label3);
            Controls.Add(password);
            Controls.Add(label2);
            Controls.Add(username);
            Controls.Add(label1);
            MaximumSize = new Size(480, 640);
            MinimumSize = new Size(480, 640);
            Name = "Register";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox username;
        private Label label2;
        private Label label3;
        private TextBox password;
        private Label label4;
        private TextBox confirmPassword;
        private Button back;
        private Button submit;
        private LinkLabel login;
    }
}