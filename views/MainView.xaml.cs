using DormitoryApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DormitoryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += App.Window_DragMove;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
            => this.BarPanel.Width = e.NewSize.Width - e.NewSize.Width / App.GoldenRatio * 1.15;
    }
}
