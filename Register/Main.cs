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

        public Main(string role, int groudID) : this()
        {
            this.groupID = groupID;
            this.role = role;
    
        }

        public Main(string role)
        {
            this.role = role;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
             DataBase = new RegisterDB(filepath);
             dataGridView.DataSource = DataBase.Students;
             FormBuilder.SetGroupComboBox(comboBoxGroup, DataBase.Groups);
            FormBuilder.SetSubjectComboBox(comboBoxSubject, DataBase.Subjects);
        }
    }
}
