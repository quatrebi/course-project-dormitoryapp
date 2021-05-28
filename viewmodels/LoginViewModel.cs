using DormitoryApp.Models;
using DormitoryApp.Database;
using DormitoryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DormitoryApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private StatusHandler m_status;
        public StatusHandler Status
        {
            get { return m_status; }
            set
            {
                m_status = value;
                OnPropertyChanged("Status");
            }
        }

        private UserModel m_localUser;
        public UserModel LocalUser
        {
            get { return m_localUser; }
            set
            {
                m_localUser = value;
                OnPropertyChanged("LocalUser");
            }
        }



        private Task taskDB;
        private readonly DatabaseModelContext localdb;
        public LoginViewModel()
        {
            LocalUser = new UserModel();
            localdb = new DatabaseModelContext();
            Status = new StatusHandler(typeof(LoginStatusID));
            taskDB = localdb.UserModels.LoadAsync();
        }

        private RelayCommand m_authCommand;
        public RelayCommand AuthCommand => m_authCommand ??
            (m_authCommand = new RelayCommand(_ =>
            {
                Console.WriteLine("AuthCommand EXECUTED");
                DispatcherTimer timer = new DispatcherTimer();
                Status.ID = (int)LoginStatusID.Auth;

                timer.Tick += (s, a) =>
                {
                    timer.Stop();
                    if (taskDB.Status == TaskStatus.Running) taskDB.Wait();
                    var founded = (from u in localdb.UserModels.Local
                                   where u.Username == LocalUser.Username &&
                                   u.Password == App.GetHash(LocalUser.Password)
                                   select u).ToArray();

                    if (founded.Length == 0 || founded?[0].Permission == 0) { Status.ID = (int)LoginStatusID.Failure; return; }
                    else Status.ID = (int)LoginStatusID.Success;
                    LocalUser = founded[0];
                    taskDB = localdb.MenuButtonModels.LoadAsync();

                    timer = new DispatcherTimer();
                    timer.Tick += (_s, _a) =>
                    {
                        timer.Stop();
                        taskDB.Wait();
                        MainView main = new MainView()
                        {
                            DataContext = new MainViewModel()
                            {
                                LocalUser = this.LocalUser,
                                Buttons = new ObservableCollection<MenuButton>((from btn in localdb.MenuButtonModels
                                                                               where (btn.Permission & LocalUser.Permission) != 0
                                                                               select btn).ToArray().Select(x => new MenuButton(x)))
                            }
                        };
                        Application.Current.MainWindow.Close();
                        Application.Current.MainWindow = main;
                        main.Show();

                    };
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                    timer.Start();
                };
                timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
                timer.Start();
            }));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
