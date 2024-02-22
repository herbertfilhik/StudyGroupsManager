using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StudyGroupsManager.src.Context;
using StudyGroupsManager.src.Models;
using StudyGroupsManager.src.Repositories;

namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupRepositoryInSQLTests
    {

        private SqliteConnection _connection;
        private DbContextOptions<AppDbContext> _options;

        [SetUp]
        public void Setup()
        {
            // Configures SQLite in-memory mode
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Initializes the database
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Test]
        public async Task GetStudyGroupsWithUserStartingWithMSQL_ShouldReturnCorrectResults()
        {
            // Initializes the context and in-memory database
            using (var context = new AppDbContext(_options)) // Use _options here
            {
                var user = new User { Name = "Marcia" };
                var studyGroup = new StudyGroup(3, "Group M", Subject.Math, DateTime.Now, new List<User>());
                studyGroup.AddUser(user);

                context.StudyGroups.Add(studyGroup);
                await context.SaveChangesAsync();
            }

            // Tests repository functionality
            using (var context = new AppDbContext(_options)) // And also here
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

                Console.WriteLine($"Number of groups found: {result.Count()}");
                foreach (var group in result)
                {
                    Console.WriteLine($"Group ID: {group.StudyGroupId}, Group: {group.Name}, Users: {string.Join(", ", group.Users.Select(u => u.Name))}");
                }

                Assert.That(result, Is.Not.Empty);
                Assert.IsTrue(result.Any(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
                Assert.IsTrue(result.All(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
            }
        }

        [Test]
        public async Task InsertTenUsersWithAlternatingGroups_ShouldHaveAtLeastTwoUsersStartingWithM()
        {
            // Initializes the context and in-memory database
            using (var context = new AppDbContext(_options))
            {
                // Creates study groups
                var studyGroups = new List<StudyGroup>
                {
                    new StudyGroup(1, "Group 1", Subject.Math, DateTime.Now, new List<User>()),
                    new StudyGroup(2, "Group 2", Subject.Chemistry, DateTime.Now, new List<User>())
                };

                // Adds study groups to the context
                context.StudyGroups.AddRange(studyGroups);

                // Creates 10 users with alternating names
                var users = new List<User>
                {
                    new User { Id = 1, Name = "Carlson", StudyGroupId = 1 },
                    new User { Id = 2, Name = "Carlos", StudyGroupId = 2 },
                    new User { Id = 3, Name = "Manuel", StudyGroupId = 1 },
                    new User { Id = 4, Name = "Ana", StudyGroupId = 2 },
                    new User { Id = 5, Name = "Carla", StudyGroupId = 1 },
                    new User { Id = 6, Name = "Pedro", StudyGroupId = 2 },
                    new User { Id = 7, Name = "Miguel", StudyGroupId = 1 },
                    new User { Id = 8, Name = "Bianca", StudyGroupId = 2 },
                    new User { Id = 9, Name = "Lucas", StudyGroupId = 1 },
                    new User { Id = 10, Name = "Carcela", StudyGroupId = 2 }
                };

                // Adds users to the context
                context.Users.AddRange(users);

                await context.SaveChangesAsync();
            }

            // Tests repository functionality
            using (var context = new AppDbContext(_options))
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

                Console.WriteLine($"Number of groups found: {result.Count()}");
                foreach (var group in result)
                {
                    Console.WriteLine($"Group ID: {group.StudyGroupId}, Group: {group.Name}, Users: {string.Join(", ", group.Users.Select(u => u.Name))}");
                }

                Assert.That(result, Is.Not.Empty);
                Assert.IsTrue(result.Any(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
            }
        }

        [TearDown]
        public void Cleanup()
        {
            _connection?.Dispose();
        }
    }
}
