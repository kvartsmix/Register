using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Data;

namespace Register
{
    public static class FormBuilder
    {
        public static void SyncComboBoxes(ComboBox cb1, ComboBox cb2)
        {

        }

        public static void CalculateAverageGradeForSubjects(BindingList<Grade> grades, DataTable dt)
        {
            var subjectIds = grades.Select(g => g.SubjectId).Distinct();

            foreach (int subjectId in subjectIds)
            {
                var validGrades = grades.Where(g => g.SubjectId == subjectId && g.Value > 0).ToList();

                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    dt.Rows[dt.Rows.Count - 1][subjectId.ToString()] = average.ToString("F2");
                }
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
        public static void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt)
        {
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
