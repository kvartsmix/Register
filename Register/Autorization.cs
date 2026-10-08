using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class Autorization : Form
    {
        string FilePath = "db.json";
        public Autorization()
        {
            InitializeComponent();
        }

        private void Autorization_Load(object sender, EventArgs e)
        {

        }
        public void Autor()
        {
            string inputLogin = Login.Text.Trim();
            string inputPass = Pass.Text;

            if (string.IsNullOrWhiteSpace(inputLogin) || string.IsNullOrWhiteSpace(inputPass))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("Файл бази даних не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string jsonString = File.ReadAllText(FilePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            DiaryData data;
            try
            {
                data = JsonSerializer.Deserialize<DiaryData>(jsonString, options);
            }
            catch (Exception ex)
            {
                return;
            }
            User foundUser = null;
            string role = "";
            if (data.students != null)
            {
                foundUser = data.students.FirstOrDefault(u =>
                    string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);
                if (foundUser != null) role = "Студент";
            }

            if (foundUser == null && data.teachers != null)
            {
                foundUser = data.teachers.FirstOrDefault(u =>
                    string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);
                if (foundUser != null) role = "Викладач";
            }

            //if (foundUser == null && data.admins != null)
            //{
            //    foundUser = data.admins.FirstOrDefault(u =>
            //        string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);
            //    if (foundUser != null) role = "Адміністратор";
            //}


            if (foundUser != null)
            {
                Main form = new Main();
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль.", "Помилка авторизації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            Autor();
        }
    }

    public class User
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null ;
        public string? Password { get; set; } = null;
    }

    public class DiaryData
    {
        public List<User> students { get; set; } = new List<User>();
        public List<User> teachers { get; set; } = new List<User>();
        //public List<User> admins { get; set; } = new List<User>();
    }
}

