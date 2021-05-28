using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DormitoryApp.Views;

namespace DormitoryApp.ViewModels
{
    public class DormitoriesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Dormitory> Dormitories { get; set; }
        private Dormitory m_selectedDormitory;
        public Dormitory SelectedDormitory
        {
            get => m_selectedDormitory;
            set
            {
                if (value.DID == -1) return;
                m_selectedDormitory = value;
                OnPropertyChanged("SelectedDormitory");
            }
        }

        private RelayCommand m_addDormitoryCommand;
        public RelayCommand AddDormitoryCommand => m_addDormitoryCommand ??
            (m_addDormitoryCommand = new RelayCommand(obj =>
            {
                var dorm = Dormitories[0];
                using (DormitoryDatabase db = new DormitoryDatabase())
                {
                    db.Dormitories.Load();
                    dorm.DID = (db.Dormitories.Local.Count() == 0 ? 0 : db.Dormitories.Max(x => x.DID)) + 1;
                    db.Dormitories.Add(dorm);

                    int s = (db.Rooms.Count() == 0 ? 0 : db.Rooms.Max(x => x.RID)) + 1;
                    for (short f = 0; f < dorm.NumberOfFloors; f++)
                    {
                        for (short r = 0; r < dorm.RoomsPerFloor; r++, s++)
                        {
                            db.Rooms.Add(new Room()
                            {
                                RID = s,
                                Floor = f,
                                Number = r,
                                DormitoryDID = dorm.DID,
                            });
                        }
                    }

                    db.SaveChanges();
                    db.Dormitories.Load();
                    Dormitories = new ObservableCollection<Dormitory>(db.Dormitories.Local.Prepend(new Dormitory() { DID = -1 }));
                    OnPropertyChanged("Dormitories");
                }
            }));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
