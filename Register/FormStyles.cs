using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    public static class FormStyles
    {
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 235, 240);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 241, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(13, 110, 253);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 246);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 95);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 36;

            dgv.RowTemplate.Height = 32; 
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
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
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.BackColor = Color.FromArgb(13, 110, 253);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 88, 202);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(7, 66, 153);
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
