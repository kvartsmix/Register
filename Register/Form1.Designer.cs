namespace Register
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewMain = new DataGridView();
            comboBoxGroup = new ComboBox();
            comboBoxSubject = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.Location = new Point(43, 119);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.Size = new Size(566, 301);
            dataGridViewMain.TabIndex = 0;
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(43, 74);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(121, 23);
            comboBoxGroup.TabIndex = 1;
            comboBoxGroup.Text = "Група";
            comboBoxGroup.SelectedIndexChanged += comboBoxGroup_SelectedIndexChanged;
            // 
            // comboBoxSubject
            // 
            comboBoxSubject.FormattingEnabled = true;
            comboBoxSubject.Location = new Point(188, 74);
            comboBoxSubject.Name = "comboBoxSubject";
            comboBoxSubject.Size = new Size(121, 23);
            comboBoxSubject.TabIndex = 2;
            comboBoxSubject.Text = "Предмет";
            // 
            // button1
            // 
            button1.Location = new Point(363, 74);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(comboBoxSubject);
            Controls.Add(comboBoxGroup);
            Controls.Add(dataGridViewMain);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewMain;
        private ComboBox comboBoxGroup;
        private ComboBox comboBoxSubject;
        private Button button1;
    }
}
