using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    public static class FormStyles
    {
        public static void StyleDataGridView(DataGridView dgv)
        {
            // Загальні налаштування стилю
            dgv.BackgroundColor = Color.FromArgb(248, 249, 250); // Світлий фон замість сірого
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 235, 240);

            // Шрифт та кольори звичайних клітинок
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 241, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(13, 110, 253);

            // Заголовок (шапка) таблиці
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 246);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 95);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;

            // Інші корисні налаштування
            dgv.RowTemplate.Height = 32; // Збільшені відступи у рядках
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Заповнення всієї ширини
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false; // Сховати крайній лівий порожній стовпчик
        }
        public static void StyleComboBox(ComboBox cb)
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = Color.White;
            cb.ForeColor = Color.FromArgb(33, 37, 41);
            cb.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }
        public static void StyleButton(Button btn)
        {
            // 1. Плоский стиль та прибрання стандартної рамки
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            // 2. Основні кольори та шрифт
            btn.BackColor = Color.FromArgb(13, 110, 253); // Bootstrap Blue
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand; // Вказівник-рука при наведенні

            // 3. Інтерактивні кольори (Hover / Click)
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 88, 202);  // Трохи темніший при наведенні
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(7, 66, 153);   // Ще темніший при кліку
        }
        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            txt.BackColor = Color.White;
            txt.ForeColor = Color.FromArgb(33, 37, 41);
        }
    }
}
