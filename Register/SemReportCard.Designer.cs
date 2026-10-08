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
            comboBoxGroup.Location = new Point(190, 37);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(121, 23);
            comboBoxGroup.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 40);
            label1.Name = "label1";
            label1.Size = new Size(162, 15);
            label1.TabIndex = 1;
            label1.Text = "Семестрова відомість групи";
            // 
            // dataGridViewSemReport
            // 
            dataGridViewSemReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSemReport.Location = new Point(12, 94);
            dataGridViewSemReport.Name = "dataGridViewSemReport";
            dataGridViewSemReport.Size = new Size(561, 287);
            dataGridViewSemReport.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(317, 40);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 3;
            label2.Text = "за V семестр";
            // 
            // SemReportCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(dataGridViewSemReport);
            Controls.Add(label1);
            Controls.Add(comboBoxGroup);
            Name = "SemReportCard";
            Text = "SemReportCard";
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