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
            panelCard = new Panel();
            labelTitle = new Label();
            labelSubtitle = new Label();
            label1 = new Label();
            Login = new TextBox();
            label2 = new Label();
            Pass = new TextBox();
            Enter = new Button();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.BorderStyle = BorderStyle.FixedSingle;
            panelCard.Controls.Add(labelTitle);
            panelCard.Controls.Add(labelSubtitle);
            panelCard.Controls.Add(label1);
            panelCard.Controls.Add(Login);
            panelCard.Controls.Add(label2);
            panelCard.Controls.Add(Pass);
            panelCard.Controls.Add(Enter);
            panelCard.Location = new Point(32, 28);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(336, 384);
            panelCard.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(30, 41, 59);
            labelTitle.Location = new Point(20, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(294, 35);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Вхід до системи";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelSubtitle
            // 
            labelSubtitle.Font = new Font("Segoe UI", 9F);
            labelSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            labelSubtitle.Location = new Point(20, 60);
            labelSubtitle.Name = "labelSubtitle";
            labelSubtitle.Size = new Size(294, 22);
            labelSubtitle.TabIndex = 1;
            labelSubtitle.Text = "Електронний журнал успішності";
            labelSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(51, 65, 85);
            label1.Location = new Point(24, 102);
            label1.Name = "label1";
            label1.Size = new Size(40, 17);
            label1.TabIndex = 2;
            label1.Text = "Email";
            // 
            // Login
            // 
            Login.Font = new Font("Segoe UI", 10.5F);
            Login.Location = new Point(24, 126);
            Login.Name = "Login";
            Login.PlaceholderText = "name@example.com";
            Login.Size = new Size(286, 26);
            Login.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(51, 65, 85);
            label2.Location = new Point(24, 176);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 4;
            label2.Text = "Пароль";
            // 
            // Pass
            // 
            Pass.Font = new Font("Segoe UI", 10.5F);
            Pass.Location = new Point(24, 200);
            Pass.Name = "Pass";
            Pass.PasswordChar = '•';
            Pass.PlaceholderText = "••••••••";
            Pass.Size = new Size(286, 26);
            Pass.TabIndex = 5;
            // 
            // Enter
            // 
            Enter.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            Enter.Location = new Point(24, 260);
            Enter.Name = "Enter";
            Enter.Size = new Size(286, 42);
            Enter.TabIndex = 6;
            Enter.Text = "Увійти";
            Enter.UseVisualStyleBackColor = false;
            Enter.Click += Enter_Click;
            // 
            // Autorization
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(400, 442);
            Controls.Add(panelCard);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Autorization";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизація | Електронний журнал";
            Load += Autorization_Load;
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCard;
        private Label labelTitle;
        private Label labelSubtitle;
        private TextBox Login;
        private TextBox Pass;
        private Button Enter;
        private Label label1;
        private Label label2;
    }
}