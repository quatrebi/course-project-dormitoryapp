using DormitoryApp.Models;
using System.Data.Entity;

namespace DormitoryApp
{
    public class DormitoryDatabase : DbContext
    {
        public DormitoryDatabase() : base("DormitoryDB") { }

        public DbSet<Account> Accounts { get; set; }
    }
}
