using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabWork2_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrtionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestLogin(LoginTextBox.Text.Trim());
                TestPassword(PasswordBox.Password, ConfirmPasswordBox.Password.Trim());
                TestEmail(EmailTextBox.Text.Trim());

                ShowSucces(LoginTextBox.Text.Trim());
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        void TestPassword(string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(password))
                throw new Exception("Введите пароль");

            if (String.IsNullOrEmpty(confirmPassword))
                throw new Exception("Введите подтверждение пароля");

            string regex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,30}$";
            if (!Regex.Match(password, regex).Success)
                throw new Exception("Пароль дожен быть от 8 до 30 сиволов, содержать латинские буквы верхнего и нижнего регистра, цифры и спецсимволы");

            if(password != confirmPassword)
                throw new Exception("Пароли не сопадают");

        }

        void TestLogin(string login)
        {
            if (String.IsNullOrEmpty(login))
                throw new Exception("Введите логин");
        }

        void TestEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new Exception("Введите пароль");

            string regex = @"^\S+@\S+\.\S+$";
            if (!Regex.Match(email, regex).Success)
                throw new Exception("Некорректно введена почта");
        }

        void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void ShowSucces(string login)
        {
            MessageBox.Show($"Вы зарегистрировались по логином {login}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}