using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using DormitoryApp.Database;

namespace DormitoryApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 


    public partial class App : Application
    {
        public const double GoldenRatio = 1.61803399;
        public static double HeatSupplyPerUnit = 21.9245;
        public static double ElectricityPerUnit = 0.1778;

        public static void Window_DragMove(object sender, MouseButtonEventArgs e) => (sender as Window).DragMove();
        private void AppShutdown(object sender, RoutedEventArgs e) => Current.Shutdown();

        public static string GetHash(string input)
            => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input ?? string.Empty)));
    }

    public class ModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item != null)
            {
                if (item is ResidentModel)
                    return element.FindResource("ResidentModelTemplate") as DataTemplate;
                if (item is UserModel)
                    return element.FindResource("UserModelTemplate") as DataTemplate;
                if (item is EmployeeModel)
                    return element.FindResource("EmployeeModelTemplate") as DataTemplate;
                if (item is EmployeeLogModel)
                    return element.FindResource("EmployeeLogModelTemplate") as DataTemplate;
                if (item is RoomModel)
                    return element.FindResource("RoomModelTemplate") as DataTemplate;
                if (item is DormitoryModel)
                    return element.FindResource("DormitoryModelTemplate") as DataTemplate;
                else
                    return element.FindResource("DefaultModelTemplate") as DataTemplate;
            }
            else return null;
        }
    }
}
