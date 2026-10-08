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
            comboBoxGroup.Location = new Point(109, 87);
            comboBoxGroup.Margin = new Padding(3, 4, 3, 4);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(138, 28);
            comboBoxGroup.TabIndex = 0;
            comboBoxGroup.SelectedIndexChanged += comboBoxGroup_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(14, 55);
            label1.Name = "label1";
            label1.Size = new Size(328, 28);
            label1.TabIndex = 1;
            label1.Text = "Семестрова відомість за V семестр";
            // 
            // dataGridViewSemReport
            // 
            dataGridViewSemReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewSemReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSemReport.Location = new Point(3, 125);
            dataGridViewSemReport.Margin = new Padding(3, 4, 3, 4);
            dataGridViewSemReport.Name = "dataGridViewSemReport";
            dataGridViewSemReport.RowHeadersWidth = 51;
            dataGridViewSemReport.Size = new Size(766, 400);
            dataGridViewSemReport.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { ExitToolStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(769, 30);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // ExitToolStrip
            // 
            ExitToolStrip.Name = "ExitToolStrip";
            ExitToolStrip.Size = new Size(60, 24);
            ExitToolStrip.Text = "Вихід";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(29, 87);
            label2.Name = "label2";
            label2.Size = new Size(67, 28);
            label2.TabIndex = 5;
            label2.Text = "Групи";
            // 
            // SemReportCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(769, 528);
            Controls.Add(label2);
            Controls.Add(dataGridViewSemReport);
            Controls.Add(label1);
            Controls.Add(comboBoxGroup);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "SemReportCard";
            Text = "SemReportCard";
            Load += SemReportCard_Load;
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