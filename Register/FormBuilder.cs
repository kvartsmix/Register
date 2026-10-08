using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Register
{
    public static class FormBuilder
    {
        private static bool isSyncing = false;

        public static void SyncComboBoxes(ComboBox cbSubject, ComboBox cbGroup, RegisterDB db, string role = "", int studentGroupId = 0)
        {
            if (role == "Студент" && studentGroupId != 0)
            {
                SetGroupComboBox(cbGroup, new BindingList<Group>(db.Groups.Where(g => g.Id == studentGroupId).ToList()));
                cbGroup.Enabled = false;

                var studentSubjects = db.GroupSubjects
                    .Where(gs => gs.GroupId == studentGroupId && gs.Subject != null)
                    .Select(gs => gs.Subject!)
                    .DistinctBy(s => s.Id)
                    .ToList();

                SetSubjectComboBox(cbSubject, new BindingList<Subject>(studentSubjects));
                return;
            }

            SetSubjectComboBox(cbSubject, db.Subjects);
            SetGroupComboBox(cbGroup, db.Groups);

            cbSubject.SelectionChangeCommitted += (s, e) =>
            {
                if (isSyncing || cbSubject.SelectedItem is not Subject selectedSubject) return;

                isSyncing = true;
                int? previouslySelectedGroupId = (cbGroup.SelectedItem as Group)?.Id;

                var filteredGroups = db.GroupSubjects
                    .Where(gs => gs.SubjectId == selectedSubject.Id && gs.Group != null)
                    .Select(gs => gs.Group!)
                    .DistinctBy(g => g.Id)
                    .ToList();

                SetGroupComboBox(cbGroup, new BindingList<Group>(filteredGroups));

                if (previouslySelectedGroupId.HasValue && filteredGroups.Any(g => g.Id == previouslySelectedGroupId.Value))
                {
                    cbGroup.SelectedValue = previouslySelectedGroupId.Value;
                }

                isSyncing = false;
            };

            // 2. Зміна ГРУПИ -> фільтруємо доступні предмети
            cbGroup.SelectionChangeCommitted += (s, e) =>
            {
                if (isSyncing || cbGroup.SelectedItem is not Group selectedGroup) return;

                isSyncing = true;
                int? previouslySelectedSubjectId = (cbSubject.SelectedItem as Subject)?.Id;

                var filteredSubjects = db.GroupSubjects
                    .Where(gs => gs.GroupId == selectedGroup.Id && gs.Subject != null)
                    .Select(gs => gs.Subject!)
                    .DistinctBy(subj => subj.Id)
                    .ToList();

                SetSubjectComboBox(cbSubject, new BindingList<Subject>(filteredSubjects));

                // Зберігаємо вибір предмета, якщо він є у новому списку
                if (previouslySelectedSubjectId.HasValue && filteredSubjects.Any(subj => subj.Id == previouslySelectedSubjectId.Value))
                {
                    cbSubject.SelectedValue = previouslySelectedSubjectId.Value;
                }

                isSyncing = false;
            };
        }

        public static void SetGroupComboBox(ComboBox cb, BindingList<Group> groups)
        {
            cb.DataSource = null;
            cb.DisplayMember = "Id";
            cb.ValueMember = "Id";
            cb.DataSource = groups;
        }

        public static void SetSubjectComboBox(ComboBox cb, BindingList<Subject> subjects)
        {
            cb.DataSource = null;
            cb.DisplayMember = "Name";
            cb.ValueMember = "Id";
            cb.DataSource = subjects;
        }

        public static void SetAllTablesComboBox(ComboBox cb, RegisterDB db)
        {
            cb.Items.Clear();

            var properties = typeof(RegisterDB).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    cb.Items.Add(prop.Name);
                }
            }

            if (cb.Items.Count > 0)
                cb.SelectedIndex = 0;
        }

        public static void SetReportDB(DataGridView dataGrid, RegisterDB db, int? groupId = null)
        {
            var dt = new DataTable();

            // 1. Перший стовпець — студент
            dt.Columns.Add("Прізвище, ім'я студента", typeof(string));

            // 2. Стовпці всіх предметів
            var subjects = (groupId.HasValue
                ? db.GroupSubjects.Where(gs => gs.GroupId == groupId.Value).Select(gs => gs.Subject).Where(s => s != null)
                : db.Subjects)
                .DistinctBy(s => s!.Id)
                .OrderBy(s => s!.Name)
                .ToList();

            foreach (var subject in subjects)
            {
                if (subject != null && !dt.Columns.Contains(subject.Name))
                {
                    dt.Columns.Add(subject.Name, typeof(string));
                }
            }

            // 3. Останній стовпець — середній бал по студенту
            dt.Columns.Add("Середній бал", typeof(string));

            // 4. Студенти
            var students = (groupId.HasValue
                ? db.Students.Where(s => s.GroupId == groupId.Value)
                : db.Students)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();

            // 5. Заповнення рядків студентів
            foreach (var student in students)
            {
                var row = dt.NewRow();
                row["Прізвище, ім'я студента"] = $"{student.LastName} {student.FirstName}".Trim();

                var studentValues = new List<double>();

                foreach (var subject in subjects)
                {
                    if (subject == null) continue;

                    var grade = student.Grades.FirstOrDefault(g => g.SubjectId == subject.Id);
                    if (grade != null && grade.Value > 0)
                    {
                        row[subject.Name] = grade.Value.ToString(CultureInfo.CurrentCulture);
                        studentValues.Add(grade.Value);
                    }
                    else
                    {
                        row[subject.Name] = string.Empty;
                    }
                }

                row["Середній бал"] = studentValues.Count > 0
                    ? studentValues.Average().ToString("F2")
                    : string.Empty;

                dt.Rows.Add(row);
            }

            var bottomSummaryRow = dt.NewRow();
            bottomSummaryRow["Прізвище, ім'я студента"] = "Середній бал";
            dt.Rows.Add(bottomSummaryRow);

            RecalculateBottomAverages(dt, subjects);

            dataGrid.DataSource = null;
            dataGrid.AutoGenerateColumns = true;
            dataGrid.DataSource = dt;

            if (dataGrid.Columns["Прізвище, ім'я студента"] != null)
                dataGrid.Columns["Прізвище, ім'я студента"].ReadOnly = true;

            if (dataGrid.Columns["Середній бал"] != null)
                dataGrid.Columns["Середній бал"].ReadOnly = true;

            int lastRowIdx = dataGrid.Rows.Count - 1;
            if (lastRowIdx >= 0)
                dataGrid.Rows[lastRowIdx].ReadOnly = true;

            dataGrid.CellEndEdit -= DataGrid_CellEndEdit;
            dataGrid.CellEndEdit += DataGrid_CellEndEdit;

            void DataGrid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex < 0 || e.RowIndex >= students.Count || e.ColumnIndex <= 0 || e.ColumnIndex >= dt.Columns.Count - 1)
                    return;

                var student = students[e.RowIndex];
                string subjectName = dataGrid.Columns[e.ColumnIndex].HeaderText;
                var subject = subjects.FirstOrDefault(s => s?.Name == subjectName);
                if (subject == null) return;

                var cellRawValue = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                double.TryParse(cellRawValue, out double newValue);

                var grade = student.Grades.FirstOrDefault(g => g.SubjectId == subject.Id);
                if (grade != null)
                {
                    grade.Value = newValue;
                }
                else if (newValue > 0)
                {
                    int newId = db.Grades.Count > 0 ? db.Grades.Max(g => g.Id) + 1 : 1;
                    var newGrade = new Grade
                    {
                        Id = newId,
                        GroupId = student.GroupId,
                        SubjectId = subject.Id,
                        Value = newValue,
                        Student = student,
                        Subject = subject,
                        StudentEmail = student.Email
                    };

                    student.Grades.Add(newGrade);
                    db.Grades.Add(newGrade);
                }

                var studentValidGrades = new List<double>();
                for (int col = 1; col < dt.Columns.Count - 1; col++)
                {
                    if (double.TryParse(dt.Rows[e.RowIndex][col]?.ToString(), out double v) && v > 0)
                    {
                        studentValidGrades.Add(v);
                    }
                }

                dt.Rows[e.RowIndex]["Середній бал"] = studentValidGrades.Count > 0
                    ? studentValidGrades.Average().ToString("F2")
                    : string.Empty;
                RecalculateBottomAverages(dt, subjects);
            }
        }

        private static void RecalculateBottomAverages(DataTable dt, List<Subject?> subjects)
        {
            if (dt.Rows.Count <= 1) return;

            int bottomRowIndex = dt.Rows.Count - 1;
            var summaryRow = dt.Rows[bottomRowIndex];

            foreach (var subject in subjects)
            {
                if (subject == null || !dt.Columns.Contains(subject.Name)) continue;

                var subjectVals = new List<double>();
                for (int i = 0; i < bottomRowIndex; i++)
                {
                    if (double.TryParse(dt.Rows[i][subject.Name]?.ToString(), out double v) && v > 0)
                        subjectVals.Add(v);
                }

                summaryRow[subject.Name] = subjectVals.Count > 0
                    ? subjectVals.Average().ToString("F2")
                    : string.Empty;
            }
        }
        public static void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt)
        {
            if (dt.Rows.Count == 0) return;

            var subjectIds = grades.Select(g => g.SubjectId).Distinct();
            int lastRowIndex = dt.Rows.Count - 1;

            foreach (int subjectId in subjectIds)
            {
                string colName = subjectId.ToString();
                if (!dt.Columns.Contains(colName)) continue;

                var validGrades = grades.Where(g => g.SubjectId == subjectId && g.Value > 0).ToList();

                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    dt.Rows[lastRowIndex][colName] = average.ToString("F2");
                }
            }
        }

        public static void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt, string emailColumnName = "Email")
        {
            if (!dt.Columns.Contains(emailColumnName) || dt.Columns.Count == 0) return;

            int lastColIndex = dt.Columns.Count - 1;
            var studentEmails = grades
                .Where(g => !string.IsNullOrEmpty(g.StudentEmail))
                .Select(g => g.StudentEmail)
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (string email in studentEmails)
            {
                DataRow? targetRow = dt.AsEnumerable()
                    .FirstOrDefault(row => string.Equals(row.Field<string>(emailColumnName), email, StringComparison.OrdinalIgnoreCase));

                if (targetRow == null) continue;

                var validGrades = grades
                    .Where(g => string.Equals(g.StudentEmail, email, StringComparison.OrdinalIgnoreCase) && g.Value > 0)
                    .ToList();

                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    targetRow[lastColIndex] = average.ToString("F2");
                }
            }
        }

        public static void FormatDataGridView(DataGridView dgv)
        {
            for (int j = 0; j < dgv.Rows.Count; j++)
            {
                if (dgv.Rows[j].IsNewRow) continue;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var cell = dgv.Rows[j].Cells[i];
                    var val = cell.Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(val)) continue;

                    if (int.TryParse(val, out int num))
                    {
                        switch (num)
                        {
                            case 2:
                                cell.Style.ForeColor = Color.White;
                                cell.Style.BackColor = Color.Black;
                                break;
                            case 3:
                                cell.Style.ForeColor = Color.Blue;
                                break;
                            case 4:
                                cell.Style.ForeColor = Color.LimeGreen;
                                break;
                            case 5:
                                cell.Style.ForeColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}