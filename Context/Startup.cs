using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyGroupsManager.Context;
using StudyGroupsManager.Data;
using StudyGroupsManager.Models; // Ajuste conforme o namespace correto do seu contexto e repositório

namespace StudyGroupsManager
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Outras configurações podem ser removidas ou condicionadas para não interferir nos testes
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configurações padrões, como app.UseRouting(), app.UseAuthorization(), etc.

            // Inicialize e adicione dados de teste de forma condicional
            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Aqui você pode verificar se o banco já está populado
                // Isso é especialmente útil para evitar duplicação de dados em desenvolvimento
                if (!context.StudyGroups.Any())
                {
                    AddTestData(context);
                }
            }
        }

        private void AddTestData(AppDbContext context)
        {
            // Cria uma lista de usuários para adicionar ao grupo de estudos
            var users = new List<User>
            {
                new User
                {
                    // Inicialize as propriedades necessárias do usuário
                    Name = "Maria"
                    // Adicione outras propriedades obrigatórias
                }
                // Adicione mais usuários conforme necessário
             };

            // Cria um novo grupo de estudos utilizando o construtor
            var studyGroup1 = new StudyGroup(
                studyGroupId: 1, // ou outro valor conforme necessário
                name: "Grupo de Estudo de Matemática",
                subject: Subject.Math,
                createDate: DateTime.Now,
                users: users
            );

            context.StudyGroups.Add(studyGroup1);
            context.SaveChanges();
        }

    }
}
