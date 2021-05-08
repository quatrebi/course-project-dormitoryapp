using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DormitoryApp.Models
{
    public class MenuButton : INotifyPropertyChanged
    {
        private string m_iconSource;
        private string m_text;

        public Uri URI
        {
            get { return new Uri($"/DormitoryApp;component/resources/images/icons/{IconSource}", UriKind.Relative); }
        }

        public string IconSource
        {
            get { return m_iconSource; }
            set
            {
                m_iconSource = value;
                OnPropertyChanged("IconSource");
            }
        }

        public string Text
        {
            get { return m_text; }
            set
            {
                m_text = value;
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
