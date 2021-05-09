using DormitoryApp.Models;
using DormitoryApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public LoginViewModel()
        {
            CurrentAccount = new Account();
            Status = new LoginStatus();
        }

        private RelayCommand authCommand;
        public RelayCommand AuthCommand
        {
            get
            {
                return authCommand ??
                    (authCommand = new RelayCommand(obj =>
                    {
                        DispatcherTimer timer = new DispatcherTimer();
                        Status.ID = (int)LoginStatusID.Auth;

                        timer.Tick += (e, args) =>
                        {
                            timer.Stop();
                            var acc = obj as Account;

                            var founded = (from a in App.db.Accounts
                                           where a.Username == CurrentAccount.Username
                                           where a.Password == CurrentAccount.Password
                                           select a).ToArray();

                            if (founded.Length == 0) { Status.ID = (int)LoginStatusID.Failure; return; }
                            else Status.ID = (int)LoginStatusID.Success;
                            acc = founded[0];

                            MainView main = new MainView()
                            {
                                DataContext = new MainViewModel()
                                {
                                    CurrentHuman = (from h in App.db.Humans
                                                    where h.UID == acc.UID
                                                    select h).ToArray()[0],
                                    Buttons = new ObservableCollection<MenuButton>
                                    (
                                        (from btn in App.db.MenuButtons
                                         where (acc.Permission & btn.Permission) != 0
                                         select btn).ToArray()
                                    )
                                }
                            };
                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = main;
                            main.Show();
                        };
                        timer.Interval = new TimeSpan(0, 0, 1);
                        timer.Start();
                    }
                    ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
