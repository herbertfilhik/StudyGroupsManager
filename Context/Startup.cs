using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyGroupsManager.Context;
using StudyGroupsManager.Models; // Adjust according to your context and repository correct namespace

namespace StudyGroupsManager
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Other configurations can be removed or conditioned to not interfere with tests
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Default configurations, like app.UseRouting(), app.UseAuthorization(), etc.

            // Initialize and add test data conditionally
            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Here you can check if the database is already populated
                // This is especially useful to avoid data duplication during development
                if (!context.StudyGroups.Any())
                {
                    AddTestData(context);
                }
            }
        }

        private void AddTestData(AppDbContext context)
        {
            // Create a list of users to add to the study group
            var users = new List<User>
            {
                new User
                {
                    // Initialize necessary user properties
                    Name = "Maria"
                    // Add other mandatory properties
                }
                // Add more users as needed
             };

            // Create a new study group using the constructor
            var studyGroup1 = new StudyGroup(
                studyGroupId: 1, // or another value as needed
                name: "Math Study Group",
                subject: Subject.Math,
                createDate: DateTime.Now,
                users: users
            );

            context.StudyGroups.Add(studyGroup1);
            context.SaveChanges();
        }

    }
}
