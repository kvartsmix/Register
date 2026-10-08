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

        // Конструктор для викладача (передає роль та email)
        public Main(string role, string email) : this()
        {
            this.role = role;
            this.email = email;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Якщо дизайнер прив'язаний до Main_Load_1, цей метод можна залишити порожнім
        }

        private void RefreshGrid()
        {
            if (DataBase != null)
            {
                FormBuilder.PopulateGrid(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role, email);
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
            FormBuilder.StyleDataGridView(dataGridView);

            // ПЕРЕДАЄМО email викладача шостим параметром!
            FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID, email);

            comboBoxSubject.SelectionChangeCommitted += (s, ev) => RefreshGrid();
            comboBoxGroup.SelectionChangeCommitted += (s, ev) => RefreshGrid();

            dataGridView.CellEndEdit += (s, ev) =>
            {
                if (DataBase != null && ev.RowIndex >= 0)
                {
                    FormBuilder.SaveEditedGrade(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role, email, ev.RowIndex, filepath);
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

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            FormBuilder.DataGridView_Cell(sender, e, dataGridView, role);
        }
    }
}