using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp.Database
{
    public class ContextInitializer : IDatabaseInitializer<DatabaseModelContext>
    {
        public void InitializeDatabase(DatabaseModelContext db)
        {
            db.UserModels.Load();
            if (db.UserModels.Find(1) != null) return;
            Console.WriteLine("DATABASE WAS INITIALIZED");

            var dor = new DormitoryModel()
            {
                Name = "admindormitory",
                NumberOfFloors = 1,
                RoomsPerFloor = 1,
                CitizensPerRoom = 1
            };
            db.DormitoryModels.Add(dor);
            db.RoomModels.Add(new RoomModel()
            {
                DormitoryModelDID = dor.DID,
                Number = 1,
                Floor = 1,
                Electricity = 100,
                HeatSupply = 100
            });
            db.SaveChanges();

            db.UserModels.Add(new UserModel()
            {
                Username = "admin",
                Password = App.GetHash("admin"),
                Permission = 127,
                Surname = "Худницкий",
                Name = "Дмитрий",
                Patronymic = "Андреевич",
                EmployeeModel = new EmployeeModel()
                {
                    DormitoryModelDID = dor.DID,
                    Position = "Администратор",
                }
            });

            db.MenuButtonModels.AddRange(new List<MenuButtonModel>()
            {
                new MenuButtonModel() { MBID = 0, Caption = "Home", ImageSource = "home.png", ViewName = "Model", ModelName = "User", Permission = 1 },
                new MenuButtonModel() { MBID = 20, Caption = "Dormitories", ImageSource = "dormitories.png", ViewName = "Collection", ModelName = "Dormitory", Permission = 64 },
                new MenuButtonModel() { MBID = 30, Caption = "Rooms", ImageSource = "rooms.png", ViewName = "Collection", ModelName = "Room", Permission = 8 },
                new MenuButtonModel() { MBID = 40, Caption = "Citizens", ImageSource = "citizens.png", ViewName = "Collection", ModelName = "Resident", Permission = 8 },
                new MenuButtonModel() { MBID = 41, Caption = "Employees", ImageSource = "employees.png", ViewName = "Collection", ModelName = "Employee", Permission = 8 },

            });

            db.SaveChanges();
        }
    }
}
