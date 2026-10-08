using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Register
{
    public partial class Autorization : Form
    {
        private const string FilePath = "db.json";

        public Autorization()
        {
            InitializeComponent();
        }

        public void Autorization_Load(object sender, EventArgs e)
        {
            // Any initialization code can go here if needed
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

            DiaryData? data;
            try
            {
                string jsonString = File.ReadAllText(FilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                data = JsonSerializer.Deserialize<DiaryData>(jsonString, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка читання JSON: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (data == null)
            {
                MessageBox.Show("База даних порожня.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? foundUser = null;
            string role = "";

            // Пошук серед студентів
            foundUser = data.students?.FirstOrDefault(u =>
                string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);

            if (foundUser != null)
            {
                role = "Студент";
            }
            else
            {
                // Пошук серед викладачів
                foundUser = data.teachers?.FirstOrDefault(u =>
                    string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);

                if (foundUser != null)
                {
                    role = "Викладач";
                }
            }

            if (foundUser != null)
            {
                Main form;

                // Якщо студент — передаємо роль та GroupId, якщо викладач — лише роль
                if (role == "Студент")
                {
                    int studentGroupId = foundUser.GroupId ?? 0;
                    form = new Main(role, studentGroupId);
                }
                else
                {
                    form = new Main(role);

                }

                this.Hide();
                form.ShowDialog();
                Pass.Clear();
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
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("group_id")]
        public int? GroupId { get; set; }
    }

    public class DiaryData
    {
        public List<User> students { get; set; } = new();
        public List<User> teachers { get; set; } = new();
    }
}