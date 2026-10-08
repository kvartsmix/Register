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
<<<<<<< HEAD
            comboBoxSubject = new ComboBox();
            comboBoxGroup = new ComboBox();
            dataGridViewMain = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            SuspendLayout();
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(169, 25);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(121, 23);
            comboBoxSubject.TabIndex = 6;
            comboBoxSubject.Text = "Предмет";
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(24, 25);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(121, 23);
            comboBoxGroup.TabIndex = 5;
            comboBoxGroup.Text = "Група";
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.Location = new Point(24, 70);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.Size = new Size(566, 301);
            dataGridViewMain.TabIndex = 4;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxSubject);
            Controls.Add(comboBoxGroup);
            Controls.Add(dataGridViewMain);
            Name = "Main";
            Text = "Main";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
=======
            comboBoxGroup = new ComboBox();
            comboBoxSubject = new ComboBox();
            dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(14, 52);
            comboBoxGroup.Margin = new Padding(3, 4, 3, 4);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(138, 28);
            comboBoxGroup.TabIndex = 0;
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(209, 52);
            comboBoxSubject.Margin = new Padding(3, 4, 3, 4);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(138, 28);
            comboBoxSubject.TabIndex = 1;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(14, 116);
            dataGridView.Margin = new Padding(3, 4, 3, 4);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(683, 413);
            dataGridView.TabIndex = 2;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(dataGridView);
            Controls.Add(comboBoxSubject);
            Controls.Add(comboBoxGroup);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Main";
            Text = "Main";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
>>>>>>> e8ab6687d7a514020b2f4678e77b7c1873f2f08a
            ResumeLayout(false);
        }

        #endregion

<<<<<<< HEAD
        private ComboBox comboBoxSubject;
        private ComboBox comboBoxGroup;
        private DataGridView dataGridViewMain;
=======
        private ComboBox comboBoxGroup;
        private ComboBox comboBoxSubject;
        private DataGridView dataGridView;
>>>>>>> e8ab6687d7a514020b2f4678e77b7c1873f2f08a
    }
}