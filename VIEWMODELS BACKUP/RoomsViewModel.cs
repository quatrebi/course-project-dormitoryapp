using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using DormitoryApp.Views;

namespace DormitoryApp.ViewModels
{
    public class RoomsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Room> Rooms { get; set; }
        private Room m_selectedRoom;
        public Room SelectedRoom
        {
            get => m_selectedRoom;
            set
            {
                m_selectedRoom = value;
                MainViewModel.Instance.LoadPageCommand.Execute(SelectedRoom);
                OnPropertyChanged("SelectedHuman");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
