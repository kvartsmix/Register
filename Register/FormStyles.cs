using System;
using System.Drawing;
using System.Windows.Forms;

namespace Register
{
    public static class FormStyles
    {
       

        public static void StyleForm(Form form)
        {
            form.BackColor = BackgroundColor;
            form.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        }

        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = CardBackgroundColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = BorderColor;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = GridSelectionBack;
            dgv.DefaultCellStyle.SelectionForeColor = GridSelectionFore;
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBack;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderFore;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dgv.ColumnHeadersHeight = 40;

            dgv.RowTemplate.Height = 36;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        public static void StyleComboBox(ComboBox cb)
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = CardBackgroundColor;
            cb.ForeColor = TextPrimary;
            cb.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        public static void StyleButton(Button btn, bool isPrimary = true)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            if (isPrimary)
            {
                btn.BackColor = PrimaryColor;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = PrimaryHover;
                btn.FlatAppearance.MouseDownBackColor = PrimaryActive;
            }
            else
            {
                btn.BackColor = Color.FromArgb(241, 245, 249);
                btn.ForeColor = TextPrimary;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(203, 213, 225);
            }
        }

        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            txt.BackColor = CardBackgroundColor;
            txt.ForeColor = TextPrimary;
        }

        public static void StyleMenuStrip(MenuStrip ms)
        {
            ms.BackColor = Color.White;
            ms.ForeColor = TextPrimary;
            ms.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);    
            ms.Padding = new Padding(6, 4, 6, 4);
        }
        public static readonly Color PrimaryColor = Color.FromArgb(37, 99, 235);
        public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
        public static readonly Color PrimaryActive = Color.FromArgb(30, 64, 175);

        public static readonly Color BackgroundColor = Color.FromArgb(248, 250, 252);
        public static readonly Color CardBackgroundColor = Color.White;
        public static readonly Color BorderColor = Color.FromArgb(226, 232, 240);

        public static readonly Color TextPrimary = Color.FromArgb(30, 41, 59);
        public static readonly Color TextSecondary = Color.FromArgb(100, 116, 139);

        public static readonly Color GridHeaderBack = Color.FromArgb(241, 245, 249);
        public static readonly Color GridHeaderFore = Color.FromArgb(71, 85, 105);
        public static readonly Color GridRowAlternating = Color.FromArgb(250, 252, 255);
        public static readonly Color GridSelectionBack = Color.FromArgb(224, 236, 255);
        public static readonly Color GridSelectionFore = Color.FromArgb(29, 78, 216);

        public static readonly Color Grade2Back = Color.Black;
        public static readonly Color Grade2Fore = Color.White;

        public static readonly Color Grade3Back = Color.White;
        public static readonly Color Grade3Fore = Color.FromArgb(37, 99, 235);

        public static readonly Color Grade4Back = Color.White;
        public static readonly Color Grade4Fore = Color.FromArgb(22, 163, 74);

        public static readonly Color Grade5Back = Color.White;
        public static readonly Color Grade5Fore = Color.FromArgb(220, 38, 38);
    }
}