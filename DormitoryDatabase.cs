using DormitoryApp.Models;
using System.Data.Entity;

namespace DormitoryApp
{
    public class DormitoryDatabase : DbContext
    {
        public DormitoryDatabase() : base("DormitoryDB") { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<MenuButton> MenuButtons { get; set; }
    }
}
