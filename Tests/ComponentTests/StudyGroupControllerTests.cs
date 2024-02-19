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
            var studyGroup = new StudyGroup { Name = "Math Study Group", Subject = Subject.Math }; // Creating a study group object
            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>())) // Setting up mock repository behavior
                           .Returns(Task.CompletedTask); // Mocking the behavior to return a completed task

            // Act
            var result = await _controller.CreateStudyGroup(studyGroup); // Invoking the action method

            // Assert
            Assert.IsInstanceOf<OkResult>(result); // Verifying if the result is of type OkResult
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

        // Include new tests if necessary
    }
}
