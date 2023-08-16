using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ElectricityMeterAutoTest.Entity;

namespace ElectricityMeterAutoTest.Entity
{
    public class Context : DbContext
    {
        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}