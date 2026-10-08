namespace Register
{
    partial class Autorization
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
            Login = new TextBox();
            Pass = new TextBox();
            Enter = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // Login
            // 
            Login.Location = new Point(69, 32);
            Login.Margin = new Padding(2, 2, 2, 2);
            Login.Name = "Login";
            Login.Size = new Size(106, 23);
            Login.TabIndex = 0;
            // 
            // Pass
            // 
            Pass.Location = new Point(69, 79);
            Pass.Margin = new Padding(2, 2, 2, 2);
            Pass.Name = "Pass";
            Pass.Size = new Size(106, 23);
            Pass.TabIndex = 1;
            // 
            // Enter
            // 
            Enter.BackColor = SystemColors.ButtonShadow;
            Enter.Location = new Point(49, 122);
            Enter.Margin = new Padding(2, 2, 2, 2);
            Enter.Name = "Enter";
            Enter.Size = new Size(78, 24);
            Enter.TabIndex = 2;
            Enter.Text = "Log in";
            Enter.UseVisualStyleBackColor = false;
            Enter.Click += Enter_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 35);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 3;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 82);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // Autorization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 250);
            ClientSize = new Size(199, 189);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Enter);
            Controls.Add(Pass);
            Controls.Add(Login);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2, 2, 2, 2);
            MinimizeBox = false;
            Name = "Autorization";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Autorization";
            Load += Autorization_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Login;
        private TextBox Pass;
        private Button Enter;
        private Label label1;
        private Label label2;
    }
}