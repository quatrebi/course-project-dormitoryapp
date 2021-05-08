using DormitoryApp.Models;
using DormitoryApp.Views;
using System;
using System.Collections.Generic;
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

        private Account m_authAccount;
        public Account AuthAccount
        {
            get { return m_authAccount; }
            set
            {
                m_authAccount = value;
                OnPropertyChanged("AuthAccount");
            }
        }

        public LoginViewModel()
        {
            AuthAccount = new Account();
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
                                           where a.Username == AuthAccount.Username
                                           where a.Password == AuthAccount.Password
                                           select a).ToArray();

                            if (founded.Length == 0) { Status.ID = (int)LoginStatusID.Failure; return; }
                            else Status.ID = (int)LoginStatusID.Success;

                            MainView main = new MainView();
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
