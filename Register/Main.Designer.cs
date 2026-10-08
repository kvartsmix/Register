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
            comboBoxSubject = new ComboBox();
            comboBoxGroup = new ComboBox();
            dataGridView = new DataGridView();
            menuStrip1 = new MenuStrip();
            додатковоToolStripMenuItem = new ToolStripMenuItem();
            семестроваВідомістьToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(193, 33);
            comboBoxSubject.Margin = new Padding(3, 4, 3, 4);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(138, 28);
            comboBoxSubject.TabIndex = 6;
            comboBoxSubject.Text = "Предмет";
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(27, 33);
            comboBoxGroup.Margin = new Padding(3, 4, 3, 4);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(138, 28);
            comboBoxGroup.TabIndex = 5;
            comboBoxGroup.Text = "Група";
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(27, 93);
            dataGridView.Margin = new Padding(3, 4, 3, 4);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(647, 401);
            dataGridView.TabIndex = 4;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { додатковоToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(914, 30);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // додатковоToolStripMenuItem
            // 
            додатковоToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { семестроваВідомістьToolStripMenuItem, вихідToolStripMenuItem });
            додатковоToolStripMenuItem.Name = "додатковоToolStripMenuItem";
            додатковоToolStripMenuItem.Size = new Size(97, 24);
            додатковоToolStripMenuItem.Text = "Додатково";
            // 
            // семестроваВідомістьToolStripMenuItem
            // 
            семестроваВідомістьToolStripMenuItem.Name = "семестроваВідомістьToolStripMenuItem";
            семестроваВідомістьToolStripMenuItem.Size = new Size(244, 26);
            семестроваВідомістьToolStripMenuItem.Text = "Семестрова відомість";
            семестроваВідомістьToolStripMenuItem.Click += семестроваВідомістьToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(244, 26);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(comboBoxSubject);
            Controls.Add(comboBoxGroup);
            Controls.Add(dataGridView);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Main";
            Text = "Main";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxSubject;
        private ComboBox comboBoxGroup;
        private DataGridView dataGridView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem додатковоToolStripMenuItem;
        private ToolStripMenuItem семестроваВідомістьToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
    }
}