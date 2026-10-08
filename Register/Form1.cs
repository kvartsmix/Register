namespace Register
{


    public partial class Form1 : Form
    {
        public string email = string.Empty;
        public string role = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string email, string role) : this()
        {

        }
    }
}
