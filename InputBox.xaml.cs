using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DormitoryApp
{
    /// <summary>
    /// Логика взаимодействия для InputBox.xaml
    /// </summary>
    public partial class InputBox : UserControl
    {
        public InputBox() => InitializeComponent();

        public string Caption
        {
            get { return (string)CaptionLabel.Content; }
            set { CaptionLabel.Content = value; }
        }
    }

    public class LoginData : DependencyObject
    {
        private static readonly DependencyProperty _dataProperty;

        static LoginData()
        {
            _dataProperty = DependencyProperty.Register("Data", typeof(string), typeof(LoginData));
        }

        public string Data
        {
            get { return (string)GetValue(_dataProperty); }
            set { SetValue(_dataProperty, value); }
        }
    }

}
