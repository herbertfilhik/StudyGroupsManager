using StudyGroupsManager.src.Models;

namespace StudyGroupsManager.src.DTOs
{
    public class StudyGroupJoinDto
    {
        public int UserId { get; set; }
        public Subject Subject { get; set; }
    }
}
