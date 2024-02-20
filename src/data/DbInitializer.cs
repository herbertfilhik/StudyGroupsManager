using StudyGroupsManager.src.Context;
using StudyGroupsManager.src.Models;

public static class DbInitializer
{
    // Ensure the database creation if it doesn't exist
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated(); // Create the DB case it doesnt exist

        // Verify data existence
        if (!context.StudyGroups.Any())
        {
            // add test data
            var studyGroup = new StudyGroup(
                studyGroupId: 1, // This is an example value. Adjust according to your application logic.
                name: "Math Study Group",
                subject: Subject.Math, // Assuming that `Subject` is an enum.
                createDate: DateTime.Now,
                users: new List<User>
                {
                    new User { Name = "Miguel" },
                    new User { Name = "John" }
                }
            );

            context.StudyGroups.Add(studyGroup);
            context.SaveChanges();
        }
    }
}
