using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StudyGroupsManager.Context;
using StudyGroupsManager.Data;
using StudyGroupsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupRepositoryTests
    {
        [Test]
        public async Task GetStudyGroupsWithUserStartingWithM_ShouldReturnCorrectResults()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Inicializa o contexto e o banco de dados em memória
            using (var context = new AppDbContext(options))
            {
                var user = new User { Name = "Maria" };
                var studyGroup = new StudyGroup(1, "Grupo M", Subject.Math, DateTime.Now, new List<User>());
                studyGroup.AddUser(user);

                context.StudyGroups.Add(studyGroup);
                await context.SaveChangesAsync();
            }

            // Testa a funcionalidade do repositório
            using (var context = new AppDbContext(options))
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithM();

                Console.WriteLine($"Número de grupos encontrados: {result.Count()}");
                foreach (var group in result)
                {
                    Console.WriteLine($"Grupo: {group.Name}, Usuários: {string.Join(", ", group.Users.Select(u => u.Name))}");
                }

                Assert.That(result, Is.Not.Empty);
                Assert.IsTrue(result.Any(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
                Assert.IsTrue(result.All(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
            }
        }
    }
}
