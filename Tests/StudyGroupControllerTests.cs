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
            // Initiate mock before each test
            _mockRepository = new Mock<IStudyGroupRepository>();
            _controller = new StudyGroupController(_mockRepository.Object);
        }

        [Test]
        public async Task CreateStudyGroup_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>());

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
                new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>())                
                // Add more study groups as necessary
            };

            _mockRepository.Setup(repo => repo.GetStudyGroups())
                           .ReturnsAsync(studyGroups);

            // Act
            var result = await _controller.GetStudyGroups();
            //var result = _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>())).Returns(Task.CompletedTask);

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult, "The result is not OkObjectResult type.");

            var model = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(model, " The value is not IEnumerable<StudyGroup> type.");

            Assert.AreEqual(studyGroups.Count, model.Count(), "The count of the study groups does not match.");

        }

        [Test]
        public async Task SearchStudyGroups_WithValidSubject_ShouldReturnFilteredResults()
        {
            // Arrange
            var subject = Subject.Math;
            var filteredStudyGroups = new List<StudyGroup> { new StudyGroup(1, "Math Group", subject, DateTime.Now, new List<User>()) };
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

        // The tests to verify the sorting of the study groups can be more complex and require a more detailed mock.
        // This may involve sorting the data returned by the mocked repository or verifying the parameters used
        // to call the repository method that performs the sorting.

        // Implement additional tests for joining, viewing, and leaving groups as necessary.

    }
}
