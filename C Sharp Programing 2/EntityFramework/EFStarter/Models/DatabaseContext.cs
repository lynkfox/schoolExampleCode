
using Microsoft.EntityFrameworkCore;
using EFStarter.Models;

namespace EFStarter.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString) : base(GetOptions(connectionString))
        {

        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        private static DbContextOptions GetOptions(string cnn)
        {
            var modelBuilder = new DbContextOptionsBuilder();
            return modelBuilder.UseSqlServer(cnn).Options;

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Animal> Animals { get; set; }


    }
}
