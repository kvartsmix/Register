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
            dataGridViewSemReport.Width = this.Width;
            dataGridViewSemReport.Height = this.Height - 50;
            FormStyles.StyleComboBox(comboBoxGroup);
            FormStyles.StyleDataGridView(dataGridViewSemReport);
            // 1. Заповнюємо групи
            FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);

            // 2. Виставляємо поточну або першу групу
            if (groupID > 0)
            {
                comboBoxGroup.SelectedValue = groupID;
            }
            else if (comboBoxGroup.SelectedItem is Group firstGroup)
            {
                this.groupID = firstGroup.Id;
            }

            // 3. Обмеження прав студента
            if (role == "Студент")
            {
                comboBoxGroup.Enabled = false;
                dataGridViewSemReport.ReadOnly = true;
            }

            // 4. Будуємо сітку
            UpdateReportGrid();

            // 5. Підписка на клік користувача по комбобоксу
            comboBoxGroup.SelectionChangeCommitted += ComboBoxGroup_SelectionChangeCommitted;
        }

        private void ComboBoxGroup_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            int selectedId = 0;

            // Перевіряємо обидва варіанти: або прив'язаний об'єкт Group, або число через SelectedValue
            if (comboBoxGroup.SelectedItem is Group grp)
            {
                selectedId = grp.Id;
            }
            else if (comboBoxGroup.SelectedValue != null && int.TryParse(comboBoxGroup.SelectedValue.ToString(), out int val))
            {
                selectedId = val;
            }

            if (selectedId > 0)
            {
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
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            dataGridViewSemReport.EndEdit();
            DataBase?.SaveToFile();
        }
        private void comboBoxGroup_SelectedIndexChanged()
        {

        }

        private void ExitToolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}