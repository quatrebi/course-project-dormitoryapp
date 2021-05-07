using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DormitoryApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>

    public partial class App : Application
    {
        public const double GoldenRatio = 1.61803399;
        public static Account account;
        public static DormitoryDatabase db;

        public static void Window_DragMove(object sender, MouseButtonEventArgs e)
            => (sender as Window).DragMove();
        private void AppShutdown(object sender, RoutedEventArgs e)
            => Current.Shutdown();

        public static string GetHash(string input)
            => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input)));

        //public static async Task<Account[]> GetAccountsAsync()
        //{
        //    Account[] tmp = new Account[] { };
        //    await db.Accounts.ForEachAsync(p => tmp.Append(p));
        //    return tmp;
        //}
    }
}
