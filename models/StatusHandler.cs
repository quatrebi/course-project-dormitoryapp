using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DormitoryApp.Models
{
    public abstract class StatusHandler : INotifyPropertyChanged
    {
        protected string[] m_messages = new string[] { "Unknown" };
        private int m_id = 0;

        public string Message
        {
            get { return Application.Current.TryFindResource($"i18n-{m_messages[ID]}") as string; }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public enum LoginStatusID : int
    {
        Unknown = 0,
        Auth, Success, Failure
    }

    public class LoginStatus : StatusHandler
    {
        public LoginStatus()
        {
            m_messages = new string[] { "EnterData", "Auth", "Succces", "Failure" };
        }

    }
}
