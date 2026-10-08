using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class SemReportCard : Form
    {
        public SemReportCard()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SemReportCard_Load(object sender, EventArgs e)
        {
            FormStyles.StyleComboBox(comboBoxGroup);
            FormStyles.StyleDataGridView(dataGridViewSemReport);
        }
    }
}
