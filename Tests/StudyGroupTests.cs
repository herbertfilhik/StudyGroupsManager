using StudyGroupsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGroupsManager.Tests
{
    [TestFixture]
    public class StudyGroupTests
    {
        [Test]
        public void AddUser_WhenCalled_ShouldAddUserToStudyGroup()
        {
            // Arrange
            var studyGroup = new StudyGroup(1, "Grupo de Matemática", Subject.Math, DateTime.Now, new List<User>());
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
            var studyGroup = new StudyGroup(1, "Grupo de Matemática", Subject.Math, DateTime.Now, new List<User> { user });

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
            var nameTooLong = new string('A', 31); // Exemplo de um nome com 31 caracteres.

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(2, nameTooShort, Subject.Math, DateTime.Now, new List<User>()));
            Assert.Throws<ArgumentException>(() => new StudyGroup(3, nameTooLong, Subject.Math, DateTime.Now, new List<User>()));
        }

        [Test]
        public void CreateStudyGroup_WithInvalidSubject_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidSubject = (Subject)999; // Exemplo de valor inválido para enum.

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new StudyGroup(4, "Grupo de Estudo Inválido", invalidSubject, DateTime.Now, new List<User>()));
        }

        [Test]
        public void CreateStudyGroup_WithNameWithinValidRange_ShouldSucceed()
        {
            // Arrange
            var validName = "Grupo Valido";
            var studyGroup = new StudyGroup(1, validName, Subject.Math, DateTime.Now, new List<User>());

            // Act & Assert
            Assert.AreEqual(validName, studyGroup.Name);
        }

        [Test]
        public void CreateStudyGroup_WithValidSubject_ShouldSucceed()
        {
            // Arrange
            var validSubject = Subject.Chemistry;
            var studyGroup = new StudyGroup(1, "Grupo de Química", validSubject, DateTime.Now, new List<User>());

            // Act & Assert
            Assert.AreEqual(validSubject, studyGroup.Subject);
        }

        [Test]
        public void CreateStudyGroup_WithCreationDate_ShouldRecordCreationDate()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var studyGroup = new StudyGroup(1, "Grupo de Física", Subject.Physics, creationDate, new List<User>());

            // Act & Assert
            Assert.AreEqual(creationDate, studyGroup.CreateDate);
        }


        // Adicione mais testes conforme necessário para validação do nome, etc.
    }

}
