using Microsoft.EntityFrameworkCore;

namespace CDFA_back.Models
{
    public class ProjectContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
