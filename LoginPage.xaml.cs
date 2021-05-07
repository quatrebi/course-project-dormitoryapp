using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DormitoryApp
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public LoginPage()
        {
            InitializeComponent();
            MouseLeftButtonDown += App.Window_DragMove;
            App.db = new DormitoryDatabase();
        }

        private void SubmitAuth(object sender, RoutedEventArgs e)
        {
            LoadingImage.Visibility = Visibility.Visible;
            Console.WriteLine(Resources["Username"].ToString());
            Console.WriteLine(Resources["Password"].ToString());

            try
            {
                string _username = (Resources["Username"] as LoginData).Data.ToString();
                string _password = App.GetHash((Resources["Password"] as LoginData).Data.ToString());

                //Account _user = new Account()
                //{
                //    UID = DateTime.Now.Millisecond,
                //    Username = _username,
                //    Password = _password
                //};

                //App.db.Accounts.Add(_user);
                //App.db.SaveChanges();
                //MessageBox.Show("User added!");
                Console.WriteLine("cur:\n{0} : {1}", (Resources["Username"] as LoginData).Data, (Resources["Password"] as LoginData).Data);
                var user = (from u in App.db.Accounts.AsParallel()
                            where u.Username == _username
                            where u.Password == _password
                            select u).ToArray();
                foreach (var u in user)
                    Console.WriteLine("{0} : {1}", u.Username, u.Password);

                if (user.Length == 0) { MessageBox.Show("Cannot find a user!"); return; }
                MessageBox.Show("Founded user!");
            }
            catch (DbEntityValidationException er)
            {
                foreach (var eve in er.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


            Hide();
            MainWindow main = new MainWindow();
            Application.Current.MainWindow = main;
            main.Show();
            Close();
        }
    }

    //public class LoginData : DependencyObject
    //{
    //    private static readonly DependencyProperty _usernameProperty;
    //    private static readonly DependencyProperty _passwrodProperty;

    //    static LoginData()
    //    {
    //        _usernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(LoginData));
    //        _passwrodProperty = DependencyProperty.Register("Password", typeof(string), typeof(LoginData));
    //    }

    //    public string Username
    //    {

    //        get { return (string)GetValue(_usernameProperty); }
    //        set { SetValue(_usernameProperty, value); }
    //    }

    //    public string Password
    //    {
    //        get { return (string)GetValue(_passwrodProperty); }
    //        set { SetValue(_passwrodProperty, value); }
    //    }
    //}
}
