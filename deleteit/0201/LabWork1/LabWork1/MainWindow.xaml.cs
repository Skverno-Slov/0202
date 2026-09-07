using System.IO;
using System.Text.Json;
using System.Windows;

namespace LabWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FilePath = "users.json";
        public MainWindow()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, string>();
            }
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
        }

        private void SaveUsers(Dictionary<string, string> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.AppendAllText(FilePath, json);
        }

        private void password_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void login_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void reg_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = login_textbox.Text.Trim();
                string pass = password_textbox.Text.Trim();

                if (String.IsNullOrWhiteSpace(login))
                    MessageBox.Show("Заполните Логин");

                if (String.IsNullOrWhiteSpace(login))
                    MessageBox.Show("Заполните Пароль");

                var users = LoadUsers();

                if (users.ContainsKey(login))
                {
                    MessageBox.Show("Пользователь уже существует");
                    return;
                }

                users[login] = pass;
                SaveUsers(users);
            }
            catch
            {

            }
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textbox.Text.Trim();
            string pass = password_textbox.Text.Trim();

            var users = LoadUsers();

            if (users.TryGetValue(login, out string? savedPass) && savedPass == pass)
            {
                MessageBox.Show($"Добро прожаловать, {login}");
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}