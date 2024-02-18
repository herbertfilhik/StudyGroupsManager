using Microsoft.EntityFrameworkCore;
using StudyGroupsManager.Models;

namespace StudyGroupsManager.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyGroup>().Property(sg => sg.CreateDate).IsRequired();
            // Outras configurações de modelo
        }
    }
}
