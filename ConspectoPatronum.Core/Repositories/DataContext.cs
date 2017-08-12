using System.Data.Entity;
using ConspectoPatronum.Domain;

namespace ConspectoPatronum.Core.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
