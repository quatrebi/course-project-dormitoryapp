using DormitoryApp.Models;
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

        private Account m_currentAccount;
        public Account CurrentAccount
        {
            get { return m_currentAccount; }
            set
            {
                m_currentAccount = value;
                OnPropertyChanged("CurrentAccount");
            }
        }

        private readonly Task taskDB;
        public LoginViewModel()
        {
            CurrentAccount = new Account();
            Status = new StatusHandler(typeof(LoginStatusID));
            taskDB = new DormitoryDatabase().Accounts.LoadAsync();
        }

        private RelayCommand m_authCommand;
        public RelayCommand AuthCommand => m_authCommand ??
            (m_authCommand = new RelayCommand(obj =>
            {
                DispatcherTimer timer = new DispatcherTimer();
                Status.ID = (int)LoginStatusID.Auth;
                var acc = obj as Account;

                timer.Tick += (s, a) =>
                {
                    timer.Stop();
                    if (taskDB.Status == TaskStatus.Running) taskDB.Wait();
                    var founded = (from u in new DormitoryDatabase().Accounts
                                   where u.Username == acc.Username &&
                                   u.Password == acc.Password
                                   select u).ToArray();
                    if (founded.Length == 0 || founded?[0].Permission == 0) { Status.ID = (int)LoginStatusID.Failure; return; }
                    else Status.ID = (int)LoginStatusID.Success;
                    acc = founded[0];

                    timer = new DispatcherTimer();
                    timer.Tick += (_s, _a) =>
                    {
                        timer.Stop();
                        MainView main = new MainView()
                        {
                            DataContext = new MainViewModel()
                            {
                                CurrentHuman = acc.Human,
                                Buttons = new ObservableCollection<MenuButton>((from btn in new DormitoryDatabase().MenuButtons
                                                                               where (btn.Permission & acc.Permission) != 0
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
