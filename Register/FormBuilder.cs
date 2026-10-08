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
            // 1. Якщо увійшов СТУДЕНТ — показуємо тільки його групу і предмети цієї групи
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

            // 2. Якщо увійшов ВИКЛАДАЧ — показуємо ТІЛЬКИ ті предмети і групи, які він веде
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

            // 3. Загальний випадок
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
                    {
                        subjectVals.Add(v);
                    }
                }

                summaryRow[subject.Name] = subjectVals.Count > 0
                    ? subjectVals.Average().ToString("F2")
                    : string.Empty;
            }
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

            FormatDataGridView(dgv);
        }

        public static void SaveEditedGrade(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role, string teacherEmail, int rowIndex, string filePath)
        {
            if (role != "Викладач") return;

            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);
            if (!isValid || subj == null || grp == null) return;

            bool canEdit = db.GroupSubjects.Any(gs =>
                gs.GroupId == grp.Id &&
                gs.SubjectId == subj.Id &&
                string.Equals(gs.TeacherEmail, teacherEmail, StringComparison.OrdinalIgnoreCase));

            if (!canEdit)
            {
                MessageBox.Show("Ви не можете виставляти оцінки з предмета, який не викладаєте у цій групі.", "Доступ заборонено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgv.Rows[rowIndex].DataBoundItem is StudentGradeRow editedRow)
            {
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
                    db.Grades.Remove(existingGrade);
                }

                db.SaveToFile(filePath);
                FormatDataGridView(dgv);
            }
        }

        public static void DataGridView_Cell(object? sender, DataGridViewCellValidatingEventArgs e, DataGridView dgv, string role)
        {
            var gradeCol = dgv.Columns["GradeValue"] ?? dgv.Columns["Grade"] ?? dgv.Columns["Value"];
            if (gradeCol != null && e.ColumnIndex == gradeCol.Index && role == "Викладач")
            {
                string input = e.FormattedValue?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    return;
                }

                if (!double.TryParse(input, out double grade) || grade < 2.0 || grade > 5.0)
                {
                    MessageBox.Show(
                        "Оцінка повинна бути числом від 2 до 5!",
                        "Некоректна оцінка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    e.Cancel = true;
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

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 235, 240);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 241, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(13, 110, 253);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 246);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 95);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;

            dgv.RowTemplate.Height = 32;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
        }
    }
}