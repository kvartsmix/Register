using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Register
{
    public class FormBuilder
    {
        void SyncComboBoxes(ComboBox cb1, ComboBox cb2)
        {

        }

        void CalculateAverageGrade(BindingList<Student> students)
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
