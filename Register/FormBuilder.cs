using System;
using System.Collections.Generic;
using System.ComponentModel;
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

                if (cbSubject.Items.Count > 0)
                    cbSubject.SelectedIndex = 0;

                return;
            }

            SetSubjectComboBox(cbSubject, db.Subjects);
            SetGroupComboBox(cbGroup, db.Groups);

            if (cbGroup.Items.Count > 0)
                cbGroup.SelectedIndex = 0;
            if (cbSubject.Items.Count > 0)
                cbSubject.SelectedIndex = 0;

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
                else if (cbGroup.Items.Count > 0)
                {
                    cbGroup.SelectedIndex = 0;
                }

                isSyncing = false;
            };

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

                if (previouslySelectedSubjectId.HasValue && filteredSubjects.Any(subj => subj.Id == previouslySelectedSubjectId.Value))
                {
                    cbSubject.SelectedValue = previouslySelectedSubjectId.Value;
                }
                else if (cbSubject.Items.Count > 0)
                {
                    cbSubject.SelectedIndex = 0;
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

        public static void PopulateGrid(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role)
        {
            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);

            if (!isValid || subj == null || grp == null)
            {
                dgv.DataSource = null;
                return;
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

            if (dgv.Columns["LastName"] != null)
                dgv.Columns["LastName"].DataPropertyName = "LastName";

            if (dgv.Columns["FirstName"] != null)
                dgv.Columns["FirstName"].DataPropertyName = "FirstName";

            if (dgv.Columns["Email"] != null)
                dgv.Columns["Email"].DataPropertyName = "Email";

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
                if (gradeCol != null) gradeCol.ReadOnly = false;
            }

            FormatDataGridView(dgv);
        }

        public static void SaveEditedGrade(DataGridView dgv, RegisterDB db, ComboBox cbSubject, ComboBox cbGroup, string role, int rowIndex, string filePath)
        {
            if (role != "Викладач") return;

            var (isValid, subj, grp) = AreComboBoxesSelected(cbSubject, cbGroup, db);
            if (!isValid || subj == null || grp == null) return;

            if (dgv.Rows[rowIndex].DataBoundItem is StudentGradeRow editedRow)
            {
                var existingGrade = db.Grades.FirstOrDefault(g =>
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
    }
}