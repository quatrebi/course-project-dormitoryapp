using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DormitoryApp.Models
{
    public class Account : INotifyPropertyChanged
    {

        private int m_uid;
        private string m_username;
        private string m_password;

        [Key]
        public int UID
        {
            get { return m_uid; }
            set
            {
                m_uid = value;
                OnPropertyChanged("UID");
            }
        }

        [Required]
        public string Username
        {
            get { return m_username; }
            set
            {
                m_username = value;
                OnPropertyChanged("Username");
            }
        }

        [Required]
        public string Password
        {
            get { return m_password; }
            set
            {
                m_password = value;
                OnPropertyChanged("Password");
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
