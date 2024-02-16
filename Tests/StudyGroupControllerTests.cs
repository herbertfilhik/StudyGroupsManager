using Moq;
using NUnit.Framework;
using StudyGroupsManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudyGroupsManager.Tests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _mockRepository;
        private StudyGroupController _controller;

        [SetUp]
        public void Setup()
        {
            // Inicializa o mock antes de cada teste
            _mockRepository = new Mock<IStudyGroupRepository>();
            _controller = new StudyGroupController(_mockRepository.Object);
        }

        [Test]
        public async Task CreateStudyGroup_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Grupo de Matemática", Subject.Math, DateTime.Now, new List<User>());

            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateStudyGroup(studyGroup);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task GetStudyGroups_WhenCalled_ShouldReturnAllStudyGroups()
        {
            // Arrange
            var studyGroups = new List<StudyGroup>
            {
                new StudyGroup(1, "Grupo de Matemática", Subject.Math, DateTime.Now, new List<User>())
                // Adicione mais grupos de estudo conforme necessário
            };

            _mockRepository.Setup(repo => repo.GetStudyGroups())
                           .ReturnsAsync(studyGroups);

            // Act
            var result = await _controller.GetStudyGroups();
            //var result = _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>())).Returns(Task.CompletedTask);

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult, "O resultado não é do tipo OkObjectResult.");

            var model = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(model, "O valor não é do tipo IEnumerable<StudyGroup>.");

            Assert.AreEqual(studyGroups.Count, model.Count(), "A contagem dos grupos de estudo não corresponde.");

        }

        [Test]
        public async Task SearchStudyGroups_WithValidSubject_ShouldReturnFilteredResults()
        {
            // Arrange
            var subject = Subject.Math;
            var filteredStudyGroups = new List<StudyGroup> { new StudyGroup(1, "Grupo de Matemática", subject, DateTime.Now, new List<User>()) };
            _mockRepository.Setup(repo => repo.SearchStudyGroups(It.Is<string>(s => s == subject.ToString())))
                           .ReturnsAsync(filteredStudyGroups);

            // Act
            var result = await _controller.SearchStudyGroups(subject.ToString());

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            var returnedGroups = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(returnedGroups);
            Assert.AreEqual(filteredStudyGroups.Count, returnedGroups.Count());
        }

        [Test]
        public async Task JoinStudyGroup_WithValidData_ShouldReturnOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.JoinStudyGroup(It.IsAny<int>(), It.IsAny<int>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.JoinStudyGroup(1, 1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task LeaveStudyGroup_WithValidData_ShouldReturnOk()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.LeaveStudyGroup(It.IsAny<int>(), It.IsAny<int>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.LeaveStudyGroup(1, 1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        // Os testes para verificar a ordenação dos grupos de estudo podem ser mais complexos e requerem um mock mais detalhado.
        // Isso pode envolver a ordenação dos dados retornados pelo repositório mockado ou a verificação dos parâmetros usados
        // para chamar o método do repositório que realiza a ordenação.

        // Implemente testes adicionais para adesão, visualização e saída de grupos conforme necessário
    }
}
