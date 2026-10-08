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
        private int groupID;
        private string role;
        public RegisterDB DataBase;

        public SemReportCard()
        {
            InitializeComponent();
            this.DataBase = Main.DataBase;
        }

        public SemReportCard(RegisterDB dataBase, string role, int groupID) : this()
        {
            if (groupID > 0)
                this.groupID = groupID;
            this.role = role;
            this.DataBase = dataBase;
        }

        private void SemReportCard_Load(object sender, EventArgs e)
        {
            FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);

            if (groupID > 0)
            {
                comboBoxGroup.SelectedValue = groupID;
            }
            else if (comboBoxGroup.SelectedValue != null && int.TryParse(comboBoxGroup.SelectedValue.ToString(), out int firstId)) this.groupID = firstId;

            if (role == "Студент")
            {
                comboBoxGroup.Enabled = false;
                dataGridViewSemReport.ReadOnly = true;
            }
            FormBuilder.SetReportDB(dataGridViewSemReport, DataBase, groupID > 0 ? groupID : null);
            FormBuilder.FormatDataGridView(dataGridViewSemReport);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            dataGridViewSemReport.EndEdit();
            DataBase.SaveToFile();
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGroup.SelectedValue != null && int.TryParse(comboBoxGroup.SelectedValue.ToString(), out int selectedId))
            {
                this.groupID = selectedId;
                FormBuilder.SetReportDB(dataGridViewSemReport, DataBase, groupID > 0 ? groupID : null);
                FormBuilder.FormatDataGridView(dataGridViewSemReport);
            }
        }
    }
}