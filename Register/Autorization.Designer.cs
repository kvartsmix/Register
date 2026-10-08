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
            SuspendLayout();
            // 
            // Login
            // 
            Login.Location = new Point(181, 102);
            Login.Name = "Login";
            Login.Size = new Size(150, 31);
            Login.TabIndex = 0;
            // 
            // Pass
            // 
            Pass.Location = new Point(437, 102);
            Pass.Name = "Pass";
            Pass.Size = new Size(150, 31);
            Pass.TabIndex = 1;
            // 
            // Enter
            // 
            Enter.Location = new Point(323, 262);
            Enter.Name = "Enter";
            Enter.Size = new Size(112, 34);
            Enter.TabIndex = 2;
            Enter.Text = "Enter";
            Enter.UseVisualStyleBackColor = true;
            Enter.Click += Enter_Click;
            // 
            // Autorization
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Enter);
            Controls.Add(Pass);
            Controls.Add(Login);
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
    }
}