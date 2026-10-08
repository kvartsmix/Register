using System;
using System.Windows.Forms;

namespace Register
{
    public partial class Main : Form
    {
        public string filepath = "db.json";
        public static RegisterDB? DataBase;

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
            // 1. Ініціалізація бази
            DataBase = new RegisterDB(filepath);
            AdapterDB.SetupDataGridView(dataGridView);

            // 2. Стилізація
            FormStyles.StyleDataGridView(dataGridView);
            FormStyles.StyleComboBox(comboBoxGroup);
            FormStyles.StyleComboBox(comboBoxSubject);

            // 3. Синхронізація з передачею email викладача
            FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID, email);

            // 4. Підписка на події
            comboBoxSubject.SelectionChangeCommitted += (s, ev) => RefreshGrid();
            comboBoxGroup.SelectionChangeCommitted += (s, ev) => RefreshGrid();

            dataGridView.CellValidating += DataGridView_CellValidating;

            dataGridView.CellEndEdit += (s, ev) =>
            {
                if (DataBase != null && ev.RowIndex >= 0)
                {
                    FormBuilder.SaveEditedGrade(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role, email, ev.RowIndex, filepath);

                    // Оновлюємо відображення сітки одразу після редагування
                    dataGridView.InvalidateRow(dataGridView.Rows.Count - 1);
                }
            };

            // 5. Оновлення таблиці
            RefreshGrid();

            // 6. Права доступу
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

        private void RefreshGrid()
        {
            if (DataBase != null)
            {
                FormBuilder.PopulateGrid(dataGridView, DataBase, comboBoxSubject, comboBoxGroup, role, email);
            }
        }

        private void DataGridView_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            // Ігноруємо валідацію для останнього рядка (підсумку)
            if (e.RowIndex == dataGridView.Rows.Count - 1)
                return;

            var gradeCol = dataGridView.Columns["GradeValue"] ?? dataGridView.Columns["Grade"] ?? dataGridView.Columns["Value"];
            if (gradeCol != null && e.ColumnIndex == gradeCol.Index && role == "Викладач")
            {
                string input = e.FormattedValue?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                    return;

                if (!double.TryParse(input, out double grade) || grade < 2.0 || grade > 5.0)
                {
                    MessageBox.Show("Оцінка повинна бути числом від 2 до 5!", "Некоректна оцінка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void семестроваВідомістьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SemReportCard form = new SemReportCard(DataBase, role, comboBoxGroup.SelectedItem as Group);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}