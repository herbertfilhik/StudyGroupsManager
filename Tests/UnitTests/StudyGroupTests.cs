using StudyGroupsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGroupsManager.Tests.UnitTests
{
    [TestFixture]
    public class StudyGroupTests
    {
        [Test]
        public void AddUser_WhenCalled_ShouldAddUserToStudyGroup()
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>());
            var user = new User { Id = 1, Name = "Maria" };

            // Act
            studyGroup.AddUser(user);

            // Assert
            Assert.Contains(user, studyGroup.Users);
        }

        [Test]
        public void RemoveUser_WhenCalled_ShouldRemoveUserFromStudyGroup()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Maria" };
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User> { user });

            // Act
            studyGroup.RemoveUser(user);

            // Assert
            Assert.IsFalse(studyGroup.Users.Contains(user));
        }

        [Test]
        public void CreateStudyGroup_WithInvalidNameLength_ShouldThrowArgumentException()
        {
            // Arrange
            var nameTooShort = "Math";
            var nameTooLong = new string('A', 31); // Example of a 31-character name.

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(2, nameTooShort, Subject.Math, DateTime.Now, new List<User>()));
            Assert.Throws<ArgumentException>(() => new StudyGroup(3, nameTooLong, Subject.Math, DateTime.Now, new List<User>()));
        }

        [Test]
        public void CreateStudyGroup_WithInvalidSubject_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidSubject = (Subject)999; // Example of an invalid ENUM value.

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(4, "Invalid Study Group", invalidSubject, DateTime.Now, new List<User>()));
        }

        [Test]
        public void CreateStudyGroup_WithNameWithinValidRange_ShouldSucceed()
        {
            // Arrange
            var validName = "Valid Group";
            var studyGroup = new StudyGroup(1, validName, Subject.Math, DateTime.Now, new List<User>());

            // Act & Assert
            Assert.That(studyGroup.Name, Is.EqualTo(validName));
        }

        [Test]
        public void CreateStudyGroup_WithValidSubject_ShouldSucceed()
        {
            // Arrange
            var validSubject = Subject.Chemistry;
            var studyGroup = new StudyGroup(1, "Chemistry Group", validSubject, DateTime.Now, new List<User>());

            // Act & Assert
            Assert.That(studyGroup.Subject, Is.EqualTo(validSubject));
        }

        [Test]
        public void CreateStudyGroup_WithCreationDate_ShouldRecordCreationDate()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var studyGroup = new StudyGroup(1, "Physics Group", Subject.Physics, creationDate, new List<User>());

            // Act & Assert
            Assert.That(studyGroup.CreateDate, Is.EqualTo(creationDate));
        }

        [Test]
        public void User_CanJoinMultipleStudyGroupsOfDifferentSubjects()
        {
            // Arrange
            var user = new User { Id = 1, Name = "João" };
            var mathGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>());
            var chemistryGroup = new StudyGroup(2, "Chemistry Group", Subject.Chemistry, DateTime.Now, new List<User>());

            // Act
            mathGroup.AddUser(user);
            chemistryGroup.AddUser(user);

            // Assert
            Assert.IsTrue(mathGroup.Users.Contains(user), "User should be in the math group.");
            Assert.IsTrue(chemistryGroup.Users.Contains(user), "User should be in the chemistry group.");
        }

        // Add more tests as necessary for name validation, etc.
    }
}
