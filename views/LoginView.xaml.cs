using DormitoryApp.ViewModels;
using System.Windows;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            MouseLeftButtonDown += App.Window_DragMove;
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext == null) return;
            (DataContext as LoginViewModel).LocalUser.Password = PasswordInput.Password;
        }
    }
}
