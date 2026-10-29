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

            FormBuilder.AddToComboBox(comboBoxTables, DataBase);
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
            }
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
    }
}
