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
        }
        void FormatDataGridView(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                for (int j = 0; j < dgv.Rows.Count; j++)
                {
                    int num;
                    try
                    {
                        num = int.Parse(dgv.Rows[j].Cells[i].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Не вірні дані у комірці: " + dgv.Rows[j].Cells[i].Value.ToString());
                    }
                    if(num == 2)
                    {
                        dgv.Rows[j].Cells[i].Style.ForeColor = Color.Red;
                    }
                    else if(num == 3)
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
            }
        }
    }
}
