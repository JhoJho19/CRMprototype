using System.Windows;

namespace ClientManagementApp
{
    public partial class LoginWindow : Window
    {
        public bool IsAdmin { get; private set; } = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "admin" && PasswordBox.Password == "Password_123")
            {
                MainWindow mainWindow = new MainWindow(true);
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

