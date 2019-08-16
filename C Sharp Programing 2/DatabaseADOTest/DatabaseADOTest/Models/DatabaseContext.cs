using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatabaseADOTest.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString) : base (GetOptions(connectionString))
        {

        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
           
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            var modelBuilder = new DbContextOptionsBuilder();
            return modelBuilder.UseSqlServer(connectionString).Options;
        }
    }
}
