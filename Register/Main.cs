using System;
using System.Windows.Forms;

namespace Register
{
    public partial class Main : Form
    {
        public string filepath = "db.json";
        private RegisterDB? DataBase;

        public string email = string.Empty;
        public int groupID;
        public string role = string.Empty;

        public Main()
        {
            InitializeComponent();
        }

        public Main(string role, int groupID) : this()
        {
            this.role = role;
            this.groupID = groupID;
        }

        public Main(string role) : this()
        {
            this.role = role;
        }

        public Main(string role, string email) : this()
        {
            this.role = role;
            this.email = email;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DataBase = new RegisterDB(filepath);
            AdapterDB.SetupDataGridView(dataGridView);
            dataGridView.DataSource = DataBase.Grades;
            FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);
            FormBuilder.SetSubjectComboBox(comboBoxSubject, DataBase.Subjects);
            FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID);
            FormBuilder.FormatDataGridView(dataGridView);
            dataGridView.Width = this.ClientSize.Width;
            dataGridView.Height = this.ClientSize.Height - comboBoxGroup.Height - 50;
            FormStyles.StyleDataGridView(dataGridView);
            FormStyles.StyleComboBox(comboBoxGroup);
            FormStyles.StyleComboBox(comboBoxSubject);
        }

        private void RefreshGrid()
        {
            if (DataBase != null)
            {
                FormBuilder.PopulateGrid(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void семестроваВідомістьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SemReportCard form = new SemReportCard();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
             DataBase = new RegisterDB(filepath);

            AdapterDB.SetupDataGridView(dataGridView);

            FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID);

            comboBoxSubject.SelectionChangeCommitted += (s, ev) => RefreshGrid();
            comboBoxGroup.SelectionChangeCommitted += (s, ev) => RefreshGrid();

            dataGridView.CellEndEdit += (s, ev) =>
            {
                if (DataBase != null && ev.RowIndex >= 0)
                {
                    FormBuilder.SaveEditedGrade(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role, ev.RowIndex, filepath);
                }
            };

            RefreshGrid();

            if (role == "Студент")
            {
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
            }
            else if (role == "Викладач")
            {
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
            }
        }
    }
}