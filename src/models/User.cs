using System.ComponentModel.DataAnnotations;

namespace StudyGroupsManager.src.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Add the StudyGroupId attribute to associate users with study groups
        public int StudyGroupId { get; set; }

        // If necessary, you can also add a navigation property to represent the relationship with the study group
        // public StudyGroup StudyGroup { get; set; }
    }
}
