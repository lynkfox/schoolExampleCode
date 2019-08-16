using Microsoft.EntityFrameworkCore;
using Schedule_Reader_1.Models;

namespace Schedule_Reader_1.Models
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

        public DbSet<Section> Sections { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }

        public DbSet<LastUpdate> LastUpdates { get; set; }



    }
}