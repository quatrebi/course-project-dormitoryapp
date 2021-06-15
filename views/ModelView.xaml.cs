using System.Collections.ObjectModel;
using System.Windows.Controls;
using DormitoryApp.Database;
using DormitoryApp.ViewModels;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ModelView.xaml
    /// </summary>
    public partial class ModelView : Page
    {
        public ModelView()
        {
            InitializeComponent();
        }

        private void DormitoryChanged_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as ComboBox).ItemsSource = new ObservableCollection<DormitoryModel>(new DatabaseModelContext().DormitoryModels);
        }

        private void DormitoryChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var db = new DatabaseModelContext();
            var empl = db.EmployeeModels.Find(MainViewModel.Instance.LocalUser.EmployeeModel.EID);
            empl.DormitoryModelDID = ((sender as ComboBox).SelectedItem as DormitoryModel).DID;
            db.SaveChanges();
            MainViewModel.Instance.LocalUser = db.UserModels.Find(MainViewModel.Instance.LocalUser.UID);
        }
    }
}
