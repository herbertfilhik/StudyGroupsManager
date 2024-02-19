using Moq; // Mocking library for creating mock objects
using StudyGroupsManager.Models; // Contains the models used in the application
using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC framework for handling web requests
using NUnit.Framework; // NUnit framework for unit testing
using System; // System namespace
using System.Collections.Generic; // Namespace for working with collections
using System.Threading.Tasks; // Namespace for working with asynchronous tasks

namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _mockRepository; // Mock repository for testing
        private StudyGroupController _controller; // Controller being tested

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IStudyGroupRepository>(); // Initializing mock repository
            _controller = new StudyGroupController(_mockRepository.Object); // Initializing controller with mock repository
        }

        // Test case to ensure CreateStudyGroup action returns OkResult when called
        [Test]
        public async Task CreateStudyGroup_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var studyGroupDto = new StudyGroupCreationDto
            {
                UserId = 1, // Supondo que UserId seja necessário
                Name = "Math Study Group",
                Subject = Subject.Math
            }; // Criando um objeto StudyGroupCreationDto
            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroupCreationDto>())) // Configurando o comportamento do repositório mock
                           .Returns(Task.CompletedTask); // Simulando o comportamento para retornar uma tarefa concluída

            // Act
            var result = await _controller.CreateStudyGroup(studyGroupDto); // Invocando o método de ação com o DTO correto

            // Assert
            Assert.IsInstanceOf<OkResult>(result); // Verificando se o resultado é do tipo OkResult
        }


        // Test case to ensure GetStudyGroups action returns all study groups
        [Test]
        public async Task GetStudyGroups_WhenCalled_ShouldReturnAllStudyGroups()
        {
            // Arrange
            var studyGroups = new List<StudyGroup> { new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>()) }; // Creating a list of study groups
            _mockRepository.Setup(repo => repo.GetStudyGroups()).ReturnsAsync(studyGroups); // Setting up mock repository behavior to return study groups

            // Act
            var result = await _controller.GetStudyGroups(); // Invoking the action method

            // Assert
            var objectResult = result as OkObjectResult; // Converting the result to OkObjectResult
            Assert.IsNotNull(objectResult); // Verifying if the result is not null
            var model = objectResult.Value as IEnumerable<StudyGroup>; // Extracting the value from the result
            Assert.IsNotNull(model); // Verifying if the model is not null
            Assert.That(model, Is.EquivalentTo(studyGroups)); // Verifying if the returned study groups are equivalent to the expected study groups
        }

        // Test case to ensure JoinStudyGroup action returns OkResult with valid data
        [Test]
        public async Task JoinStudyGroup_WithValidData_ShouldReturnOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.JoinStudyGroup(It.IsAny<int>(), It.IsAny<int>())) // Setting up mock repository behavior
                           .Returns(Task.CompletedTask); // Mocking the behavior to return a completed task

            // Act
            var result = await _controller.JoinStudyGroup(1, 1); // Invoking the action method

            // Assert
            Assert.IsInstanceOf<OkResult>(result); // Verifying if the result is of type OkResult
        }

        // Test case to ensure LeaveStudyGroup action returns OkResult with valid data
        [Test]
        public async Task LeaveStudyGroup_WithValidData_ShouldReturnOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.LeaveStudyGroup(It.IsAny<int>(), It.IsAny<int>())) // Setting up mock repository behavior
                           .Returns(Task.CompletedTask); // Mocking the behavior to return a completed task

            // Act
            var result = await _controller.LeaveStudyGroup(1, 1); // Invoking the action method

            // Assert
            Assert.IsInstanceOf<OkResult>(result); // Verifying if the result is of type OkResult
        }

        // Test case to ensure SearchStudyGroups action returns filtered study groups based on subject
        [Test]
        public async Task SearchStudyGroups_WithSubject_ShouldReturnFilteredStudyGroups()
        {
            // Arrange
            var subject = Subject.Math; // Setting up the subject for filtering
            var filteredStudyGroups = new List<StudyGroup> { new StudyGroup(1, "Math Group", subject, DateTime.Now, new List<User>()) }; // Creating a list of filtered study groups
            _mockRepository.Setup(repo => repo.SearchStudyGroups(subject.ToString())) // Setting up mock repository behavior to return filtered study groups
                           .ReturnsAsync(filteredStudyGroups);

            // Act
            var result = await _controller.SearchStudyGroups(subject.ToString()); // Invoking the action method

            // Assert
            var objectResult = result as OkObjectResult; // Converting the result to OkObjectResult
            Assert.IsNotNull(objectResult); // Verifying if the result is not null
            var returnedGroups = objectResult.Value as IEnumerable<StudyGroup>; // Extracting the value from the result
            Assert.IsNotNull(returnedGroups); // Verifying if the returned groups are not null
            Assert.That(returnedGroups, Is.EquivalentTo(filteredStudyGroups)); // Verifying if the returned study groups are equivalent to the filtered study groups
        }

        [Test]
        public async Task CreateStudyGroup_WhenUserAlreadyHasGroupForSubject_ShouldReturnBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            var studyGroupDto = new StudyGroupCreationDto
            {
                UserId = 1, // Exemplo de ID de usuário
                Name = "Grupo de Estudo de Matemática",
                Subject = Subject.Math
            };

            // Configura o mock para simular que o usuário já possui um grupo para o assunto
            mockRepository.Setup(repo => repo.UserAlreadyHasGroupForSubject(studyGroupDto.UserId, studyGroupDto.Subject))
                          .ReturnsAsync(true);

            // Act
            var result = await controller.CreateStudyGroup(studyGroupDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetStudyGroups_WhenFilteredBySubject_ShouldReturnFilteredStudyGroups()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            var subjectToFilter = Subject.Math; // Assunto pelo qual filtraremos os grupos de estudo
            var studyGroups = new List<StudyGroup>
            {
                new StudyGroup(1, "Math Study Group", Subject.Math, DateTime.Now, new List<User>()),
                new StudyGroup(2, "Chemistry Study Group", Subject.Chemistry, DateTime.Now, new List<User>())
            };
            // Configura o mock para retornar apenas os grupos de estudo filtrados pelo assunto especificado
            mockRepository.Setup(r => r.SearchStudyGroups(subjectToFilter.ToString()))
                          .ReturnsAsync(studyGroups.Where(sg => sg.Subject == subjectToFilter).ToList());

            // Act
            var result = await controller.SearchStudyGroups(subjectToFilter.ToString());

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "O resultado deve ser uma instância de OkObjectResult.");
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups, "A lista de grupos de estudo retornada não deve ser nula.");
            Assert.IsTrue(returnedStudyGroups.All(sg => sg.Subject == subjectToFilter), "Todos os grupos de estudo retornados devem ser do assunto filtrado.");
            Assert.AreEqual(returnedStudyGroups.Count, studyGroups.Count(sg => sg.Subject == subjectToFilter), "O número de grupos de estudo retornados deve corresponder ao número de grupos filtrados pelo assunto.");
        }

        [Test]
        public async Task GetStudyGroups_WhenSortedByCreationDate_ShouldReturnStudyGroupsInCorrectOrder()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            var studyGroups = new List<StudyGroup>
    {
        new StudyGroup(1, "Older Study Group", Subject.Math, DateTime.Now.AddDays(-10), new List<User>()),
        new StudyGroup(2, "Newer Study Group", Subject.Math, DateTime.Now, new List<User>())
    };

            // Ordena a lista em ordem decrescente de data de criação antes de configurar o mock
            var orderedStudyGroups = studyGroups.OrderByDescending(sg => sg.CreateDate).ToList();

            // Configura o mock para retornar os grupos de estudo em ordem de criação quando solicitado
            mockRepository.Setup(r => r.GetStudyGroupsSortedByCreationDate(It.IsAny<bool>()))
                          .ReturnsAsync(orderedStudyGroups);

            // Act
            var result = await controller.GetStudyGroupsSortedByCreationDate(true); // true para ordenação decrescente

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.AreEqual(2, returnedStudyGroups.Count);
            Assert.IsTrue(returnedStudyGroups[0].CreateDate > returnedStudyGroups[1].CreateDate, "Os grupos de estudo não estão ordenados corretamente pela data de criação.");
        }


        // Include new tests if necessary
    }
}
