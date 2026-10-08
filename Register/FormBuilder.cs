using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

                // Зміна предмета -> групи, де цей викладач веде цей предмет
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

                // Зміна групи -> предмети, які цей викладач веде в обраній групі
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

            // 3. Загальний випадок (якщо роль без обмежень / адмін)
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

            // Перевіряємо, чи має викладач право на цей предмет у цій групі
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

                // Дозволяємо редагувати оцінку лише якщо цей викладач дійсно веде цей предмет
                if (gradeCol != null) gradeCol.ReadOnly = !isTeacherAssigned;
            }

            FormatDataGridView(dgv);
        }

        public static void SaveEditedGrade(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role, string teacherEmail, int rowIndex, string filePath)
        {
            if (role != "Викладач") return;

            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);
            if (!isValid || subj == null || grp == null) return;

            // Перевірка прав викладача
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
                // ВАЛІДАЦІЯ: оцінка має бути або пустою (null), або в межах [2; 5]
                if (editedRow.GradeValue.HasValue && (editedRow.GradeValue.Value < 2.0 || editedRow.GradeValue.Value > 5.0))
                {
                    MessageBox.Show("Оцінка повинна бути від 2 до 5!", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PopulateGrid(dgv, db, cbSubject, cbGroup, role, teacherEmail); // Повертаємо попереднє значення
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
                    // Якщо поле очистили — видаляємо оцінку
                    db.Grades.Remove(existingGrade);
                }

                db.SaveToFile(filePath);
                FormatDataGridView(dgv);
            }
        }
        public static void DataGridView_Cell(object? sender, DataGridViewCellValidatingEventArgs e, DataGridView dgv, string role)
        {
            // Валідуємо тільки стовпчик оцінки для викладача
            var gradeCol = dgv.Columns["GradeValue"] ?? dgv.Columns["Grade"] ?? dgv.Columns["Value"];
            if (gradeCol != null && e.ColumnIndex == gradeCol.Index && role == "Викладач")
            {
                string input = e.FormattedValue?.ToString()?.Trim() ?? "";

                // Якщо клітинку очистили (стерли оцінку) — це валідна дія
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }

                // Перевіряємо, чи це число і чи воно в діапазоні від 2 до 5
                if (!double.TryParse(input, out double grade) || grade < 2.0 || grade > 5.0)
                {
                    MessageBox.Show(
                        "Оцінка повинна бути числом від 2 до 5!",
                        "Некоректна оцінка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // e.Cancel = true повертає користувача назад у режим редагування цієї клітинки
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