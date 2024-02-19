using Microsoft.EntityFrameworkCore;
using StudyGroupsManager.Context;
using StudyGroupsManager.Repositories;
using StudyGroupsManager.Models;

namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupRepositoryInMemoryTests
    {
        [Test]
        public async Task GetStudyGroupsWithUserStartingWithM_ShouldReturnCorrectResults()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Initializes the context and the in-memory database
            using (var context = new AppDbContext(options))
            {
                var user = new User { Name = "Miguel" };
                var studyGroup = new StudyGroup(4, "Group M", Subject.Math, DateTime.Now, new List<User>());
                studyGroup.AddUser(user);

                context.StudyGroups.Add(studyGroup);
                await context.SaveChangesAsync();
            }

            // Tests the functionality of the repository
            using (var context = new AppDbContext(options))
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithM();

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
    }
}
