using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace DormitoryApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>

    public partial class App : Application
    {
        public const double GoldenRatio = 1.61803399;

        public static void Window_DragMove(object sender, MouseButtonEventArgs e) => (sender as Window).DragMove();
        private void AppShutdown(object sender, RoutedEventArgs e) => Current.Shutdown();

        public static string GetHash(string input)
            => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input)));

        //public static async Task<Account[]> GetAccountsAsync()
        //{
        //    Account[] tmp = new Account[] { };
        //    await db.Accounts.ForEachAsync(p => tmp.Append(p));
        //    return tmp;
        //}
    }

    //public static class UiRefresh
    //{
    //    private static Action EmptyDelegate = delegate () { };
    //    public static void Refresh(this UIElement uiElement)
    //    {
    //        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    //    }
    //}
}
