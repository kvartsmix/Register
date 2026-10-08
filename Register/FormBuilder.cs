using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Register
{
    public static class FormBuilder
    {
        public static void SetGroupComboBox(ComboBox cb, BindingList<Group> groups)
        {
            cb.DataSource = null;
            cb.DisplayMember = "Id";
            cb.ValueMember = "Id";
            cb.DataSource = groups;
        }

        public static void CalculateAverageGradeForSubjects(BindingList<Grade> grades, DataTable dt)
        {
            if (dt.Rows.Count == 0) return;

            var subjectIds = grades.Select(g => g.SubjectId).Distinct();
            int lastRowIndex = dt.Rows.Count - 1;

            foreach (int subjectId in subjectIds)
            {
                string colName = subjectId.ToString();
                if (!dt.Columns.Contains(colName)) continue;

                var validGrades = grades
                    .Where(g => g.SubjectId == subjectId && g.Value > 0)
                    .ToList();

                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    dt.Rows[lastRowIndex][colName] = average.ToString("F2");
                }
            }
        }

        public static void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt, string emailColumnName = "Email")
        {
            if (!dt.Columns.Contains(emailColumnName)) return;

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
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                for (int j = 0; j < dgv.Rows.Count; j++)
                {
                    if (dgv.Rows[j].Cells[i].Value == null || string.IsNullOrWhiteSpace(dgv.Rows[j].Cells[i].Value.ToString()))
                    {
                        continue;
                    }

                    if (int.TryParse(dgv.Rows[j].Cells[i].Value.ToString(), out int num))
                    {
                        if (num == 2)
                        {
                            dgv.Rows[j].Cells[i].Style.ForeColor = Color.Red;
                        }
                        else if (num == 3)
                        {
                            dgv.Rows[j].Cells[i].Style.ForeColor = Color.Yellow;
                        }
                        else if (num == 4)
                        {
                            dgv.Rows[j].Cells[i].Style.ForeColor = Color.LightGreen;
                        }
                        else if (num == 5)
                        {
                            dgv.Rows[j].Cells[i].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            dgv.Rows[j].Cells[i].Style.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.LightGray;
                    }
                }
            }
        }
    }
}