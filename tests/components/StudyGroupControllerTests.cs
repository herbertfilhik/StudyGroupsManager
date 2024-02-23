using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudyGroupsManager.src.Context;
using StudyGroupsManager.src.DTOs;
using StudyGroupsManager.src.Models;
using StudyGroupsManager.src.Repositories;


namespace StudyGroupsManager.Tests.ComponentTests
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _mockRepository;  // Mock repository for testing
        private StudyGroupController _controller;  // Controller being tested

        private SqliteConnection _connection;
        private DbContextOptions<AppDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IStudyGroupRepository>(); // Initializing mock repository
            _controller = new StudyGroupController(_mockRepository.Object); // Initializing controller with mock repository

            // Configures SQLite in-memory mode
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Initializes the database
            using (var context = new AppDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        // Test case to ensure CreateStudyGroup action returns OkResult when called
        [Test]
        public async Task CreateStudyGroup_WhenCalled_ShouldReturnOk()
        {
            // Arrange
            var studyGroupDto = new StudyGroupCreationDto
            {
                UserId = 1,
                Name = "Math Study Group",
                Subject = Subject.Math
            };

            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateStudyGroup(studyGroupDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
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
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            int studyGroupId = 1;
            int userId = 1;

            // Simulating that the study group and the user exist
            mockRepository.Setup(repo => repo.GetStudyGroupById(studyGroupId))
                          .ReturnsAsync(new StudyGroup { StudyGroupId = studyGroupId, Users = new List<User>() });
            mockRepository.Setup(repo => repo.GetUserById(userId))
                          .ReturnsAsync(new User { Id = userId });

            // Configuring the mock behavior to simulate the successful addition of a user to the study group
            mockRepository.Setup(repo => repo.JoinStudyGroup(studyGroupId, userId))
                          .Returns(Task.CompletedTask);

            // Act
            var result = await controller.JoinStudyGroup(studyGroupId, userId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        // Test case to ensure LeaveStudyGroup action returns OkResult with valid data
        [Test]
        public async Task LeaveStudyGroup_WithValidData_ShouldReturnOk()
        {
            // Arrange
            int userId = 1;
            int studyGroupId = 1;

            // Configures the mock to simulate that the user is a member of the study group
            _mockRepository.Setup(repo => repo.IsUserMemberOfStudyGroup(userId, studyGroupId)).ReturnsAsync(true);

            // Configures the mock to simulate the behavior of successfully leaving the study group
            _mockRepository.Setup(repo => repo.LeaveStudyGroup(studyGroupId, userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.LeaveStudyGroup(studyGroupId, userId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
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
                UserId = 1,
                Name = "Math Study Group",
                Subject = Subject.Math
            };

            // Configures the mock to simulate that the user already has a group for the subject
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
            var subjectToFilter = Subject.Math;
            var studyGroups = new List<StudyGroup>
            {
                new StudyGroup(1, "Math Study Group", Subject.Math, DateTime.Now, new List<User>()),
                new StudyGroup(2, "Chemistry Study Group", Subject.Chemistry, DateTime.Now, new List<User>())
            };

            // Configures the mock to return only the study groups filtered by the specified subject
            mockRepository.Setup(r => r.SearchStudyGroups(subjectToFilter.ToString()))
                          .ReturnsAsync(studyGroups.Where(sg => sg.Subject == subjectToFilter).ToList());

            // Act
            var result = await controller.SearchStudyGroups(subjectToFilter.ToString());

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "The result should be an instance of OkObjectResult.");
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups, "The returned list of study groups should not be null.");
            Assert.IsTrue(returnedStudyGroups.All(sg => sg.Subject == subjectToFilter), "All returned study groups should belong to the filtered subject.");
            Assert.AreEqual(returnedStudyGroups.Count, studyGroups.Count(sg => sg.Subject == subjectToFilter), "The number of returned study groups should match the number of groups filtered by the subject.");
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

            // Sort the list in descending order of creation date before configuring the mock
            var orderedStudyGroups = studyGroups.OrderByDescending(sg => sg.CreateDate).ToList();

            // Configure the mock to return the study groups in order of creation when requested
            mockRepository.Setup(r => r.GetStudyGroupsSortedByCreationDate(It.IsAny<bool>()))
                          .ReturnsAsync(orderedStudyGroups);

            // Act
            var result = await controller.GetStudyGroupsSortedByCreationDate(true);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.AreEqual(2, returnedStudyGroups.Count);
            Assert.IsTrue(returnedStudyGroups[0].CreateDate > returnedStudyGroups[1].CreateDate, "The study groups are not correctly sorted by creation date.");
        }

        [Test]
        public async Task JoinStudyGroup_WhenUserAlreadyInAnotherGroupOfSameSubject_ShouldReturnBadRequest()
        {
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            int studyGroupId = 1;
            int userId = 1;

            // Configure the mock to simulate that the study group exists
            mockRepository.Setup(r => r.GetStudyGroupById(studyGroupId))
                          .ReturnsAsync(new StudyGroup { StudyGroupId = studyGroupId, Subject = Subject.Math });

            // Configure the mock to simulate that the user is already a member of a study group for the same subject
            mockRepository.Setup(r => r.UserIsMemberOfStudyGroupForSubject(userId, Subject.Math))
                          .ReturnsAsync(true);

            // Act
            var result = await controller.JoinStudyGroup(studyGroupId, userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateStudyGroup_WithInvalidNameLength_ShouldReturnValidationError()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            var tooShortName = "Mat"; // Less than 5 characters
            var tooLongName = new string('A', 31); // More than 30 characters
            var validSubject = Subject.Math;

            var studyGroupDtoShortName = new StudyGroupCreationDto { Name = tooShortName, Subject = validSubject };
            var studyGroupDtoLongName = new StudyGroupCreationDto { Name = tooLongName, Subject = validSubject };

            // Act
            var resultShortName = await controller.CreateStudyGroup(studyGroupDtoShortName);
            var resultLongName = await controller.CreateStudyGroup(studyGroupDtoLongName);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(resultShortName);
            Assert.IsInstanceOf<BadRequestObjectResult>(resultLongName);

            var badRequestResultShort = resultShortName as BadRequestObjectResult;
            var badRequestResultLong = resultLongName as BadRequestObjectResult;

            Assert.IsTrue(badRequestResultShort.Value.ToString().Contains("The name of the group must be between 5 and 30 characters."));
            Assert.IsTrue(badRequestResultLong.Value.ToString().Contains("The name of the group must be between 5 and 30 characters."));
        }

        [Test]
        public async Task CreateStudyGroup_WithInvalidSubject_ShouldReturnValidationError()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            var invalidSubjectValue = (Subject)999;
            var studyGroupDto = new StudyGroupCreationDto { Name = "Study Group", Subject = invalidSubjectValue };

            // Act
            var result = await controller.CreateStudyGroup(studyGroupDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Invalid Subject"));
        }

        [Test]
        public async Task LeaveStudyGroup_WhenUserIsNotMember_ShouldReturnBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IStudyGroupRepository>();
            var controller = new StudyGroupController(mockRepository.Object);
            int userId = 1;
            int studyGroupId = 1;

            // Configures the mock to simulate that the user is not a member of the study group
            mockRepository.Setup(r => r.IsUserMemberOfStudyGroup(userId, studyGroupId)).ReturnsAsync(false);

            // Act
            var result = await controller.LeaveStudyGroup(studyGroupId, userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetStudyGroupsWithUserStartingWithMInMemoryDataBase_ReturnsOkWithStudyGroups()
        {
            // Arrange
            var mockStudyGroups = new List<StudyGroup>
            {
                // Add here studygroup mock instances that satisfy the acceptance criteria                
                // Ex, user with groups with name iniate 'M'
            };

            _mockRepository.Setup(repo => repo.GetStudyGroupsWithUserStartingWithMInMemoryDataBase())
                .ReturnsAsync(mockStudyGroups);

            // Act
            var result = await _controller.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups);
            // Here you can add more asserts abour returned data 
        }

        [Test]
        public async Task GetStudyGroupsWithUserStartingWithM_ReturnsOkWithFilteredStudyGroups()
        {
            // Arrange
            var mockStudyGroups = new List<StudyGroup>
    {
        // Add here studeygroup mock instances that satisfy the acceptance criteria
        // Ex, user with groups with name iniate 'M'

        new StudyGroup
        {
            // Supondo que StudyGroup tenha uma propriedade 'Users' e 'Name', ajuste conforme seu modelo
            Name = "Math Group",
            Users = new List<User>
            {
                new User { Name = "Maria" },
                // Another users can add here
            }
            // Add another properties for initiate a study group
        }
    };

            _mockRepository.Setup(repo => repo.GetStudyGroupsWithUserStartingWithM())
                .ReturnsAsync(mockStudyGroups);

            // Act
            var result = await _controller.GetStudyGroupsWithUserStartingWithM();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups);
            // Verify id returned groups satisfy the specific criteria (users iniate with 'M')
            Assert.IsTrue(returnedStudyGroups.All(sg => sg.Users.Any(u => u.Name.StartsWith("M"))));
        }

        [Test]
        public async Task GetFilteredAndSortedStudyGroups_ReturnsFilteredAndSortedStudyGroups()
        {
            // Arrange
            var mockStudyGroups = new List<StudyGroup>
    {
        new StudyGroup { StudyGroupId = 1, Name = "Group A", Subject = Subject.Math, CreateDate = DateTime.Now.AddDays(-1), Users = new List<User>() },
        new StudyGroup { StudyGroupId = 2, Name = "Group B", Subject = Subject.Math, CreateDate = DateTime.Now, Users = new List<User>() },
        new StudyGroup { StudyGroupId = 3, Name = "Group C", Subject = Subject.Chemistry, CreateDate = DateTime.Now.AddDays(-2), Users = new List<User>() }
    };

            _mockRepository.Setup(repo => repo.GetStudyGroups())
                .ReturnsAsync(mockStudyGroups);

            string subjectFilter = "Math";
            bool sortByCreationDateDescending = true;

            // Act
            var result = await _controller.GetFilteredAndSortedStudyGroups(subjectFilter, sortByCreationDateDescending);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups);
            Assert.AreEqual(2, returnedStudyGroups.Count); // Verify is just Math groups are returned
            Assert.IsTrue(returnedStudyGroups[0].CreateDate > returnedStudyGroups[1].CreateDate); // Verify if groups return date by creation in unecessary form

        }

        [Test]
        public async Task GetFilteredAndSortedStudyGroups_WhenSortedAscending_ReturnsStudyGroupsSortedByCreationDateAscending()
        {
            // Arrange
            var mockStudyGroups = new List<StudyGroup>
    {
        new StudyGroup { StudyGroupId = 1, Name = "Grupo A", Subject = Subject.Math, CreateDate = DateTime.Now.AddDays(-1), Users = new List<User>() },
        new StudyGroup { StudyGroupId = 2, Name = "Grupo B", Subject = Subject.Math, CreateDate = DateTime.Now, Users = new List<User>() },
        new StudyGroup { StudyGroupId = 3, Name = "Grupo C", Subject = Subject.Chemistry, CreateDate = DateTime.Now.AddDays(-2), Users = new List<User>() }
    };

            _mockRepository.Setup(repo => repo.GetStudyGroups())
                .ReturnsAsync(mockStudyGroups);

            string subjectFilter = "Math";
            bool sortByCreationDateDescending = false; // Testing asc ordenation

            // Act
            var result = await _controller.GetFilteredAndSortedStudyGroups(subjectFilter, sortByCreationDateDescending);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStudyGroups = okResult.Value as List<StudyGroup>;
            Assert.IsNotNull(returnedStudyGroups);
            Assert.AreEqual(2, returnedStudyGroups.Count); // Verify if just Math Groups Returned
            Assert.IsTrue(returnedStudyGroups[0].CreateDate < returnedStudyGroups[1].CreateDate); // Verifica se os grupos de estudo estão ordenados por data de criação de forma ascendente
        }

        [Test]
        public async Task CreateStudyGroup_WhenRepositoryThrowsException_ReturnsBadRequestWithExceptionMessage()
        {
            // Arrange
            var studyGroupDto = new StudyGroupCreationDto
            {
                UserId = 1,
                Name = "Math Group",
                Subject = Subject.Math
            };

            var exceptionMessage = "Error study group creation.";
            _mockRepository.Setup(repo => repo.UserAlreadyHasGroupForSubject(studyGroupDto.UserId, studyGroupDto.Subject))
                           .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.CreateStudyGroup(studyGroupDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(exceptionMessage, badRequestResult.Value);
        }

        [Test]
        public async Task JoinStudyGroup_WhenStudyGroupNotFound_ReturnsNotFound()
        {
            // Arrange
            int nonExistentStudyGroupId = 999; // ID the represent inexistent group
            int userId = 1; // User id

            _mockRepository.Setup(repo => repo.GetStudyGroupById(nonExistentStudyGroupId))
                           .ReturnsAsync((StudyGroup)null); // Configure the rep to return null, simulate a not found group

            // Act
            var result = await _controller.JoinStudyGroup(nonExistentStudyGroupId, userId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result); // Verify if result is NotFoundObjectResult
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Study group not found.", notFoundResult.Value); // Verify error message
        }

        // Include new tests if necessary
    }
}
