using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Data;

namespace Register
{
    public class FormBuilder
    {
        void SyncComboBoxes(ComboBox cb1, ComboBox cb2)
        {

        }

        void CalculateAverageGradeForSubjects(BindingList<Grade> grades, DataTable dt)
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

        void CalculateAverageGradeForStudents(BindingList<Grade> grades, DataTable dt)
        {
            var studentIds = grades.Select(g => g.StudentId).Distinct();
            foreach (int studentId in studentIds)
            {
                var validGrades = grades.Where(g => g.StudentId == studentId && g.Value > 0).ToList();
                if (validGrades.Count > 0)
                {
                    double average = validGrades.Average(g => g.Value);
                    dt.Rows[studentId.ToString()][dt.Columns.Count - 1] = average.ToString("F2");
                }
            }
        }
    }
}
