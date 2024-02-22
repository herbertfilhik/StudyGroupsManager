using StudyGroupsManager.src.Models;

namespace StudyGroupsManager.src.DTOs
{
    public class StudyGroupCreationDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
    }

}
