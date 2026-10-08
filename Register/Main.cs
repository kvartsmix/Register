using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        // Конструктор для Студента
        public Main(string role, int groupID) : this()
        {
            this.role = role;
            this.groupID = groupID;
        }

        // Конструктор для Преподавателя (только роль)
        public Main(string role) : this()
        {
            this.role = role;
        }

        // Конструктор для Преподавателя с email
        public Main(string role, string email) : this()
        {
            this.role = role;
            this.email = email;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DataBase = new RegisterDB(filepath);

            AdapterDB.SetupDataGridView(dataGridView);

            // Настройка синхронизации выпадающих списков
            FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID);

            // Обновление таблицы при подтверждении выбора пользователем
            comboBoxSubject.SelectionChangeCommitted += (s, ev) => UpdateGrid();
            comboBoxGroup.SelectionChangeCommitted += (s, ev) => UpdateGrid();

            // Если преподаватель вводит или меняет оценку
            dataGridView.CellEndEdit += DataGridView_CellEndEdit;

            // Начальная загрузка данных
            UpdateGrid();

            // Разграничение прав доступа
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

        private void UpdateGrid()
        {
            if (DataBase == null) return;

            // Проверка: выбраны ли оба списка и существует ли такая связка
            var (isValid, subj, grp) = FormBuilder.AreComboBoxesSelected(comboBoxSubject, comboBoxGroup, DataBase);

            if (!isValid || subj == null || grp == null)
            {
                dataGridView.DataSource = null;
                return;
            }

            // 1. Получаем всех студентов выбранной группы
            var groupStudents = DataBase.Students
                .Where(s => s.GroupId == grp.Id)
                .OrderBy(s => s.LastName)
                .ToList();

            // 2. Формируем строки: если оценки нет, поле GradeValue остаётся null (пустая ячейка)
            var rows = groupStudents.Select(student =>
            {
                string studentEmail = (student.Email ?? "").Trim();

                var grade = DataBase.Grades.FirstOrDefault(g =>
                    g.GroupId == grp.Id &&
                    g.SubjectId == subj.Id &&
                    string.Equals((g.StudentEmail ?? "").Trim(), studentEmail, StringComparison.OrdinalIgnoreCase));

                return new StudentGradeRow
                {
                    LastName = student.LastName,
                    FirstName = student.FirstName,
                    Email = student.Email,
                    GradeValue = (grade != null && grade.Value > 0) ? grade.Value : null
                };
            }).ToList();

            // 3. Привязываем данные
            dataGridView.DataSource = new BindingList<StudentGradeRow>(rows);

            // Настройка колонок
            dataGridView.AutoGenerateColumns = false; // Вимикаємо автостворення, якщо колонки вже є з SetupDataGridView

            if (dataGridView.Columns["LastName"] != null)
                dataGridView.Columns["LastName"].DataPropertyName = "LastName";

            if (dataGridView.Columns["FirstName"] != null)
                dataGridView.Columns["FirstName"].DataPropertyName = "FirstName";

            if (dataGridView.Columns["Email"] != null)
                dataGridView.Columns["Email"].DataPropertyName = "Email";

            // ГОЛОВНЕ: для колонки оцінки (перевірте точну назву колонки з SetupDataGridView, зазвичай це "GradeValue", "Grade" або "Value")
            var gradeCol = dataGridView.Columns["GradeValue"] ?? dataGridView.Columns["Grade"] ?? dataGridView.Columns["Value"] ?? dataGridView.Columns[2];
            if (gradeCol != null)
            {
                gradeCol.DataPropertyName = "GradeValue";
                gradeCol.DefaultCellStyle.NullValue = "";
            }

            dataGridView.DataSource = new BindingList<StudentGradeRow>(rows);
            // Блокировка персональных данных для преподавателя
            if (role == "Викладач")
            {
                if (dataGridView.Columns["LastName"] != null) dataGridView.Columns["LastName"].ReadOnly = true;
                if (dataGridView.Columns["FirstName"] != null) dataGridView.Columns["FirstName"].ReadOnly = true;
                if (dataGridView.Columns["Email"] != null) dataGridView.Columns["Email"].ReadOnly = true;
                if (dataGridView.Columns["GradeValue"] != null) dataGridView.Columns["GradeValue"].ReadOnly = false;
            }

            // Цветовая подсветка оценок
            FormBuilder.FormatDataGridView(dataGridView);
        }

        private void DataGridView_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (DataBase == null || role != "Викладач") return;

            var (isValid, subj, grp) = FormBuilder.AreComboBoxesSelected(comboBoxSubject, comboBoxGroup, DataBase);
            if (!isValid || subj == null || grp == null) return;

            // Получаем отредактированную строку
            if (dataGridView.Rows[e.RowIndex].DataBoundItem is StudentGradeRow editedRow)
            {
                var existingGrade = DataBase.Grades.FirstOrDefault(g =>
                    g.GroupId == grp.Id &&
                    g.SubjectId == subj.Id &&
                    string.Equals(g.StudentEmail, editedRow.Email, StringComparison.OrdinalIgnoreCase));

                if (editedRow.GradeValue.HasValue && editedRow.GradeValue.Value > 0)
                {
                    if (existingGrade != null)
                    {
                        existingGrade.Value = editedRow.GradeValue.Value;
                    }
                    else
                    {
                        int nextId = DataBase.Grades.Any() ? DataBase.Grades.Max(g => g.Id) + 1 : 1;
                        DataBase.Grades.Add(new Grade
                        {
                            Id = nextId,
                            GroupId = grp.Id,
                            SubjectId = subj.Id,
                            StudentEmail = editedRow.Email,
                            Value = editedRow.GradeValue.Value
                        });
                    }
                }
                else if (existingGrade != null)
                {
                    // Если преподаватель стёр оценку — удаляем запись
                    DataBase.Grades.Remove(existingGrade);
                }

                // Сохраняем изменения в файл
                DataBase.SaveToFile(filepath);

                // Обновляем стили
                FormBuilder.FormatDataGridView(dataGridView);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }

    public class StudentGradeRow
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double? GradeValue { get; set; }
    }
}