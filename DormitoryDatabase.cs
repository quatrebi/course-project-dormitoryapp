using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp
{
    public class DormitoryDatabase : DbContext
    {
        public DormitoryDatabase() : base("DormitoryDB") { }

        public DbSet<Account> Accounts { get; set; }
    }
}
