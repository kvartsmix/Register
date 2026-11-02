using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Register
{
    public partial class Autorization : Form
    {
        public static string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\db.json"));
        private DiaryData? data;

        public Autorization()
        {
            InitializeComponent();
        }

        public void Autorization_Load(object sender, EventArgs e)
        {
            FormStyles.StyleForm(this);
            FormStyles.StyleTextBox(Login);
            FormStyles.StyleTextBox(Pass);
            
            FormStyles.StyleButton(Enter, isPrimary: true);
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                data = JsonSerializer.Deserialize<DiaryData>(jsonString, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка читання JSON: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        public void Autor()
        {
            string inputLogin = Login.Text.Trim();
            string inputPass = Pass.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputLogin) || string.IsNullOrWhiteSpace(inputPass))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл бази даних не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (data == null)
            {
                MessageBox.Show("База даних порожня або не завантажена.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User? foundUser = null;
            string? role = null;

            foundUser = data.students?.FirstOrDefault(u =>
                string.Equals(u.Email, inputLogin, StringComparison.OrdinalIgnoreCase) && u.Password == inputPass);

            if (foundUser != null)
            {
                Main form = new Main();
                    if (foundUser != null)
                    {
                        role = "Адмін";
                    }
                }
                this.Hide();
                form.ShowDialog();
                Pass.Clear();
                this.Show();
            }
            if (foundUser == null || role == null)
            {
                MessageBox.Show("Невірний логін або пароль.", "Помилка авторизації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form form;

            switch (role)
            {
                case "Студент":
                    int groupId = foundUser.GroupId ?? 0;
                    form = new Main(role, groupId);
                    break;

                case "Викладач":
                    form = new Main(role, foundUser.Email ?? string.Empty);
                    break;

                case "Адмін":
                    form = new AdminForm();
                    break;

                default:
                    return;
            }

            Login.Text = "";
            Pass.Text = "";
            this.Hide();
            form.ShowDialog();
            Pass.Clear();
            this.Show();
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
        public List<User> admins { get; set; } = new();
    }
}