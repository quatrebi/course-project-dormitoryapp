using DormitoryApp.Database;
using DormitoryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private readonly DatabaseModelContext db;
        private readonly List<string> blockedUsers;
        public RegistrationView()
        {
            InitializeComponent();
            MouseLeftButtonDown += App.Window_DragMove;
            db = new DatabaseModelContext();
            db.UserModels.Load();
            blockedUsers = new List<string>(db.UserModels.Select(x => x.Username));

            db.RoomModels.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Username_Validation(object sender, TextChangedEventArgs e)
        {
            bools[0] = !blockedUsers.Contains((sender as TextBox).Text) &&
                new Regex(@"^[a-zA-Z0-9]{3,}$").IsMatch((sender as TextBox).Text);
            Validate(sender);
        }

        private void Password_Validation(object sender, TextChangedEventArgs e)
        {
            bools[1] = new Regex(@"^[a-zA-Z0-9]{3,}$").IsMatch((sender as TextBox).Text);
            Validate(sender);
        }

        private bool[] bools = new bool[2];
        private void Validate(object sender)
        {
            if (bools[0] && bools[1])
            {
                AcceptButton.IsEnabled = true;
                AcceptButton.Opacity = 1;
            }
            else
            {
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.IndianRed);
                AcceptButton.IsEnabled = false;
                AcceptButton.Opacity = 0.25;
            }
        }

        private void NumberComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).ItemsSource = db.RoomModels
                .Where(x => x.DormitoryModelDID == MainViewModel.Instance.LocalUser.EmployeeModel.DormitoryModelDID)
                .Select(x => x.Number).ToList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mv = DataContext as ModelViewModel;
            (mv.DisplayedModel as ResidentModel).RoomModel.Number = (int)(sender as ComboBox).SelectedItem;
        }
    }
}
