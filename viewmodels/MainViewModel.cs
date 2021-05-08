using DormitoryApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public ObservableCollection<MenuButton> Buttons { get; set; }

        public MainViewModel()
        {
            Buttons = new ObservableCollection<MenuButton>()
            {
                new MenuButton() {IconSource = "bold-button.png", Text = "Bold Button" },
                new MenuButton() {IconSource = "italic-button.png", Text = "Italic Button" },
            };
            SelectedButton = Buttons[1];
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
