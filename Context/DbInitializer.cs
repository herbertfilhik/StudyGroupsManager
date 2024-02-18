using StudyGroupsManager.Context;
using StudyGroupsManager.Models;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated(); // Create the DB case it doesnt exist

        // Verify data existence
        if (!context.StudyGroups.Any())
        {
            // add data test
            var studyGroup = new StudyGroup(
                studyGroupId: 1, // Este é um valor de exemplo. Ajuste conforme a lógica do seu aplicativo.
                name: "Grupo de Estudo de Matemática",
                subject: Subject.Math, // Assumindo que `Subject` é um enum.
                createDate: DateTime.Now,
                users: new List<User>
                {
                    new User { Name = "Maria" },
                    new User { Name = "João" }
                }
            );

            context.StudyGroups.Add(studyGroup);
            context.SaveChanges();
        }
    }
}
