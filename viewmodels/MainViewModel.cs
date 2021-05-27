using DormitoryApp.Models;
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

namespace DormitoryApp.ViewModels
{
    //public class Navigation : INotifyPropertyChanged
    //{
    //    private string m_pageName;
    //    private object m_data;

    //    public object Data
    //    {
    //        get => m_data;
    //        set
    //        {
    //            m_data = value;
    //            OnPropertyChanged("Data");
    //        }
    //    }

    //    public Uri PageSource => new Uri($"/DormitoryApp;component/views/{m_pageName}View.xaml", UriKind.Relative);

    //    public string PageName
    //    {
    //        get => Application.Current.TryFindResource($"i18n-{m_pageName}") as string;
    //        set
    //        {
    //            m_pageName = value;
    //            OnPropertyChanged("PageName");
    //            OnPropertyChanged("PageSource");
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    //    public override string ToString() => $"-> {PageName}";
    //}
    public class MainViewModel : INotifyPropertyChanged
    {
        //public ObservableCollection<Navigation> NavigationBar { get; set; }
        //public Navigation SelectedNavigation
        //{
        //    get => NavigationBar.Last();
        //    set
        //    {
        //        NavigationBar = new ObservableCollection<Navigation>(NavigationBar.TakeWhile(x => x != value));
        //        OnPropertyChanged("SelectedNavigation");
        //        OnPropertyChanged("NavigationBar");
        //        OnPropertyChanged("FrameSource");
        //    }
        //}

        public ObservableCollection<MenuButton> Buttons { get; set; }
        private MenuButton m_selectedButton;
        public MenuButton SelectedButton
        {
            get => m_selectedButton;
            set
            {
                m_selectedButton = value;
                if (m_selectedButton != null) LoadPageCommand.Execute(m_selectedButton);
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
                string name = string.Empty;

                if (obj is MenuButton) name = (obj as MenuButton).ViewName;
                else SelectedButton = null;
                if (obj is Human) name = "Profile";
                if (obj is Room) name = "Room";

                Type type = Type.GetType($"DormitoryApp.Views.{name}View", false, true);
                Page page = Activator.CreateInstance(type) as Page;
                if (type == typeof(ProfileView))
                {
                    var citizen = obj is Human ? obj as Human : CurrentHuman;
                    if (citizen == null) page = new ErrorView() { DataContext = ErrorStatusID.NullReference };
                    else page.DataContext = new ProfileViewModel()
                    {
                        CurrentHuman = citizen,
                        University = (citizen as Citizen)?.UniversityInfo,
                        Dormitory = (citizen is Citizen ? (citizen as Citizen).DormitoryInfo : (citizen as Employee).DormitoryInfo)
                    };
                }
                else if (type == typeof(HumansView))
                {
                    bool IsCitizen = (obj as MenuButton).Caption == "Citizens";
                    var humans = new DormitoryDatabase().Humans.Where(x => IsCitizen ? x is Citizen : x is Employee);
                    if (IsCitizen)
                    {
                        page.DataContext = new HumansViewModel()
                        {
                            Humans = new ObservableCollection<Human>(humans.Select(x => x as Citizen).Prepend(new Citizen() { HID = -1 }))
                        };
                    }
                    else
                    {
                        page.DataContext = new HumansViewModel()
                        {
                            Humans = new ObservableCollection<Human>(humans.Select(x => x as Employee).Prepend(new Employee() { HID = -1 }))
                        };
                    }

                }
                else if (type == typeof(DormitoriesView))
                {
                    page.DataContext = new DormitoriesViewModel()
                    {
                        Dormitories = new ObservableCollection<Dormitory>(new DormitoryDatabase().Dormitories.Prepend(new Dormitory() { DID = -1 }))
                    };
                }
                else if (type == typeof(RoomsView))
                {
                    page.DataContext = new RoomsViewModel()
                    {
                        Rooms = new ObservableCollection<Room>(new DormitoryDatabase().Rooms)
                    };
                }
                ContentPage = page;
            }));


        private Human m_currentCitizen;
        public Human CurrentHuman
        {
            get
            {
                new DormitoryDatabase().Humans.Load();
                return m_currentCitizen;
            }
            set
            {
                m_currentCitizen = value;
                OnPropertyChanged("CurrentHuman");
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
