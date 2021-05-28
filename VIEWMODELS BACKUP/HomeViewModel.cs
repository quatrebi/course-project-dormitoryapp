using DormitoryApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DormitoryApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public string WelcomeMessage
        {
            get { return string.Format(Application.Current.TryFindResource("i18n-Welcome") as string, CurrentHuman.Name); }
        }

        private Citizen m_currentHuman;
        public Citizen CurrentHuman
        {
            get { return m_currentHuman; }
            set
            {
                m_currentHuman = value;
                OnPropertyChanged("CurrentHuman");
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
