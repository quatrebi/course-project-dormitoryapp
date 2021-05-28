using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DormitoryApp.Database;
using DormitoryApp.Views;
using DormitoryApp.ViewModels;

namespace DormitoryApp.ViewModels
{
    public class CollectionViewModel : INotifyPropertyChanged
    {
        public MainViewModel Instance => MainViewModel.Instance;
        public ObservableCollection<object> Models { get; set; }
        private object m_selectedModel;
        public object SelectedModel
        {
            get => m_selectedModel;
            set
            {
                m_selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }

        public object FirstModel => Models[0];

        public RelayCommand AddModelCommand => MainViewModel.Instance.AddModelCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
