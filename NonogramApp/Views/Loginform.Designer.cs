namespace NonogramApp.Views
{
    partial class Loginform
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
            back = new Button();
            submit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 64F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.ForeColor = Color.White;
            label1.Location = new Point(127, 24);
            label1.Name = "label1";
            label1.Size = new Size(207, 78);
            label1.TabIndex = 0;
            label1.Text = "Login";
            label1.Click += label1_Click;
            // 
            // username
            // 
            username.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            username.Location = new Point(90, 161);
            username.Name = "username";
            username.Size = new Size(300, 27);
            username.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label2.ForeColor = Color.White;
            label2.Location = new Point(90, 140);
            label2.Name = "label2";
            label2.Size = new Size(147, 18);
            label2.TabIndex = 2;
            label2.Text = "Gebruikersnaam:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.ForeColor = Color.White;
            label3.Location = new Point(90, 232);
            label3.Name = "label3";
            label3.Size = new Size(115, 18);
            label3.TabIndex = 4;
            label3.Text = "Wachtwoord:";
            label3.Click += label3_Click;
            // 
            // password
            // 
            password.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            password.Location = new Point(90, 253);
            password.Name = "password";
            password.Size = new Size(300, 27);
            password.TabIndex = 3;
            password.TextChanged += textBox2_TextChanged;
            // 
            // back
            // 
            back.BackColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderColor = Color.FromArgb(106, 4, 29);
            back.FlatAppearance.BorderSize = 2;
            back.FlatStyle = FlatStyle.Flat;
            back.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            back.ForeColor = Color.White;
            back.Location = new Point(90, 357);
            back.Name = "back";
            back.Size = new Size(120, 50);
            back.TabIndex = 5;
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
            submit.Location = new Point(270, 357);
            submit.Name = "submit";
            submit.Size = new Size(120, 50);
            submit.TabIndex = 6;
            submit.Text = "Ok";
            submit.UseVisualStyleBackColor = false;
            submit.Click += button1_Click_1;
            // 
            // Loginform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 92, 99);
            ClientSize = new Size(464, 521);
            Controls.Add(submit);
            Controls.Add(back);
            Controls.Add(label3);
            Controls.Add(password);
            Controls.Add(label2);
            Controls.Add(username);
            Controls.Add(label1);
            MaximumSize = new Size(480, 560);
            MinimumSize = new Size(480, 560);
            Name = "Loginform";
            Text = "Loginform";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox username;
        private Label label2;
        private Label label3;
        private TextBox password;
        private Button back;
        private Button submit;
    }
}