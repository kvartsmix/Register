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
            menuStrip1 = new MenuStrip();
            зберегтиToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStrip = new ToolStripMenuItem();
            panelHeader = new Panel();
            label1 = new Label();
            label2 = new Label();
            comboBoxGroup = new ComboBox();
            panelGridContainer = new Panel();
            dataGridViewSemReport = new DataGridView();
            menuStrip1.SuspendLayout();
            panelHeader.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { зберегтиToolStripMenuItem, ExitToolStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(12, 6, 12, 6);
            menuStrip1.Size = new Size(984, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // зберегтиToolStripMenuItem
            // 
            зберегтиToolStripMenuItem.Name = "зберегтиToolStripMenuItem";
            зберегтиToolStripMenuItem.Size = new Size(86, 21);
            зберегтиToolStripMenuItem.Text = "Зберегти";
            зберегтиToolStripMenuItem.Click += зберегтиToolStripMenuItem_Click;
            // 
            // ExitToolStrip
            // 
            ExitToolStrip.Name = "ExitToolStrip";
            ExitToolStrip.Size = new Size(60, 21);
            ExitToolStrip.Text = "Вихід";
            ExitToolStrip.Click += ExitToolStrip_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(label2);
            panelHeader.Controls.Add(comboBoxGroup);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 33);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(20, 14, 20, 14);
            panelHeader.Size = new Size(984, 76);
            panelHeader.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(30, 41, 59);
            label1.Location = new Point(16, 12);
            label1.Name = "label1";
            label1.Size = new Size(328, 25);
            label1.TabIndex = 0;
            label1.Text = "Семестрова відомість за V семестр";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(71, 85, 105);
            label2.Location = new Point(18, 44);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 1;
            label2.Text = "Група:";
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(74, 40);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(160, 25);
            comboBoxGroup.TabIndex = 2;
            // 
            // panelGridContainer
            // 
            panelGridContainer.BackColor = Color.FromArgb(248, 250, 252);
            panelGridContainer.Controls.Add(dataGridViewSemReport);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 109);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(16);
            panelGridContainer.Size = new Size(984, 511);
            panelGridContainer.TabIndex = 2;
            // 
            // dataGridViewSemReport
            // 
            dataGridViewSemReport.BackgroundColor = Color.White;
            dataGridViewSemReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSemReport.Dock = DockStyle.Fill;
            dataGridViewSemReport.Location = new Point(16, 16);
            dataGridViewSemReport.Name = "dataGridViewSemReport";
            dataGridViewSemReport.RowHeadersWidth = 51;
            dataGridViewSemReport.Size = new Size(952, 479);
            dataGridViewSemReport.TabIndex = 0;
            dataGridViewSemReport.CellValidating += dataGridViewSemReport_CellValidating;
            dataGridViewSemReport.CellValueChanged += dataGridViewSemReport_CellValueChanged;
            // 
            // SemReportCard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(984, 620);
            Controls.Add(panelGridContainer);
            Controls.Add(panelHeader);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9.5F);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(800, 500);
            Name = "SemReportCard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Семестрова відомість";
            FormClosing += SemReportCard_FormClosing;
            Load += SemReportCard_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ExitToolStrip;
        private ToolStripMenuItem зберегтиToolStripMenuItem;
        private Panel panelHeader;
        private Label label1;
        private Label label2;
        private ComboBox comboBoxGroup;
        private Panel panelGridContainer;
        private DataGridView dataGridViewSemReport;
    }
}