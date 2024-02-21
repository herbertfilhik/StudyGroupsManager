// Unit tests for the StudyGroup class
using NUnit.Framework; // Importing NUnit framework for testing
using System; // Importing System namespace for basic functionalities
using System.Collections.Generic;
using StudyGroupsManager.src.Models; // Importing System.Collections.Generic for using Lists

namespace StudyGroupsManager.Tests.UnitTests // Defining the namespace for unit tests
{
    [TestFixture] // Indicates that this class contains tests
    public class StudyGroupTests // Class to contain unit tests for StudyGroup
    {
        [Test] // Indicates a test method
        public void AddUser_WhenCalled_ShouldAddUserToStudyGroup() // Test method to verify the AddUser functionality
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>()); // Creating a study group
            var user = new User { Id = 1, Name = "Maria" }; // Creating a user to be added to the study group

            // Act
            studyGroup.AddUser(user); // Adding the user to the study group

            // Assert
            Assert.Contains(user, studyGroup.Users); // Verifying if the user has been added to the study group
        }

        [Test] // Another test method
        public void RemoveUser_WhenCalled_ShouldRemoveUserFromStudyGroup() // Test method to verify the RemoveUser functionality
        {
            // Arrange
            var user = new User { Id = 1, Name = "Maria" }; // Creating a user
            var studyGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User> { user }); // Creating a study group and adding the user to it

            // Act
            studyGroup.RemoveUser(user); // Removing the user from the study group

            // Assert
            Assert.IsFalse(studyGroup.Users.Contains(user)); // Verifying if the user has been removed from the study group
        }

        [Test] // Another test method
        public void CreateStudyGroup_WithInvalidNameLength_ShouldThrowArgumentException() // Test method to verify the behavior when creating a study group with an invalid name length
        {
            // Arrange
            var nameTooShort = "Math"; // Setting a name that is too short
            var nameTooLong = new string('A', 31); // Setting a name that is too long

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(2, nameTooShort, Subject.Math, DateTime.Now, new List<User>())); // Verifying if ArgumentException is thrown for a name that is too short
            Assert.Throws<ArgumentException>(() => new StudyGroup(3, nameTooLong, Subject.Math, DateTime.Now, new List<User>())); // Verifying if ArgumentException is thrown for a name that is too long
        }

        [Test] // Another test method
        public void CreateStudyGroup_WithInvalidSubject_ShouldThrowArgumentException() // Test method to verify the behavior when creating a study group with an invalid subject
        {
            // Arrange
            var invalidSubject = (Subject)999; // Setting an invalid subject value

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(4, "Invalid Study Group", invalidSubject, DateTime.Now, new List<User>())); // Verifying if ArgumentException is thrown for an invalid subject
        }

        [Test] // Another test method
        public void CreateStudyGroup_WithNameWithinValidRange_ShouldSucceed() // Test method to verify the behavior when creating a study group with a valid name
        {
            // Arrange
            var validName = "Valid Group"; // Setting a valid name

            // Act & Assert
            var studyGroup = new StudyGroup(1, validName, Subject.Math, DateTime.Now, new List<User>()); // Creating a study group with a valid name
            Assert.That(studyGroup.Name, Is.EqualTo(validName)); // Verifying if the study group has been created with the expected name
        }

        [Test] // Another test method
        public void CreateStudyGroup_WithValidSubject_ShouldSucceed() // Test method to verify the behavior when creating a study group with a valid subject
        {
            // Arrange
            var validSubject = Subject.Chemistry; // Setting a valid subject

            // Act & Assert
            var studyGroup = new StudyGroup(1, "Chemistry Group", validSubject, DateTime.Now, new List<User>()); // Creating a study group with a valid subject
            Assert.That(studyGroup.Subject, Is.EqualTo(validSubject)); // Verifying if the study group has been created with the expected subject
        }

        [Test] // Another test method
        public void CreateStudyGroup_WithCreationDate_ShouldRecordCreationDate() // Test method to verify if the creation date is recorded when creating a study group
        {
            // Arrange
            var creationDate = DateTime.Now; // Setting the current date as the creation date

            // Act & Assert
            var studyGroup = new StudyGroup(1, "Physics Group", Subject.Physics, creationDate, new List<User>()); // Creating a study group with a specified creation date
            Assert.That(studyGroup.CreateDate, Is.EqualTo(creationDate)); // Verifying if the study group has been created with the expected creation date
        }

        [Test] // Another test method
        public void User_CanJoinMultipleStudyGroupsOfDifferentSubjects() // Test method to verify if a user can join multiple study groups of different subjects
        {
            // Arrange
            var user = new User { Id = 1, Name = "João" }; // Creating a user
            var mathGroup = new StudyGroup(1, "Math Group", Subject.Math, DateTime.Now, new List<User>()); // Creating a math study group
            var chemistryGroup = new StudyGroup(2, "Chemistry Group", Subject.Chemistry, DateTime.Now, new List<User>()); // Creating a chemistry study group

            // Act
            mathGroup.AddUser(user); // Adding the user to the math study group
            chemistryGroup.AddUser(user); // Adding the user to the chemistry study group

            // Assert
            Assert.IsTrue(mathGroup.Users.Contains(user), "User should be in the math group."); // Verifying if the user is in the math study group
            Assert.IsTrue(chemistryGroup.Users.Contains(user), "User should be in the chemistry group."); // Verifying if the user is in the chemistry study group
        }
    }
}
