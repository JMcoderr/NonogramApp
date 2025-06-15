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
            button4 = new Button();
            pictureBox1 = new PictureBox();
            Login = new Button();
            button6 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();

            // title1
            title1.AutoSize = true;
            title1.Font = new Font("Verdana", 37.8F, FontStyle.Bold);
            title1.ForeColor = Color.White;
            title1.Location = new Point(342, 100);
            title1.Name = "title1";
            title1.Size = new Size(556, 61);
            title1.TabIndex = 0;
            title1.Text = "Nonogram Puzzles";
            title1.Click += title1_Click;

            // button1 - Nieuwe Puzzle
            button1.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button1.Location = new Point(400, 250);
            button1.Name = "button1";
            button1.Size = new Size(400, 60);
            button1.TabIndex = 1;
            button1.Text = "Nieuwe Puzzle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            // button2 - Ga verder
            button2.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button2.Location = new Point(400, 330);
            button2.Name = "button2";
            button2.Size = new Size(400, 60);
            button2.TabIndex = 2;
            button2.Text = "Ga verder";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;

            // button3 - Instellingen
            button3.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button3.Location = new Point(400, 410);
            button3.Name = "button3";
            button3.Size = new Size(400, 60);
            button3.TabIndex = 3;
            button3.Text = "Instellingen";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;

            // button4 - Stop
            button4.BackColor = Color.FromArgb(106, 4, 29);
            button4.FlatAppearance.BorderColor = Color.FromArgb(106, 4, 29);
            button4.FlatAppearance.BorderSize = 2;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Verdana", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            button4.Location = new Point(520, 490);
            button4.Name = "button4";
            button4.Size = new Size(160, 60);
            button4.TabIndex = 4;
            button4.Text = "Stop";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;

            // button6 - optioneel
            button6.Location = new Point(1080, 180);
            button6.Name = "button6";
            button6.Size = new Size(80, 30);
            button6.TabIndex = 7;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(13, 92, 99);
            ClientSize = new Size(1184, 636);
            Controls.Add(button6);
            Controls.Add(Login);
            Controls.Add(pictureBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(title1);
            MaximumSize = new Size(1200, 675);
            MinimumSize = new Size(1200, 675);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private PictureBox pictureBox1;
        private Button Login;
        private Button button6;
    }
}
