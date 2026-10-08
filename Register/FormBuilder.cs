using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Register
{
    public class FormBuilder
    {
        /// <summary>
        /// Синхронізація: при зміні cbSubject оновлюються доступні групи в cbGroup
        /// </summary>
        public void SyncComboBoxes(ComboBox cbSubject, ComboBox cbGroup, RegisterDB db, string role = "", int studentGroupId = 0)
        {
            cbSubject.DisplayMember = "Name";
            cbSubject.ValueMember = "Id";

            cbGroup.DisplayMember = "Id";
            cbGroup.ValueMember = "Id";

            cbSubject.SelectedIndexChanged += (s, e) =>
            {
                if (cbSubject.SelectedItem is not Subject selectedSubject)
                {
                    cbGroup.DataSource = null;
                    return;
                }

                // Знаходимо групи, в яких викладається обраний предмет
                var availableGroups = db.GroupSubjects
                    .Where(gs => gs.SubjectId == selectedSubject.Id && gs.Group != null)
                    .Select(gs => gs.Group!)
                    .DistinctBy(g => g.Id)
                    .ToList();

                if (role == "Студент" && studentGroupId != 0)
                {
                    availableGroups = availableGroups.Where(g => g.Id == studentGroupId).ToList();
                    cbGroup.Enabled = false;
                }
                else
                {
                    cbGroup.Enabled = true;
                }

                cbGroup.DataSource = availableGroups;
            };
        }

        /// <summary>
        /// Обчислення середнього балу для кожного предмету (заповнює останній рядок таблиці)
        /// </summary>
        public void CalculateAverageGradeForSubjects(BindingList<Grade> grades, DataTable dt)
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

        /// <summary>
        /// Обчислення середнього балу для кожного студента за його StudentEmail
        /// </summary>
        public void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt, string emailColumnName = "Email")
        {
            if (!dt.Columns.Contains(emailColumnName)) return;

            int lastColIndex = dt.Columns.Count - 1;
            var studentEmails = grades
                .Where(g => !string.IsNullOrEmpty(g.StudentEmail))
                .Select(g => g.StudentEmail)
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (string email in studentEmails)
            {
                // Знаходимо рядок поточного студента за значенням у колонці email
                DataRow? targetRow = dt.AsEnumerable()
                    .FirstOrDefault(row => string.Equals(row.Field<string>(emailColumnName), email, StringComparison.OrdinalIgnoreCase));

                if (targetRow == null) continue;

                var validGrades = grades
                    .Where(g => string.Equals(g.StudentEmail, email, StringComparison.OrdinalIgnoreCase) && g.Value > 0)
                    .ToList();

                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    // Записуємо середнє значення в останній стовпчик
                    targetRow[lastColIndex] = average.ToString("F2");
                }
            }
        }
    }
}