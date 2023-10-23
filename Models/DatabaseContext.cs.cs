using Microsoft.EntityFrameworkCore;

namespace Resume.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            // TODO: Database.EnsureCreated()
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Contact> Contacts { get; set; } = default!;

        public DbSet<Work> Works { get; set; } = default!;

        public DbSet<Social> Socials { get; set; } = default!;

        public DbSet<Biography> Biographies { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<StorageFile> StorageFiles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}