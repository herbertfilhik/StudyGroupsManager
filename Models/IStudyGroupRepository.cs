namespace StudyGroupsManager.Models
{
    // Interface for managing study groups
    public interface IStudyGroupRepository
    {
        // Creates a new study group
        Task CreateStudyGroup(StudyGroupCreationDto studyGroupDto);


        // Retrieves all study groups
        Task<IEnumerable<StudyGroup>> GetStudyGroups();

        // Searches for study groups by subject
        Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject);

        // Allows a user to join a study group
        Task JoinStudyGroup(int studyGroupId, int userId);

        // Allows a user to leave a study group
        Task LeaveStudyGroup(int studyGroupId, int userId);

        // Retrieves study groups with users whose names start with 'M'
        Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithM();

        // Retrieves study groups with users whose names start with 'M' from an in-memory database
        Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithMInMemoryDataBase();

        // Retrieves filtered and sorted study groups based on parameters
        void GetStudyGroupsFilteredAndSorted(string filter, bool sortByAscending);

        // Responsable to verify if a user already in studygroup
        Task<bool> UserAlreadyHasGroupForSubject(int userId, Subject subject);
        Task<IEnumerable<StudyGroup>> GetStudyGroupsSortedByCreationDate(bool descending);


        // Add other methods here if necessary
    }
}
