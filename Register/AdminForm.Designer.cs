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
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            зберегтиToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            panelHeader = new Panel();
            labelTable = new Label();
            comboBoxTables = new ComboBox();
            panelGridContainer = new Panel();
            dataGridViewReport = new DataGridView();
            menuStrip1.SuspendLayout();
            panelHeader.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(12, 6, 12, 6);
            menuStrip1.Size = new Size(984, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { зберегтиToolStripMenuItem, вихідToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(53, 21);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // зберегтиToolStripMenuItem
            // 
            зберегтиToolStripMenuItem.Name = "зберегтиToolStripMenuItem";
            зберегтиToolStripMenuItem.Size = new Size(180, 26);
            зберегтиToolStripMenuItem.Text = "Зберегти";
            зберегтиToolStripMenuItem.Click += зберегтиToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(180, 26);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(labelTable);
            panelHeader.Controls.Add(comboBoxTables);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 33);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(16, 12, 16, 12);
            panelHeader.Size = new Size(984, 60);
            panelHeader.TabIndex = 1;
            // 
            // labelTable
            // 
            labelTable.AutoSize = true;
            labelTable.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            labelTable.ForeColor = Color.FromArgb(71, 85, 105);
            labelTable.Location = new Point(16, 20);
            labelTable.Name = "labelTable";
            labelTable.Size = new Size(111, 17);
            labelTable.TabIndex = 0;
            labelTable.Text = "Оберіть таблицю:";
            // 
            // comboBoxTables
            // 
            comboBoxTables.FormattingEnabled = true;
            comboBoxTables.Location = new Point(135, 16);
            comboBoxTables.Name = "comboBoxTables";
            comboBoxTables.Size = new Size(220, 25);
            comboBoxTables.TabIndex = 1;
            comboBoxTables.SelectedIndexChanged += comboBoxTables_SelectedIndexChanged;
            // 
            // panelGridContainer
            // 
            panelGridContainer.BackColor = Color.FromArgb(248, 250, 252);
            panelGridContainer.Controls.Add(dataGridViewReport);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 93);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(16);
            panelGridContainer.Size = new Size(984, 527);
            panelGridContainer.TabIndex = 2;
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.BackgroundColor = Color.White;
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Dock = DockStyle.Fill;
            dataGridViewReport.Location = new Point(16, 16);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.RowHeadersWidth = 51;
            dataGridViewReport.Size = new Size(952, 495);
            dataGridViewReport.TabIndex = 0;
            dataGridViewReport.CellValueChanged += dataGridViewReport_CellValueChanged;
            // 
            // AdminForm
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
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Панель адміністратора | Управління даними";
            FormClosed += AdminForm_FormClosed;
            Load += AdminForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem зберегтиToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
        private Panel panelHeader;
        private Label labelTable;
        private ComboBox comboBoxTables;
        private Panel panelGridContainer;
        private DataGridView dataGridViewReport;
    }
}