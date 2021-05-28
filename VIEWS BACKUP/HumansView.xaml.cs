using DormitoryApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для HumansView.xaml
    /// </summary>
    public partial class HumansView : Page
    {
        public HumansView()
        {
            InitializeComponent();
        }
    }

    public class HumanDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item != null && item is Human)
            {
                if (item is Citizen)
                    return element.FindResource("CitizenDataTemplate") as DataTemplate;
                else
                    return element.FindResource("EmployeeDataTemplate") as DataTemplate;
            }
            else return null;
        }
    }
}
