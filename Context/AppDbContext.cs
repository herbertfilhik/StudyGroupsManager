using Microsoft.EntityFrameworkCore;
using StudyGroupsManager.Models;

namespace StudyGroupsManager.Context
{
    // Represents the database context for the application
    public class AppDbContext : DbContext
    {
        // Represents a DbSet for StudyGroup entities in the database
        public DbSet<StudyGroup> StudyGroups { get; set; }
        // Represents a DbSet for User entities in the database
        public DbSet<User> Users { get; set; }

        // Constructor that accepts DbContextOptions<AppDbContext> to initialize the context
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Method to configure the model that overrides the base class method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configures the CreateDate property of the StudyGroup entity to be required
            modelBuilder.Entity<StudyGroup>().Property(sg => sg.CreateDate).IsRequired();
            // Other model configurations
        }
    }
}
