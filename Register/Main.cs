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
        public string email;
        public string role;
        public Main()
        {
            InitializeComponent();
        }

        public Main(string email, string role) : this()
        { 
            this.email = email;
            this.role = role;
        }
    }
}
