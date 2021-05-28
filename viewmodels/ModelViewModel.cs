using DormitoryApp.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp.ViewModels
{
    public class ModelViewModel : INotifyPropertyChanged
    {
        private bool m_isEditable = false;
        public bool IsEditable
        {
            get => m_isEditable;
            set
            {
                m_isEditable = value;
                OnPropertyChanged("IsEditable");
            }
        }

        public MainViewModel Instance => MainViewModel.Instance;

        private ObservableCollection<object> m_displayedModels;
        public ObservableCollection<object> DisplayedModels
        {
            get => m_displayedModels;
            set
            {
                m_displayedModels = value;
                OnPropertyChanged("DisplayedModel");
            }
        }
        public object DisplayedModel => DisplayedModels[0];
        public ObservableCollection<EmployeeLogModel> EmployeeLogs
        {
            get
            {
                if (!(DisplayedModel is RoomModel)) return new ObservableCollection<EmployeeLogModel>();
                var rmodel = DisplayedModel as RoomModel;
                return new ObservableCollection<EmployeeLogModel>(new DatabaseModelContext().EmployeeLogModels
                    .Where(x => x.RoomModelRID == rmodel.RID).OrderByDescending(x => x.Datetime));
            }
        }

        private EmployeeLogModel m_newEmployeeLogModel;

        public EmployeeLogModel NewEmployeeLog
        {
            get => m_newEmployeeLogModel ?? (m_newEmployeeLogModel = new EmployeeLogModel()
            {
                Datetime = DateTime.Now,
                RoomModel = DisplayedModel as RoomModel,
                RoomModelRID = (DisplayedModel as RoomModel).RID,
                EmployeeModelUID = MainViewModel.Instance.LocalUser.EmployeeModel.UID,
            });
            set
            {
                m_newEmployeeLogModel = value;
                OnPropertyChanged("NewEmployeeLog");
            }
        }
        private RelayCommand m_editModelCommand;
        public RelayCommand EditModelCommand => m_editModelCommand ??
            (m_editModelCommand = new RelayCommand(obj =>
            {
                Console.WriteLine("EditModelCommand EXECUTED");
                IsEditable = !IsEditable;
            }));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
