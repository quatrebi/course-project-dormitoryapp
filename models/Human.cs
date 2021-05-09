using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp.Models
{
    public class Human : INotifyPropertyChanged
    {
        private string m_surname;
        private string m_name;
        private string m_patronymic;

        [Key]
        public int UID { get; set; }

        [Required]
        public string Surname
        {
            get { return m_surname; }
            set
            {
                m_surname = value;
                OnPropertyChanged("Surname");
            }
        }

        [Required]
        public string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }

        [Required]
        public string Patronymic
        {
            get { return m_patronymic; }
            set
            {
                m_patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}
