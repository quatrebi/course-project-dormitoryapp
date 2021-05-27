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

namespace DormitoryApp.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
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

        private UniversityInfo m_university;
        public UniversityInfo University
        {
            get => m_university;
            set
            {
                m_university = value;
                OnPropertyChanged("University");
            }
        }

        private DormitoryInfo m_dormitory;
        public DormitoryInfo Dormitory
        {
            get => m_dormitory;
            set
            {
                m_dormitory = value;
                OnPropertyChanged("Dormitory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
