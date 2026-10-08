namespace Register
{
    partial class SemReportCard
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
            comboBoxGroup = new ComboBox();
            label1 = new Label();
            dataGridViewSemReport = new DataGridView();
            menuStrip1 = new MenuStrip();
            ExitToolStrip = new ToolStripMenuItem();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(95, 65);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(121, 23);
            comboBoxGroup.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 41);
            label1.Name = "label1";
            label1.Size = new Size(260, 21);
            label1.TabIndex = 1;
            label1.Text = "Семестрова відомість за V семестр";
            // 
            // dataGridViewSemReport
            // 
            dataGridViewSemReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewSemReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSemReport.Location = new Point(3, 94);
            dataGridViewSemReport.Name = "dataGridViewSemReport";
            dataGridViewSemReport.Size = new Size(670, 300);
            dataGridViewSemReport.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ExitToolStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(673, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // ExitToolStrip
            // 
            ExitToolStrip.Name = "ExitToolStrip";
            ExitToolStrip.Size = new Size(47, 20);
            ExitToolStrip.Text = "Вихід";
            ExitToolStrip.Click += ExitToolStrip_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(25, 65);
            label2.Name = "label2";
            label2.Size = new Size(53, 21);
            label2.TabIndex = 5;
            label2.Text = "Групи";
            label2.Click += label2_Click;
            // 
            // SemReportCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(673, 396);
            Controls.Add(label2);
            Controls.Add(dataGridViewSemReport);
            Controls.Add(label1);
            Controls.Add(comboBoxGroup);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "SemReportCard";
            Text = "SemReportCard";
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxGroup;
        private Label label1;
        private DataGridView dataGridViewSemReport;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ExitToolStrip;
        private Label label2;
    }
}