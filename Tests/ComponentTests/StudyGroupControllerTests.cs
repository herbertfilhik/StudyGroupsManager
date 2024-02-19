using Moq;
using StudyGroupsManager.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            _mockRepository = new Mock<IStudyGroupRepository>();
            _controller = new StudyGroupController(_mockRepository.Object);
        }

        [Test]
        public async Task CreateStudyGroup_WhenCalled_ShouldReturnOk()
        {
            var studyGroup = new StudyGroup { Name = "Math Study Group", Subject = Subject.Math };
            _mockRepository.Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
                           .Returns(Task.CompletedTask);

            var result = await _controller.CreateStudyGroup(studyGroup);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task GetStudyGroups_WhenCalled_ShouldReturnAllStudyGroups()
        {
            var studyGroups = new List<StudyGroup> { new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>()) };
            _mockRepository.Setup(repo => repo.GetStudyGroups()).ReturnsAsync(studyGroups);

            var result = await _controller.GetStudyGroups();

            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            var model = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(model);
            Assert.That(model, Is.EquivalentTo(studyGroups));
        }

        [Test]
        public async Task JoinStudyGroup_WithValidData_ShouldReturnOk()
        {
            _mockRepository.Setup(repo => repo.JoinStudyGroup(It.IsAny<int>(), It.IsAny<int>()))
                           .Returns(Task.CompletedTask);

            var result = await _controller.JoinStudyGroup(1, 1);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task LeaveStudyGroup_WithValidData_ShouldReturnOk()
        {
            _mockRepository.Setup(repo => repo.LeaveStudyGroup(It.IsAny<int>(), It.IsAny<int>()))
                           .Returns(Task.CompletedTask);

            var result = await _controller.LeaveStudyGroup(1, 1);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task SearchStudyGroups_WithSubject_ShouldReturnFilteredStudyGroups()
        {
            var subject = Subject.Math;
            var filteredStudyGroups = new List<StudyGroup> { new StudyGroup(1, "Math Group", subject, DateTime.Now, new List<User>()) };
            _mockRepository.Setup(repo => repo.SearchStudyGroups(subject.ToString()))
                           .ReturnsAsync(filteredStudyGroups);

            var result = await _controller.SearchStudyGroups(subject.ToString());

            var objectResult = result as OkObjectResult;
            Assert.IsNotNull(objectResult);
            var returnedGroups = objectResult.Value as IEnumerable<StudyGroup>;
            Assert.IsNotNull(returnedGroups);
            Assert.That(returnedGroups, Is.EquivalentTo(filteredStudyGroups));
        }

        // Include new tests if necessary
    }
}
