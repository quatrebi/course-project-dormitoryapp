using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DormitoryApp.Models
{
    /// <summary>
    ///  MAYBE TO DELETE?
    /// </summary>
    public class StatusHandler : INotifyPropertyChanged
    {
        private int m_id = 0;
        protected string[] m_messages = new string[] { "Unknown" };

        public string Message => Application.Current.TryFindResource($"i18n-{m_messages[ID]}") as string;

        public int ID
        {
            get { return m_id; }
            set
            {
                m_id = value;
                OnPropertyChanged("ID");
                OnPropertyChanged("Message");
            }
        }

        public StatusHandler(Type enumType) => m_messages = Enum.GetNames(enumType);

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public enum LoginStatusID : int
    {
        EnterData = 0, Auth, Success, Failure, UnknownError
    }

    public enum ErrorStatusID : int
    {
        NullReference = 0
    }
}
