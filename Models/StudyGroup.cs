using System.ComponentModel.DataAnnotations;

namespace StudyGroupsManager.Models
{
    public class StudyGroup
    {
        private const int MinNameLength = 5;
        private const int MaxNameLength = 30;

        // Parameterless constructor required for Entity Framework Core
        public StudyGroup()
        {
            Users = new List<User>();
        }

        // Constructor with parameters
        public StudyGroup(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
        {
            // Validate name length
            if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                throw new ArgumentException($"The group name must be between {MinNameLength} and {MaxNameLength} characters.");
            }

            // Verify if the subject is valid
            if (!Enum.IsDefined(typeof(Subject), subject))
            {
                throw new ArgumentException("Invalid subject.");
            }

            StudyGroupId = studyGroupId;
            Name = name;
            Subject = subject;
            CreateDate = createDate;
            Users = users ?? new List<User>();
        }

        // Define this property as the primary key
        [Key]
        public int StudyGroupId { get; set; }

        // Define constraints for the Name property
        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = "The group name must be between {2} and {1} characters.")]
        public string? Name { get; set; }

        public Subject Subject { get; set; } // Subject of the study group

        public DateTime CreateDate { get; set; } // Date when the study group was created

        public List<User> Users { get; private set; } // List of users in the study group

        // Method to add a user to the study group
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        // Method to remove a user from the study group
        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
    }

    // Enum representing different subjects
    public enum Subject
    {
        Math,
        Chemistry,
        Physics
    }
}
