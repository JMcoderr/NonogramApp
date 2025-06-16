namespace NonogramApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            title1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            quit = new Button();
            login = new Button();
            register = new Button();
            label1 = new Label();
            logout = new Button();
            stats = new Button();
            SuspendLayout();
            // 
            // title1
            // 
            title1.AutoSize = true;
            title1.Font = new Font("Verdana", 37.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title1.ForeColor = Color.White;
            title1.Location = new Point(342, 100);
            title1.Name = "title1";
            title1.Size = new Size(556, 61);
            title1.TabIndex = 0;
            title1.Text = "Nonogram Puzzles";
            title1.Click += title1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button1.Location = new Point(400, 250);
            button1.Name = "button1";
            button1.Size = new Size(400, 60);
            button1.TabIndex = 1;
            button1.Text = "Nieuwe Puzzle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button2.Location = new Point(400, 330);
            button2.Name = "button2";
            button2.Size = new Size(400, 60);
            button2.TabIndex = 2;
            button2.Text = "Ga verder";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button3.Location = new Point(400, 410);
            button3.Name = "button3";
            button3.Size = new Size(400, 60);
            button3.TabIndex = 3;
            button3.Text = "Instellingen";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // quit
            // 
            quit.BackColor = Color.FromArgb(106, 4, 29);
            quit.FlatAppearance.BorderColor = Color.FromArgb(106, 4, 29);
            quit.FlatAppearance.BorderSize = 2;
            quit.FlatStyle = FlatStyle.Flat;
            quit.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            quit.Location = new Point(520, 490);
            quit.Name = "quit";
            quit.Size = new Size(160, 60);
            quit.TabIndex = 4;
            quit.Text = "Stop";
            quit.UseVisualStyleBackColor = false;
            quit.Click += button4_Click;
            // 
            // login
            // 
            login.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            login.Location = new Point(1080, 12);
            login.Name = "login";
            login.Size = new Size(90, 30);
            login.TabIndex = 6;
            login.Text = "Login";
            login.UseVisualStyleBackColor = true;
            login.Click += button5_Click;
            // 
            // register
            // 
            register.Location = new Point(1080, 48);
            register.Name = "register";
            register.Size = new Size(90, 30);
            register.TabIndex = 7;
            register.Text = "Registreren";
            register.UseVisualStyleBackColor = true;
            register.Click += button6_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.Location = new Point(996, 84);
            label1.Name = "label1";
            label1.Size = new Size(58, 18);
            label1.TabIndex = 8;
            label1.Text = "label1";
            label1.Visible = false;
            label1.Click += label1_Click;
            // 
            // logout
            // 
            logout.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            logout.Location = new Point(1080, 48);
            logout.Name = "logout";
            logout.Size = new Size(90, 30);
            logout.TabIndex = 9;
            logout.Text = "Logout";
            logout.UseVisualStyleBackColor = true;
            logout.Visible = false;
            logout.Click += logout_Click;
            // 
            // stats
            // 
            stats.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            stats.Location = new Point(1080, 12);
            stats.Name = "stats";
            stats.Size = new Size(90, 30);
            stats.TabIndex = 10;
            stats.Text = "Statestieken";
            stats.UseVisualStyleBackColor = true;
            stats.Visible = false;
            stats.Click += stats_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(13, 92, 99);
            ClientSize = new Size(1184, 636);
            Controls.Add(stats);
            Controls.Add(logout);
            Controls.Add(label1);
            Controls.Add(register);
            Controls.Add(login);
            Controls.Add(quit);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(title1);
            MaximumSize = new Size(1200, 675);
            MinimumSize = new Size(1200, 675);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button quit;
        private Button login;
        private Button register;
        private Label label1;
        private Button logout;
        private Button stats;
    }
}
