using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class Main : Form
    {
        public string filepath = "db.json";
        RegisterDB DataBase;
        public string email;
        public int groupID;
        public string role;

        public Main()
        {
            InitializeComponent();
        }

        // Конструктор для Студента
        public Main(string role, int groupID) : this()
        {
            this.groupID = groupID;
            this.role = role;
        }

        // Конструктор для Викладача (обов'язково з : this())
        public Main(string role) : this()
        {
            this.role = role;
        }

        public Main(string role, string email) : this()
        {
            this.role = role;
            this.email = email;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DataBase = new RegisterDB(filepath);
     AdapterDB.SetupDataGridView(dataGridView);
            dataGridView.DataSource = DataBase.Grades;
                        FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);
            FormBuilder.SetSubjectComboBox(comboBoxSubject, DataBase.Subjects);
  FormBuilder.SyncComboBoxes(comboBoxSubject, comboBoxGroup, DataBase, role, groupID);
            FormBuilder.FormatDataGridView(dataGridView);
          
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }
    }
}