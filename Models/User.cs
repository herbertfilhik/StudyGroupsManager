using System.ComponentModel.DataAnnotations;

namespace StudyGroupsManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
