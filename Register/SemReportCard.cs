using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Register
{
    public partial class SemReportCard : Form
    {
        private int groupID;
        private bool hasChanged = false;
        private string role;
        public RegisterDB DataBase;

        public SemReportCard()
        {
            InitializeComponent();
            this.DataBase = Main.DataBase;
        }

        public SemReportCard(RegisterDB dataBase, string role, Group group) : this()
        {
            if (group != null && group.Id > 0)
                this.groupID = group.Id;

            this.role = role;
            this.DataBase = dataBase ?? Main.DataBase;
        }

        private void SemReportCard_Load(object sender, EventArgs e)
        {
            FormStyles.StyleComboBox(comboBoxGroup);
            FormStyles.StyleDataGridView(dataGridViewSemReport);

            FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);

            if (groupID > 0)
            {
                comboBoxGroup.SelectedValue = groupID;
            }
            else if (comboBoxGroup.SelectedItem is Group firstGroup)
            {
                this.groupID = firstGroup.Id;
            }

            if (role == "Студент")
            {
                comboBoxGroup.Enabled = false;
                dataGridViewSemReport.ReadOnly = true;
            }

            UpdateReportGrid();

            comboBoxGroup.SelectionChangeCommitted += ComboBoxGroup_SelectionChangeCommitted;
        }

        private void ComboBoxGroup_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            int selectedId = 0;

            if (comboBoxGroup.SelectedItem is Group grp)
                selectedId = grp.Id;
            else if (comboBoxGroup.SelectedValue != null && int.TryParse(comboBoxGroup.SelectedValue.ToString(), out int val))
                selectedId = val;

            if (selectedId > 0)
            {
                if (hasChanged)
                {
                    dataGridViewSemReport.EndEdit();
                    FormBuilder.ValidateChanges(hasChanged, DataBase);
                    hasChanged = false;
                }

                this.groupID = selectedId;
                UpdateReportGrid();
            }
        }

        private void UpdateReportGrid()
        {
            FormBuilder.SetReportDB(dataGridViewSemReport, DataBase, groupID > 0 ? groupID : null);
            FormBuilder.FormatDataGridView(dataGridViewSemReport);

            if (role == "Студент")
            {
                dataGridViewSemReport.ReadOnly = true;
            }
            hasChanged = false;
        }

        private void dataGridViewSemReport_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int avgColIndex = dataGridViewSemReport.Columns.Count - 1;

            if (e.ColumnIndex > 0 && e.ColumnIndex < avgColIndex)
            {
                hasChanged = true;

                var row = dataGridViewSemReport.Rows[e.RowIndex];
                string studentFullName = row.Cells[0].Value?.ToString() ?? "";
                string subjectName = dataGridViewSemReport.Columns[e.ColumnIndex].HeaderText;
                string cellVal = row.Cells[e.ColumnIndex].Value?.ToString()?.Trim() ?? "";
                var student = DataBase.Students.FirstOrDefault(s => $"{s.LastName} {s.FirstName}".Trim() == studentFullName && s.GroupId == groupID);
                var subject = DataBase.Subjects.FirstOrDefault(s => s.Name == subjectName);

                if (student != null && subject != null)
                {
                    var existingGrade = DataBase.Grades.FirstOrDefault(g =>
                        g.GroupId == groupID &&
                        g.SubjectId == subject.Id &&
                        string.Equals(g.StudentEmail, student.Email, StringComparison.OrdinalIgnoreCase));

                    if (double.TryParse(cellVal, out double newGrade) && newGrade >= 2.0 && newGrade <= 5.0)
                    {
                        if (existingGrade != null)
                        {
                            existingGrade.Value = newGrade;
                        }
                        else
                        {
                            int nextId = DataBase.Grades.Any() ? DataBase.Grades.Max(g => g.Id) + 1 : 1;
                            DataBase.Grades.Add(new Grade
                            {
                                Id = nextId,
                                GroupId = groupID,
                                SubjectId = subject.Id,
                                StudentEmail = student.Email,
                                Value = newGrade
                            });
                        }
                    }
                    else if (string.IsNullOrEmpty(cellVal) && existingGrade != null)
                    {
                        DataBase.Grades.Remove(existingGrade);
                    }
                }

                ApplyCellColor(dataGridViewSemReport.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                RecalculateRowAverage(e.RowIndex);
            }
        }

        private void RecalculateRowAverage(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dataGridViewSemReport.Rows.Count) return;

            var row = dataGridViewSemReport.Rows[rowIndex];
            double sum = 0;
            int count = 0;
            int avgColIndex = dataGridViewSemReport.Columns.Count - 1;

            for (int col = 1; col < avgColIndex; col++)
            {
                var val = row.Cells[col].Value;
                if (val != null && double.TryParse(val.ToString(), out double grade) && grade > 0)
                {
                    sum += grade;
                    count++;
                }
            }

            var avgCell = row.Cells[avgColIndex];
            if (count > 0)
            {
                double avg = Math.Round(sum / count, 2);
                avgCell.Value = avg.ToString("0.00");

                if (avg >= 4.0)
                    avgCell.Style.ForeColor = Color.ForestGreen;
                else if (avg < 3.0)
                    avgCell.Style.ForeColor = Color.Red;
                else
                    avgCell.Style.ForeColor = Color.Orange;
            }
            else
            {
                avgCell.Value = "";
                avgCell.Style.ForeColor = Color.Black;
            }
        }

        private void SemReportCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridViewSemReport.EndEdit();
            FormBuilder.ValidateChanges(hasChanged, DataBase);
        }

        private void ExitToolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewSemReport.EndEdit();
            DataBase.SaveToFile(Main.filepath);
            hasChanged = false;
        }
        private void ApplyCellColor(DataGridViewCell cell)
        {
            if (cell.Value != null && int.TryParse(cell.Value.ToString(), out int val))
            {
                switch (val)
                {
                    case 5:
                        cell.Style.ForeColor = Color.Red; 
                        break;
                    case 4:
                        cell.Style.ForeColor = Color.ForestGreen;
                        break;
                    case 3:
                        cell.Style.ForeColor = Color.Blue;
                        break;
                    case 2:
                        cell.Style.ForeColor = Color.White;
                        cell.Style.BackColor = Color.Black;
                        return;
                    default:
                        cell.Style.ForeColor = Color.Black;
                        break;
                }
                cell.Style.BackColor = Color.White;
            }
            else
            {
                cell.Style.ForeColor = Color.Black;
                cell.Style.BackColor = Color.White;
            }
        }
        private void dataGridViewSemReport_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            FormBuilder.DataGridView_CellValidating1(sender, e, dataGridViewSemReport, role);
        }
    }
}