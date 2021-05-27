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
    public class HumansViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Human> Humans { get; set; }
        private Human m_selectedHuman;
        public Human SelectedHuman
        {
            get => m_selectedHuman;
            set
            {
                m_selectedHuman = value;
                if (SelectedHuman.HID == -1) AddTenantCommand.Execute(SelectedHuman is Citizen);
                //{
                //    SelectedHuman.DormitoryInfo = new DormitoryInfo
                //    {
                //        Dormitory = (MainViewModel.Instance.CurrentHuman as Employee).DormitoryInfo.Dormitory
                //    };
                //    SelectedHuman.DormitoryInfo.DormitoryDID = SelectedHuman.DormitoryInfo.Dormitory.DID;
                //    SelectedHuman.UniversityInfo = new UniversityInfo();
                //    SelectedHuman.Account = new Account() { Permission = 1 };
                //}
                else MainViewModel.Instance.LoadPageCommand.Execute(SelectedHuman);
                OnPropertyChanged("SelectedHuman");
            }
        }

        private RelayCommand m_addTenantCommand;
        public RelayCommand AddTenantCommand => m_addTenantCommand ??
            (m_addTenantCommand = new RelayCommand(obj =>
            {
                bool IsCitizen = (bool)obj;

                RegistrationView reg = new RegistrationView()
                {
                    DataContext = new RegistrationViewModel()
                    {
                        CurrentHuman = SelectedHuman,
                        IsCitizen = IsCitizen
                    }
                };

                if (MainViewModel.Instance.CurrentHuman.Account.Permission <= 63)
                {
                    (reg.DataContext as RegistrationViewModel).Dormitories = new ObservableCollection<Dormitory>()
                    {
                        (MainViewModel.Instance.CurrentHuman as Employee).DormitoryInfo.Dormitory
                    };
                }
                else (reg.DataContext as RegistrationViewModel).Dormitories = new ObservableCollection<Dormitory>(new DormitoryDatabase().Dormitories);

                DormitoryInfo di = new DormitoryInfo()
                {
                    Dormitory = (reg.DataContext as RegistrationViewModel).Dormitories.FirstOrDefault()
                };

                if (IsCitizen)
                {
                    (SelectedHuman as Citizen).UniversityInfo = new UniversityInfo();
                    (SelectedHuman as Citizen).DormitoryInfo = di;
                }
                else (SelectedHuman as Employee).DormitoryInfo = di;


                SelectedHuman.Account = new Account() { Permission = (short)(IsCitizen ? 1 : 31) };

                reg.ShowDialog();
                if (reg.DialogResult.Value && reg.DialogResult.HasValue)
                {
                    using (DormitoryDatabase db = new DormitoryDatabase())
                    {
                        var h = new Human()
                        {
                            HID = (db.Humans.Count() == 0 ? 0 : db.Humans.Max(x => x.HID)) + 1,
                            Surname = SelectedHuman.Surname,
                            Name = SelectedHuman.Name,
                            Patronymic = SelectedHuman.Patronymic,
                            Account = new Account()
                            {
                                AID = (db.Accounts.Count() == 0 ? 0 : db.Accounts.Max(x => x.AID)) + 1,
                                Username = SelectedHuman.Account.Username,
                                Password = SelectedHuman.Account.Password,
                                Permission = SelectedHuman.Account.Permission
                            }
                        };
                        DormitoryInfo dor = new DormitoryInfo()
                        {
                            DIID = (db.DormitoryInfos.Count() == 0 ? 0 : db.DormitoryInfos.Max(x => x.DIID)) + 1,
                            DormitoryDID = (reg.DataContext as RegistrationViewModel).SelectedDormitory.DID
                        };
                        if (IsCitizen)
                        {
                            dor.OrderNumber = (SelectedHuman as Citizen).DormitoryInfo.OrderNumber;
                            dor.CheckInDate = (SelectedHuman as Citizen).DormitoryInfo.CheckInDate;
                            dor.CheckOutDate = (SelectedHuman as Citizen).DormitoryInfo.CheckOutDate;
                            Citizen c = new Citizen()
                            {
                                HID = h.HID,
                                Surname = h.Surname,
                                Name = h.Name,
                                Patronymic = h.Patronymic,
                                Account = h.Account,
                                DormitoryInfo = dor
                            };
                            dor.Citizen = c;

                            c.UniversityInfo = new UniversityInfo()
                            {
                                UIID = (db.UniversityInfos.Count() == 0 ? 0 : db.UniversityInfos.Max(x => x.UIID)) + 1,
                                University = (SelectedHuman as Citizen).UniversityInfo.University,
                                Faculty = (SelectedHuman as Citizen).UniversityInfo.Faculty,
                                Specialty = (SelectedHuman as Citizen).UniversityInfo.Specialty,
                                GroupNumber = (SelectedHuman as Citizen).UniversityInfo.GroupNumber,
                                Citizen = c
                            };

                            db.Humans.Add(c);
                            Humans.Append(c);
                            OnPropertyChanged("Humans");
                        }
                        else
                        {
                            Employee e = new Employee()
                            {
                                HID = h.HID,
                                Surname = h.Surname,
                                Name = h.Name,
                                Patronymic = h.Name,
                                Account = h.Account,
                                Position = "Заведка",
                                DormitoryInfo = dor
                            };
                            dor.Employee = e;
                            db.Humans.Add(e);
                            Humans.Append(e);
                            OnPropertyChanged("Humans");
                        }

                        db.SaveChanges();
                    }
                }
                m_selectedHuman = new Human() { HID = -1 };

            }));

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
