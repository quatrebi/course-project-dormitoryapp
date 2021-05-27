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
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private int m_totalFloors;
        private int m_numberOfFloor;
        public int NumberOfFloor
        {
            get => m_numberOfFloor;
            set
            {
                if (value < 0 || value > m_totalFloors) return;
                m_numberOfFloor = value;
                OnPropertyChanged("NumberOfFloor");
            }
        }

        public ObservableCollection<Dormitory> Dormitories { get; set; }
        private Dormitory m_selectedDormitory;
        public Dormitory SelectedDormitory
        {
            get => m_selectedDormitory;
            set
            {
                m_selectedDormitory = value;
                m_totalFloors = new DormitoryDatabase().Entry(SelectedDormitory).Entity.NumberOfFloors;
                OnPropertyChanged("SelectedDormitory");
            }
        }

        private Human m_currentCitizen;
        public Human CurrentHuman
        {
            get => m_currentCitizen;
            set
            {
                m_currentCitizen = value;
                OnPropertyChanged("CurrentHuman");
            }
        }
        public bool IsCitizen { get; set; }
        public Citizen GetCitizen => IsCitizen ? CurrentHuman as Citizen : new Citizen();
        public Employee GetEmployee => IsCitizen ? new Employee() : CurrentHuman as Employee;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
