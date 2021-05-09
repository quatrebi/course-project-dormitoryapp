using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DormitoryApp.Models
{
    public class MenuButton : INotifyPropertyChanged
    {
        private string m_iconSource;
        private string m_text;

        [Key]
        public int UID { get; set; }

        [Required]
        public short Permission { get; set; }

        public Uri URI
        {
            get { return new Uri($"/DormitoryApp;component/resources/images/icons/{IconSource}", UriKind.Relative); }
        }

        [Required]
        public string IconSource
        {
            get { return m_iconSource; }
            set
            {
                m_iconSource = value;
                OnPropertyChanged("IconSource");
            }
        }

        [Required]
        public string Text
        {
            get { return Application.Current.TryFindResource($"i18n-{m_text}") as string; }
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
