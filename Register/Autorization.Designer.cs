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
            labelPassword = new Label();
            SuspendLayout();
            // 
            // Login
            // 
            Login.Location = new Point(209, 76);
            Login.Margin = new Padding(2, 2, 2, 2);
            Login.Name = "Login";
            Login.Size = new Size(106, 23);
            Login.TabIndex = 0;
            // 
            // Pass
            // 
            Pass.Location = new Point(209, 118);
            Pass.Margin = new Padding(2, 2, 2, 2);
            Pass.Name = "Pass";
            Pass.Size = new Size(106, 23);
            Pass.TabIndex = 1;
            // 
            // Enter
            // 
            Enter.Location = new Point(224, 170);
            Enter.Margin = new Padding(2, 2, 2, 2);
            Enter.Name = "Enter";
            Enter.Size = new Size(78, 20);
            Enter.TabIndex = 2;
            Enter.Text = "Enter";
            Enter.UseVisualStyleBackColor = true;
            Enter.Click += Enter_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(209, 59);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 3;
            label1.Text = "Email";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(209, 101);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(57, 15);
            labelPassword.TabIndex = 4;
            labelPassword.Text = "Password";
            // 
            // Autorization
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(labelPassword);
            Controls.Add(label1);
            Controls.Add(Enter);
            Controls.Add(Pass);
            Controls.Add(Login);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Autorization";
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
        private Label labelPassword;
    }
}