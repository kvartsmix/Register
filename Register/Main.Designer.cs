namespace Register
{
    partial class Main
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
            panelHeader = new Panel();
            labelGroup = new Label();
            comboBoxGroup = new ComboBox();
            labelSubject = new Label();
            comboBoxSubject = new ComboBox();
            panelGridContainer = new Panel();
            dataGridView = new DataGridView();
            menuStrip1 = new MenuStrip();
            додатковоToolStripMenuItem = new ToolStripMenuItem();
            семестроваВідомістьToolStripMenuItem = new ToolStripMenuItem();
            зберегтиToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            panelHeader.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { додатковоToolStripMenuItem, зберегтиToolStripMenuItem, вихідToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(12, 6, 12, 6);
            menuStrip1.Size = new Size(984, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // додатковоToolStripMenuItem
            // 
            додатковоToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { семестроваВідомістьToolStripMenuItem });
            додатковоToolStripMenuItem.Name = "додатковоToolStripMenuItem";
            додатковоToolStripMenuItem.Size = new Size(97, 21);
            додатковоToolStripMenuItem.Text = "Додатково";
            // 
            // семестроваВідомістьToolStripMenuItem
            // 
            семестроваВідомістьToolStripMenuItem.Name = "семестроваВідомістьToolStripMenuItem";
            семестроваВідомістьToolStripMenuItem.Size = new Size(244, 26);
            семестроваВідомістьToolStripMenuItem.Text = "Семестрова відомість";
            семестроваВідомістьToolStripMenuItem.Click += семестроваВідомістьToolStripMenuItem_Click;
            // 
            // зберегтиToolStripMenuItem
            // 
            зберегтиToolStripMenuItem.Name = "зберегтиToolStripMenuItem";
            зберегтиToolStripMenuItem.Size = new Size(86, 21);
            зберегтиToolStripMenuItem.Text = "Зберегти";
            зберегтиToolStripMenuItem.Click += зберегтиToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(60, 21);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(labelGroup);
            panelHeader.Controls.Add(comboBoxGroup);
            panelHeader.Controls.Add(labelSubject);
            panelHeader.Controls.Add(comboBoxSubject);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 33);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(16, 12, 16, 12);
            panelHeader.Size = new Size(984, 60);
            panelHeader.TabIndex = 1;
            // 
            // labelGroup
            // 
            labelGroup.AutoSize = true;
            labelGroup.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            labelGroup.ForeColor = Color.FromArgb(71, 85, 105);
            labelGroup.Location = new Point(16, 20);
            labelGroup.Name = "labelGroup";
            labelGroup.Size = new Size(49, 17);
            labelGroup.TabIndex = 0;
            labelGroup.Text = "Група:";
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(72, 16);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(160, 25);
            comboBoxGroup.TabIndex = 1;
            comboBoxGroup.SelectedIndexChanged += comboBoxGroup_SelectedIndexChanged;
            // 
            // labelSubject
            // 
            labelSubject.AutoSize = true;
            labelSubject.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            labelSubject.ForeColor = Color.FromArgb(71, 85, 105);
            labelSubject.Location = new Point(256, 20);
            labelSubject.Name = "labelSubject";
            labelSubject.Size = new Size(67, 17);
            labelSubject.TabIndex = 2;
            labelSubject.Text = "Предмет:";
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(330, 16);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(320, 25);
            comboBoxSubject.TabIndex = 3;
            // 
            // panelGridContainer
            // 
            panelGridContainer.BackColor = Color.FromArgb(248, 250, 252);
            panelGridContainer.Controls.Add(dataGridView);
            panelGridContainer.Dock = DockStyle.Fill;
            panelGridContainer.Location = new Point(0, 93);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Padding = new Padding(16);
            panelGridContainer.Size = new Size(984, 527);
            panelGridContainer.TabIndex = 2;
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(16, 16);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(952, 495);
            dataGridView.TabIndex = 0;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            // 
            // Main
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
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Електронний журнал | Оцінки";
            FormClosed += Main_FormClosed;
            Load += Main_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label labelGroup;
        private Label labelSubject;
        private ComboBox comboBoxSubject;
        private ComboBox comboBoxGroup;
        private Panel panelGridContainer;
        private DataGridView dataGridView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem додатковоToolStripMenuItem;
        private ToolStripMenuItem семестроваВідомістьToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
        private ToolStripMenuItem зберегтиToolStripMenuItem;
    }
}