using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Register
{
    public static class FormBuilder
    {
        private static bool isSyncing = false;

        public static void SyncComboBoxes(ComboBox cbSubject, ComboBox cbGroup, RegisterDB db, string role = "", int studentGroupId = 0)
        {
            // Якщо увійшов студент — жорстко обмежуємо його групою
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

            // Початкове заповнення повними списками
            SetSubjectComboBox(cbSubject, db.Subjects);
            SetGroupComboBox(cbGroup, db.Groups);

            // 1. Зміна ПРЕДМЕТА -> фільтруємо доступні групи
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

                // Зберігаємо вибір групи, якщо вона є у новому списку
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
                // Шукаємо рядок студента за Email у DataTable
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