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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        RegisterDB DataBase;
        bool hasChanged = false;
        private void AdminForm_Load(object sender, EventArgs e)
        {
            DataBase = new RegisterDB(Main.filepath);

            FormStyles.StyleComboBox(comboBoxTables);
            FormStyles.StyleDataGridView(dataGridViewReport);

            FormStyles.StyleMenuStripItem(зберегтиToolStripMenuItem, "\uE74E Зберегти");
            FormStyles.StyleMenuStripItem(вихідToolStripMenuItem, "\uE7E8 Вийти");


            dataGridViewReport.AllowUserToAddRows = true;
            FormBuilder.AddToComboBox(comboBoxTables, DataBase);
            comboBoxTables.SelectedIndex = 0;
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxTables.SelectedItem.ToString())
            {

                case "Teachers": dataGridViewReport.DataSource = DataBase.Teachers; break;
                case "Students": dataGridViewReport.DataSource = DataBase.Students; break;
                case "Groups": dataGridViewReport.DataSource = DataBase.Groups; break;
                case "Subjects": dataGridViewReport.DataSource = DataBase.Subjects; break;
                case "GroupSubjects": dataGridViewReport.DataSource = DataBase.GroupSubjects; break;
                case "Admins": dataGridViewReport.DataSource = DataBase.Admins; break;
            }
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = DataBase.Groups.Count - 1; i >= 0; i--)
            {
                if (DataBase.Groups[i].Id == 0)
                {
                    DataBase.Groups.RemoveAt(i);
                }
            }
            DataBase.SaveToFile(Main.filepath);
            hasChanged = false;
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormBuilder.ValidateChanges(hasChanged, DataBase);
            hasChanged = false;
        }

        private void dataGridViewReport_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            hasChanged = true;
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
