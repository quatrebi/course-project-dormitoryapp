using DormitoryApp.Models;
using DormitoryApp.Database;
using DormitoryApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace DormitoryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenuButton> Buttons { get; set; }
        private MenuButton m_selectedButton;
        public MenuButton SelectedButton
        {
            get => m_selectedButton;
            set
            {
                m_selectedButton = value;
                OnPropertyChanged("SelectedButton");
            }
        }

        private Page m_contentPage;
        public Page ContentPage
        {
            get => m_contentPage;
            set
            {
                m_contentPage = value;
                OnPropertyChanged("ContentPage");
            }
        }


        private RelayCommand m_loadPageCommand;
        public RelayCommand LoadPageCommand => m_loadPageCommand ??
            (m_loadPageCommand = new RelayCommand(obj =>
            {
                if (obj == null) return;

                // OBJ MUST BE A MENUBUTTON OR DATABASE MODEL
                Console.WriteLine("LoadPageCommand EXECUTED");
                string pageName = string.Empty;
                string modelName = string.Empty;
                Type modelType;

                if (obj is MenuButton)
                {
                    pageName = (obj as MenuButton).ViewName;
                    modelName = (obj as MenuButton).ModelName;
                    modelType = Type.GetType($"DormitoryApp.Database.{modelName}Model", false, true);
                }
                else
                {
                    pageName = "Model";
                    modelType = obj.GetType();
                }

                Type viewType = Type.GetType($"DormitoryApp.Views.{pageName}View", false, true);
                Page view = Activator.CreateInstance(viewType) as Page;

                Type viewmodelType = Type.GetType($"DormitoryApp.ViewModels.{pageName}ViewModel", false, true);
                object viewmodel = Activator.CreateInstance(viewmodelType);

                if (viewType == typeof(ModelView) && obj is DormitoryModel)
                    return;

                using (DatabaseModelContext db = new DatabaseModelContext())
                {
                    db.DormitoryModels.Load();
                    db.UserModels.Load();
                    db.RoomModels.Load();
                    db.ResidentModels.Load();
                    db.EmployeeModels.Load();

                    if (viewmodel is ModelViewModel)
                    {
                        var modelvm = viewmodel as ModelViewModel;
                        object model;
                        if (modelType == typeof(UserModel))
                        {
                            model = (LocalUser.Permission & 64) != 0 ? LocalUser : (LocalUser.EmployeeModel as object ?? LocalUser.ResidentModel);
                        }
                        else model = Convert.ChangeType(obj, modelType);
                        modelvm.DisplayedModels = new ObservableCollection<object>() { model };

                    }
                    else if (viewmodel is CollectionViewModel)
                    {

                        var collectionvm = viewmodel as CollectionViewModel;
                        if (modelType == typeof(ResidentModel))
                        {
                            if (LocalUser.EmployeeModel == null) return;
                            collectionvm.Models = new ObservableCollection<object>(db.ResidentModels
                                .Where(x => x.RoomModel.DormitoryModelDID == LocalUser.EmployeeModel.DormitoryModelDID).Prepend(new ResidentModel()
                                {
                                    CheckInDate = DateTime.Now,
                                    CheckOutDate = DateTime.Now.AddMonths(6),
                                    UserModel = new UserModel()
                                    {
                                        Permission = 1
                                    },
                                    RoomModel = new RoomModel()
                                    {
                                        DormitoryModel = LocalUser.EmployeeModel.DormitoryModel,
                                        DormitoryModelDID = LocalUser.EmployeeModel.DormitoryModelDID
                                    }
                                }));
                        }
                        if (modelType == typeof(EmployeeModel))
                        {
                            if (LocalUser.EmployeeModel == null) return;
                            collectionvm.Models = new ObservableCollection<object>(db.EmployeeModels
                                .Where(x => x.DormitoryModelDID == LocalUser.EmployeeModel.DormitoryModelDID).Prepend(new EmployeeModel()
                            {
                                UserModel = new UserModel()
                                {
                                    Permission = 63
                                },
                                DormitoryModel = LocalUser.EmployeeModel.DormitoryModel,
                                DormitoryModelDID = LocalUser.EmployeeModel.DormitoryModelDID
                            }));
                        }
                        if (modelType == typeof(DormitoryModel))
                            collectionvm.Models = new ObservableCollection<object>(db.DormitoryModels.Prepend(new DormitoryModel()));
                        if (modelType == typeof(RoomModel))
                            collectionvm.Models = new ObservableCollection<object>(db.RoomModels.Where(x => x.DormitoryModelDID == LocalUser.EmployeeModel.DormitoryModelDID));
                    }
                }

                view.DataContext = viewmodel;
                ContentPage = view;
            }));

        private RelayCommand m_addModelCommand;
        public RelayCommand AddModelCommand => m_addModelCommand ??
                (m_addModelCommand = new RelayCommand(obj =>
                {
                    Console.WriteLine("AddModelCommand EXECUTED");

                    using (DatabaseModelContext db = new DatabaseModelContext())
                    {
                        if (obj is DormitoryModel)
                        {
                            var dmodel = obj as DormitoryModel;
                            Console.WriteLine(dmodel.Name);
                            db.DormitoryModels.Add(dmodel);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch { return; }
                            List<RoomModel> rooms = new List<RoomModel>(dmodel.NumberOfFloors * dmodel.RoomsPerFloor);
                            for (int i = 0; i < rooms.Capacity; i++)
                            {
                                var rand = new Random(DateTime.Now.Millisecond);
                                rooms.Add(new RoomModel()
                                {
                                    DormitoryModelDID = dmodel.DID,
                                    Floor = i % dmodel.NumberOfFloors + 1,
                                    Number = i + 1,
                                    HeatSupply = rand.NextDouble() * rand.Next(40, 160) * App.HeatSupplyPerUnit,
                                    Electricity = rand.NextDouble() * rand.Next(1000, 3000) * App.ElectricityPerUnit
                                });
                            }
                            db.RoomModels.AddRange(rooms);
                            db.SaveChanges();
                            MainViewModel.Instance.LoadPageCommand.Execute(MainViewModel.Instance.SelectedButton);
                            return;
                        }
                        else if (obj is EmployeeLogModel)
                        {
                            var elmodel = obj as EmployeeLogModel;
                            var room = elmodel.RoomModel;
                            elmodel.Datetime = DateTime.Now;
                            elmodel.RoomModel = null;
                            db.EmployeeLogModels.Add(elmodel);
                            OnPropertyChanged("EmployeeLogs");
                            db.SaveChanges();
                            MainViewModel.Instance.LoadPageCommand.Execute(room);
                            return;
                        }
                        else
                        {
                            RegistrationView reg = new RegistrationView()
                            {
                                DataContext = new ModelViewModel()
                                {
                                    DisplayedModels = new ObservableCollection<object>() { obj }
                                }
                            };
                            if (reg.ShowDialog() != true) return;
                        }
                        if (obj is ResidentModel)
                        {
                            var rmodel = obj as ResidentModel;
                            rmodel.UserModel.Password = App.GetHash(rmodel.UserModel.Password);
                            db.UserModels.Add(rmodel.UserModel);
                            db.SaveChanges();
                            rmodel.RoomModelRID = db.RoomModels.Where(x => x.Number == rmodel.RoomModel.Number &&
                                                                      x.DormitoryModelDID == rmodel.RoomModel.DormitoryModelDID).Select(x => x.RID).FirstOrDefault();
                            rmodel.RoomModel = null;
                            db.ResidentModels.Add(rmodel);
                        }
                        if (obj is EmployeeModel)
                        {
                            var emodel = obj as EmployeeModel;
                            emodel.UserModel.Password = App.GetHash(emodel.UserModel.Password);
                            db.UserModels.Add(emodel.UserModel);
                            db.SaveChanges();

                            emodel.DormitoryModel = null;
                            db.EmployeeModels.Add(emodel);
                        }
                        db.SaveChanges();

                    }
                    MainViewModel.Instance.LoadPageCommand.Execute(MainViewModel.Instance.SelectedButton);
                }));


        private UserModel m_localUser;
        public UserModel LocalUser
        {
            get => m_localUser;
            set
            {
                m_localUser = value;
                OnPropertyChanged("LocalUser");
            }
        }

        public static MainViewModel Instance { get; private set; }
        public MainViewModel()
        {
            if (Instance == null) Instance = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
