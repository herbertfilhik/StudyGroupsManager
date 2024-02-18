using Microsoft.Data.Sqlite;
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
    public class StudyGroupRepositoryInSQLTests
    {

        private SqliteConnection _connection;
        private DbContextOptions<AppDbContext> _options;

        [SetUp]
        public void Setup()
        {
            // Configura SQLite em modo in-memory
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Inicializa o banco de dados
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Test]
        public async Task GetStudyGroupsWithUserStartingWithMSQL_ShouldReturnCorrectResults()
        {
            // Inicializa o contexto e o banco de dados em memória
            using (var context = new AppDbContext(_options)) // Use _options aqui
            {
                var user = new User { Name = "Marcia" };
                var studyGroup = new StudyGroup(3, "Grupo M", Subject.Math, DateTime.Now, new List<User>());
                studyGroup.AddUser(user);

                context.StudyGroups.Add(studyGroup);
                await context.SaveChangesAsync();
            }

            // Testa a funcionalidade do repositório
            using (var context = new AppDbContext(_options)) // E também aqui
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

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

        [Test]
        public async Task InsertTenUsersWithAlternatingGroups_ShouldHaveAtLeastTwoUsersStartingWithM()
        {
            // Inicializa o contexto e o banco de dados em memória
            using (var context = new AppDbContext(_options))
            {
                // Cria os grupos de estudo
                var studyGroups = new List<StudyGroup>
        {
            new StudyGroup(1, "Grupo 1", Subject.Math, DateTime.Now, new List<User>()),
            new StudyGroup(2, "Grupo 2", Subject.Chemistry, DateTime.Now, new List<User>())
        };

                // Adiciona os grupos de estudo ao contexto
                context.StudyGroups.AddRange(studyGroups);

                // Cria 10 usuários com nomes alternados
                var users = new List<User>
                {
                    new User { Id = 1, Name = "Marcia", StudyGroupId = 1 },
                    new User { Id = 2, Name = "Carlos", StudyGroupId = 2 },
                    new User { Id = 3, Name = "Manuel", StudyGroupId = 1 },
                    new User { Id = 4, Name = "Ana", StudyGroupId = 2 },
                    new User { Id = 5, Name = "Mariana", StudyGroupId = 1 },
                    new User { Id = 6, Name = "Pedro", StudyGroupId = 2 },
                    new User { Id = 7, Name = "Miguel", StudyGroupId = 1 },
                    new User { Id = 8, Name = "Bianca", StudyGroupId = 2 },
                    new User { Id = 9, Name = "Lucas", StudyGroupId = 1 },
                    new User { Id = 10, Name = "Carcela", StudyGroupId = 2 }
                };

                // Adiciona os usuários ao contexto
                context.Users.AddRange(users);

                await context.SaveChangesAsync();
            }

            // Testa a funcionalidade do repositório
            using (var context = new AppDbContext(_options))
            {
                var repository = new StudyGroupRepository(context);
                var result = await repository.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

                Console.WriteLine($"Número de grupos encontrados: {result.Count()}");
                foreach (var group in result)
                {
                    Console.WriteLine($"Grupo: {group.Name}, Usuários: {string.Join(", ", group.Users.Select(u => u.Name))}");
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
