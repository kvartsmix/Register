using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Register
{
    public class StudentGradeRow
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double? GradeValue { get; set; }
    }

    public static class FormBuilder
    {
        private static bool isSyncing = false;

        public static void SyncComboBoxes(ComboBox cbSubject, ComboBox cbGroup, RegisterDB db, string role = "", int studentGroupId = 0, string teacherEmail = "")
        {
            // 1. Студент: бачить виключно свою групу і предмети цієї групи
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

                if (cbSubject.Items.Count > 0)
                    cbSubject.SelectedIndex = 0;

                return;
            }

            // 2. Викладач: бачить ТІЛЬКИ ті предмети і групи, які він особисто веде
            if (role == "Викладач" && !string.IsNullOrEmpty(teacherEmail))
            {
                var teacherSubjects = db.GroupSubjects
                    .Where(gs => string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase) && gs.Subject != null)
                    .Select(gs => gs.Subject!)
                    .DistinctBy(s => s.Id)
                    .ToList();

                var teacherGroups = db.GroupSubjects
                    .Where(gs => string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase) && gs.Group != null)
                    .Select(gs => gs.Group!)
                    .DistinctBy(g => g.Id)
                    .ToList();

                SetSubjectComboBox(cbSubject, new BindingList<Subject>(teacherSubjects));
                SetGroupComboBox(cbGroup, new BindingList<Group>(teacherGroups));

                if (cbGroup.Items.Count > 0) cbGroup.SelectedIndex = 0;
                if (cbSubject.Items.Count > 0) cbSubject.SelectedIndex = 0;

                // Вибір предмета -> тільки групи цього викладача з цього предмета
                cbSubject.SelectionChangeCommitted += (s, e) =>
                {
                    if (isSyncing || cbSubject.SelectedItem is not Subject selectedSubject) return;

                    isSyncing = true;
                    int? prevGroupId = (cbGroup.SelectedItem as Group)?.Id;

                    var filteredGroups = db.GroupSubjects
                        .Where(gs => gs.SubjectId == selectedSubject.Id &&
                                     string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase) &&
                                     gs.Group != null)
                        .Select(gs => gs.Group!)
                        .DistinctBy(g => g.Id)
                        .ToList();

                    SetGroupComboBox(cbGroup, new BindingList<Group>(filteredGroups));

                    if (prevGroupId.HasValue && filteredGroups.Any(g => g.Id == prevGroupId.Value))
                        cbGroup.SelectedValue = prevGroupId.Value;
                    else if (cbGroup.Items.Count > 0)
                        cbGroup.SelectedIndex = 0;

                    isSyncing = false;
                };

                // Вибір групи -> тільки предмети цього викладача в обраній групі
                cbGroup.SelectionChangeCommitted += (s, e) =>
                {
                    if (isSyncing || cbGroup.SelectedItem is not Group selectedGroup) return;

                    isSyncing = true;
                    int? prevSubjectId = (cbSubject.SelectedItem as Subject)?.Id;

                    var filteredSubjects = db.GroupSubjects
                        .Where(gs => gs.GroupId == selectedGroup.Id &&
                                     string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase) &&
                                     gs.Subject != null)
                        .Select(gs => gs.Subject!)
                        .DistinctBy(subj => subj.Id)
                        .ToList();

                    SetSubjectComboBox(cbSubject, new BindingList<Subject>(filteredSubjects));

                    if (prevSubjectId.HasValue && filteredSubjects.Any(subj => subj.Id == prevSubjectId.Value))
                        cbSubject.SelectedValue = prevSubjectId.Value;
                    else if (cbSubject.Items.Count > 0)
                        cbSubject.SelectedIndex = 0;

                    isSyncing = false;
                };

                return;
            }

            // 3. Інші користувачі (повний перелік)
            SetSubjectComboBox(cbSubject, db.Subjects);
            SetGroupComboBox(cbGroup, db.Groups);

            if (cbGroup.Items.Count > 0) cbGroup.SelectedIndex = 0;
            if (cbSubject.Items.Count > 0) cbSubject.SelectedIndex = 0;

            cbSubject.SelectionChangeCommitted += (s, e) =>
            {
                if (isSyncing || cbSubject.SelectedItem is not Subject selectedSubject) return;
                isSyncing = true;
                int? prevGroupId = (cbGroup.SelectedItem as Group)?.Id;

                var filteredGroups = db.GroupSubjects
                    .Where(gs => gs.SubjectId == selectedSubject.Id && gs.Group != null)
                    .Select(gs => gs.Group!)
                    .DistinctBy(g => g.Id)
                    .ToList();

                SetGroupComboBox(cbGroup, new BindingList<Group>(filteredGroups));

                if (prevGroupId.HasValue && filteredGroups.Any(g => g.Id == prevGroupId.Value))
                    cbGroup.SelectedValue = prevGroupId.Value;
                else if (cbGroup.Items.Count > 0)
                    cbGroup.SelectedIndex = 0;

                isSyncing = false;
            };

            cbGroup.SelectionChangeCommitted += (s, e) =>
            {
                if (isSyncing || cbGroup.SelectedItem is not Group selectedGroup) return;
                isSyncing = true;
                int? prevSubjectId = (cbSubject.SelectedItem as Subject)?.Id;

                var filteredSubjects = db.GroupSubjects
                    .Where(gs => gs.GroupId == selectedGroup.Id && gs.Subject != null)
                    .Select(gs => gs.Subject!)
                    .DistinctBy(subj => subj.Id)
                    .ToList();

                SetSubjectComboBox(cbSubject, new BindingList<Subject>(filteredSubjects));

                if (prevSubjectId.HasValue && filteredSubjects.Any(subj => subj.Id == prevSubjectId.Value))
                    cbSubject.SelectedValue = prevSubjectId.Value;
                else if (cbSubject.Items.Count > 0)
                    cbSubject.SelectedIndex = 0;

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

        public static (bool IsValid, Subject? Subject, Group? Group) AreComboBoxesSelected(ComboBox cbSubject, ComboBox cbGroup, RegisterDB db)
        {
            var subject = cbSubject.SelectedItem as Subject;
            var group = cbGroup.SelectedItem as Group;

            if (subject == null || group == null)
            {
                return (false, null, null);
            }

            bool isValid = db.GroupSubjects.Any(gs =>
                gs.SubjectId == subject.Id &&
                gs.GroupId == group.Id);

            return (isValid, subject, group);
        }

        public static void PopulateGrid(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role, string teacherEmail = "")
        {
            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);

            if (!isValid || subj == null || grp == null)
            {
                dgv.DataSource = null;
                return;
            }

            bool isTeacherAssigned = true;
            if (role == "Викладач" && !string.IsNullOrEmpty(teacherEmail))
            {
                isTeacherAssigned = db.GroupSubjects.Any(gs =>
                    gs.GroupId == grp.Id &&
                    gs.SubjectId == subj.Id &&
                    string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase));
            }

            var groupStudents = db.Students
                .Where(s => s.GroupId == grp.Id)
                .OrderBy(s => s.LastName)
                .ToList();

            var rows = groupStudents.Select(student =>
            {
                string studentEmail = (student.Email ?? "").Trim();

                var grade = db.Grades.FirstOrDefault(g =>
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

            // РОЗРАХУНОК СЕРЕДНЬОГО БАЛУ ПО ОБРАНОМУ ПРЕДМЕТУ:
            var validGrades = rows
                .Where(r => r.GradeValue.HasValue && r.GradeValue.Value >= 2.0 && r.GradeValue.Value <= 5.0)
                .Select(r => r.GradeValue!.Value)
                .ToList();

            double? subjectAverage = validGrades.Count > 0 ? Math.Round(validGrades.Average(), 2) : null;

            // Додаємо підсумковий рядок у кінець списку
            rows.Add(new StudentGradeRow
            {
                LastName = "Середній бал",
                FirstName = "",
                Email = "",
                GradeValue = subjectAverage
            });

            dgv.AutoGenerateColumns = false;

            if (dgv.Columns["LastName"] != null) dgv.Columns["LastName"].DataPropertyName = "LastName";
            if (dgv.Columns["FirstName"] != null) dgv.Columns["FirstName"].DataPropertyName = "FirstName";
            if (dgv.Columns["Email"] != null) dgv.Columns["Email"].DataPropertyName = "Email";

            var gradeCol = dgv.Columns["GradeValue"] ?? dgv.Columns["Grade"] ?? dgv.Columns["Value"] ?? (dgv.Columns.Count > 2 ? dgv.Columns[2] : null);
            if (gradeCol != null)
            {
                gradeCol.DataPropertyName = "GradeValue";
                gradeCol.DefaultCellStyle.NullValue = "";
            }

            dgv.DataSource = new BindingList<StudentGradeRow>(rows);

            if (role == "Викладач")
            {
                if (dgv.Columns["LastName"] != null) dgv.Columns["LastName"].ReadOnly = true;
                if (dgv.Columns["FirstName"] != null) dgv.Columns["FirstName"].ReadOnly = true;
                if (dgv.Columns["Email"] != null) dgv.Columns["Email"].ReadOnly = true;
                if (gradeCol != null) gradeCol.ReadOnly = !isTeacherAssigned;
            }

            // Стилізуємо та блокуємо останній підсумковий рядок
            int lastRowIndex = dgv.Rows.Count - 1;
            if (lastRowIndex >= 0)
            {
                dgv.Rows[lastRowIndex].ReadOnly = true;
                dgv.Rows[lastRowIndex].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                dgv.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(240, 243, 246);
            }

            FormatDataGridView(dgv);
        }
        public static void SaveEditedGrade(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role, string teacherEmail, int rowIndex, string filePath)
        {
            if (role != "Викладач") return;

            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);
            if (!isValid || subj == null || grp == null) return;

            // Не чіпаємо останній підсумковий рядок
            if (rowIndex == dgv.Rows.Count - 1) return;

            bool canEdit = db.GroupSubjects.Any(gs =>
                gs.GroupId == grp.Id &&
                gs.SubjectId == subj.Id &&
                string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase));

            if (!canEdit)
            {
                MessageBox.Show("Ви не можете редагувати оцінки з предмета, який не ведете у цій групі.", "Доступ заборонено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgv.Rows[rowIndex].DataBoundItem is StudentGradeRow editedRow)
            {
                if (editedRow.LastName == "Середній бал" || string.IsNullOrEmpty(editedRow.Email))
                    return;

                // Валідація діапазону [2; 5]
                if (editedRow.GradeValue.HasValue && (editedRow.GradeValue.Value < 2.0 || editedRow.GradeValue.Value > 5.0))
                {
                    MessageBox.Show("Оцінка повинна бути від 2 до 5!", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PopulateGrid(dgv, db, cbSubject, cbGroup, role, teacherEmail);
                    return;
                }

                var existingGrade = db.Grades.FirstOrDefault(g =>
                    g.GroupId == grp.Id &&
                    g.SubjectId == subj.Id &&
                    string.Equals(g.StudentEmail, editedRow.Email, StringComparison.OrdinalIgnoreCase));

                if (editedRow.GradeValue.HasValue && editedRow.GradeValue.Value >= 2.0 && editedRow.GradeValue.Value <= 5.0)
                {
                    if (existingGrade != null)
                    {
                        existingGrade.Value = editedRow.GradeValue.Value;
                    }
                    else
                    {
                        int nextId = db.Grades.Any() ? db.Grades.Max(g => g.Id) + 1 : 1;
                        db.Grades.Add(new Grade
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
                    // Очищення клітинки видаляє оцінку
                    db.Grades.Remove(existingGrade);
                }

                // Зберігаємо в файл
                db.SaveToFile(filePath);

                // ДИНАМІЧНИЙ ПЕРЕРАХУНОК СЕРЕДНЬОГО БАЛУ БЕЗ ПЕРЕЗАВАНТАЖЕННЯ:
                if (dgv.DataSource is BindingList<StudentGradeRow> list && list.Count > 1)
                {
                    // Беремо всі рядки крім останнього (підсумкового)
                    var studentScores = list
                        .Take(list.Count - 1)
                        .Where(r => r.GradeValue.HasValue && r.GradeValue.Value >= 2.0 && r.GradeValue.Value <= 5.0)
                        .Select(r => r.GradeValue!.Value)
                        .ToList();

                    double? newAverage = studentScores.Count > 0 ? Math.Round(studentScores.Average(), 2) : null;

                    // Оновлюємо значення в останньому рядку
                    var summaryRow = list[list.Count - 1];
                    summaryRow.GradeValue = newAverage;

                    // Оновлюємо відображення цього рядка
                    list.ResetItem(list.Count - 1);
                }

                FormatDataGridView(dgv);
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

                    if (string.IsNullOrEmpty(val) || val == "0")
                    {
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.BackColor = Color.White;
                        continue;
                    }

                    if (double.TryParse(val, out double gradeValue))
                    {
                        int num = (int)Math.Round(gradeValue);
                        switch (num)
                        {
                            case 2:
                                cell.Style.ForeColor = Color.White;
                                cell.Style.BackColor = Color.Black;
                                break;
                            case 3:
                                cell.Style.ForeColor = Color.Blue;
                                cell.Style.BackColor = Color.White;
                                break;
                            case 4:
                                cell.Style.ForeColor = Color.LimeGreen;
                                cell.Style.BackColor = Color.White;
                                break;
                            case 5:
                                cell.Style.ForeColor = Color.Red;
                                cell.Style.BackColor = Color.White;
                                break;
                            default:
                                cell.Style.ForeColor = Color.Black;
                                cell.Style.BackColor = Color.White;
                                break;
                        }
                    }
                }
            }
        }

        public static void SetReportDB(DataGridView dataGrid, RegisterDB db, int? groupId = null)
        {
            var dt = new DataTable();

            dt.Columns.Add("Прізвище, ім'я студента", typeof(string));

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

            dt.Columns.Add("Середній бал", typeof(string));

            var students = (groupId.HasValue
                ? db.Students.Where(s => s.GroupId == groupId.Value)
                : db.Students)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();

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
                    {
                        subjectVals.Add(v);
                    }
                }

                summaryRow[subject.Name] = subjectVals.Count > 0
                    ? subjectVals.Average().ToString("F2")
                    : string.Empty;
            }
        }
    }
}