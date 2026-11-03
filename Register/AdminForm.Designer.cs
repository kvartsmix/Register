namespace Register
{
    partial class AdminForm
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
            dataGridViewReport = new DataGridView();
            comboBoxTables = new ComboBox();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            зберегтиToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.BackgroundColor = SystemColors.Control;
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Location = new Point(-1, 100);
            dataGridViewReport.Margin = new Padding(3, 4, 3, 4);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.RowHeadersWidth = 51;
            dataGridViewReport.Size = new Size(917, 501);
            dataGridViewReport.TabIndex = 0;
            dataGridViewReport.CellValueChanged += dataGridViewReport_CellValueChanged;
            // 
            // comboBoxTables
            // 
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Location = new Point(14, 48);
            comboBoxTables.Margin = new Padding(3, 4, 3, 4);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(138, 28);
            comboBoxTables.TabIndex = 1;
            comboBoxTables.SelectedIndexChanged += comboBoxTables_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { зберегтиToolStripMenuItem, вихідToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // зберегтиToolStripMenuItem
            // 
            зберегтиToolStripMenuItem.Name = "зберегтиToolStripMenuItem";
            зберегтиToolStripMenuItem.Size = new Size(155, 26);
            зберегтиToolStripMenuItem.Text = "Зберегти";
            зберегтиToolStripMenuItem.Click += зберегтиToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(155, 26);
            вихідToolStripMenuItem.Text = "Вихід";
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(comboBoxTables);
            Controls.Add(dataGridViewReport);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "AdminForm";
            Text = "AdminForm";
            FormClosed += AdminForm_FormClosed;
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewReport;
        private ComboBox comboBoxTables;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem зберегтиToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
    }
}