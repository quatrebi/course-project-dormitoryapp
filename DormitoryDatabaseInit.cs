using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp
{
    public class DormitoryDatabaseInit : IDatabaseInitializer<DormitoryDatabase>
    {
        public void InitializeDatabase(DormitoryDatabase db)
        {
            if (db.Accounts.Find(127) != null) return;
            Console.WriteLine("INITIED");
            Citizen c = new Citizen()
            {
                HID = 127,
                Surname = "Администратор"
            };
            db.Humans.Add(c);
            db.Accounts.Add(new Account()
            {
                AID = 127,
                Username = "admin",
                Password = "admin",
                Permission = 127,
                Human = c
            });
            db.MenuButtons.AddRange(new List<MenuButtonModel>()
            {
                new MenuButtonModel() { MBID = 0, Caption = "Home", ImageSource = "home.png", ViewName = "Profile", Permission = 1 },
                //new MenuButtonModel() { MBID = 10, Caption = "Profile", ImageSource = "profile.png", ViewName = "Profile", Permission = 1 },
                new MenuButtonModel() { MBID = 20, Caption = "Dormitories", ImageSource = "dormitories.png", ViewName = "Dormitories", Permission = 64 },
                new MenuButtonModel() { MBID = 30, Caption = "Rooms", ImageSource = "rooms.png", ViewName = "Rooms", Permission = 8 },
                new MenuButtonModel() { MBID = 40, Caption = "Citizens", ImageSource = "citizens.png", ViewName = "Humans", Permission = 8 },
                new MenuButtonModel() { MBID = 41, Caption = "Employees", ImageSource = "employees.png", ViewName = "Humans", Permission = 8 },
            });
            db.SaveChanges();
        }
    }
}
