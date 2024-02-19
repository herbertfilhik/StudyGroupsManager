using Moq;
using StudyGroupsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _mockRepository;
        private StudyGroupController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize mock before each test
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

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult, "The result is not OkObjectResult type.");

            var model = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(model, " The value is not IEnumerable<StudyGroup> type.");

            Assert.That(model.Count(), Is.EqualTo(studyGroups.Count), "The count of the study groups does not match.");
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
            Assert.That(returnedGroups.Count(), Is.EqualTo(filteredStudyGroups.Count));
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

        [Test]
        public async Task CreateStudyGroup_WhenDuplicateSubjectForUser_ShouldReturnBadRequest()
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>());
            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
                           .Throws(new InvalidOperationException("User already has a study group for this subject."));

            // Act
            var result = await _controller.CreateStudyGroup(studyGroup);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task SearchStudyGroups_WithSubject_ShouldReturnFilteredStudyGroups()
        {
            // Arrange
            var subject = Subject.Math;
            var filteredStudyGroups = new List<StudyGroup>
    {
        new StudyGroup(1, "Math Group", subject, DateTime.Now, new List<User>())
    };
            _mockRepository.Setup(repo => repo.SearchStudyGroups(subject.ToString()))
                           .ReturnsAsync(filteredStudyGroups);

            // Act
            var result = await _controller.SearchStudyGroups(subject.ToString());

            // Assert
            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            var returnedGroups = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(returnedGroups);
            Assert.That(returnedGroups, Is.Not.Empty);
            foreach (var group in returnedGroups)
            {
                Assert.That(group.Subject, Is.EqualTo(subject));
            }
        }


        // Additional tests for joining, viewing, and leaving groups can be implemented as necessary.

    }
}
