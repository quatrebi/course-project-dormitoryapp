using DormitoryApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DormitoryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MenuButton m_selectedButton;
        public MenuButton SelectedButton
        {
            get { return m_selectedButton; }
            set
            {
                m_selectedButton = value;
                OnPropertyChanged("SelectedButton");
            }
        }

        private Human m_currentHuman;
        public Human CurrentHuman
        {
            get { return m_currentHuman; }
            set
            {
                m_currentHuman = value;
                OnPropertyChanged("CurrentHuman");
            }
        }

        public ObservableCollection<MenuButton> Buttons { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
