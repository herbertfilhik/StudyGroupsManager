﻿
namespace StudyGroupsManager.Models
{
    public interface IStudyGroupRepository
    {
        Task CreateStudyGroup(StudyGroup studyGroup);
        Task<IEnumerable<StudyGroup>> GetStudyGroups();
        Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject);
        Task JoinStudyGroup(int studyGroupId, int userId);
        Task LeaveStudyGroup(int studyGroupId, int userId);        
        Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithM();
        Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

        void GetStudyGroupsFilteredAndSorted(string v1, bool v2);       

        // Add other methods here if necessary
    }
}
