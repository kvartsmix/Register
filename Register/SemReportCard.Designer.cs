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
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).BeginInit();
            SuspendLayout();
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(217, 49);
            comboBoxGroup.Margin = new Padding(3, 4, 3, 4);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(138, 28);
            comboBoxGroup.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 53);
            label1.Name = "label1";
            label1.Size = new Size(205, 20);
            label1.TabIndex = 1;
            label1.Text = "Семестрова відомість групи";
            // 
            // dataGridViewSemReport
            // 
            dataGridViewSemReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSemReport.Location = new Point(14, 125);
            dataGridViewSemReport.Margin = new Padding(3, 4, 3, 4);
            dataGridViewSemReport.Name = "dataGridViewSemReport";
            dataGridViewSemReport.RowHeadersWidth = 51;
            dataGridViewSemReport.Size = new Size(641, 383);
            dataGridViewSemReport.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(362, 53);
            label2.Name = "label2";
            label2.Size = new Size(97, 20);
            label2.TabIndex = 3;
            label2.Text = "за V семестр";
            // 
            // SemReportCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label2);
            Controls.Add(dataGridViewSemReport);
            Controls.Add(label1);
            Controls.Add(comboBoxGroup);
            Margin = new Padding(3, 4, 3, 4);
            Name = "SemReportCard";
            Text = "SemReportCard";
            Load += SemReportCard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSemReport).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxGroup;
        private Label label1;
        private DataGridView dataGridViewSemReport;
        private Label label2;
    }
}