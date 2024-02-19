using StudyGroupsManager.Models;
using Microsoft.EntityFrameworkCore;
using StudyGroupsManager.Context;

namespace StudyGroupsManager.Data
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly AppDbContext _context;

        public StudyGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        // Method to create a study group
        public async Task CreateStudyGroup(StudyGroup studyGroup)
        {
            _context.StudyGroups.Add(studyGroup);
            await _context.SaveChangesAsync();
        }

        // Method to get all study groups
        public async Task<IEnumerable<StudyGroup>> GetStudyGroups()
        {
            return await _context.StudyGroups.ToListAsync();
        }

        // Method to search for study groups by subject
        public async Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject)
        {
            return await _context.StudyGroups
                .Where(sg => sg.Subject.ToString().Equals(subject))
                .ToListAsync();
        }

        // Method to join a study group
        public async Task JoinStudyGroup(int studyGroupId, int userId)
        {
            // This is a sample implementation. You will need to adjust it according to your application logic.
            var user = await _context.Users.FindAsync(userId);
            var studyGroup = await _context.StudyGroups.FindAsync(studyGroupId);
            if (user != null && studyGroup != null)
            {
                // Assuming you have a list of users in StudyGroup
                studyGroup.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        // Method to leave a study group
        public async Task LeaveStudyGroup(int studyGroupId, int userId)
        {
            // Similar implementation to JoinStudyGroup, but removing the user from the group
            var user = await _context.Users.FindAsync(userId);
            var studyGroup = await _context.StudyGroups.FindAsync(studyGroupId);
            if (user != null && studyGroup != null && studyGroup.Users.Contains(user))
            {
                studyGroup.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Method to get study groups filtered and sorted
        public void GetStudyGroupsFilteredAndSorted(string subject, bool sortByCreationDateDescending)
        {
            // Sample implementation. Adjust as necessary.
            var query = _context.StudyGroups.AsQueryable();

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(sg => sg.Subject.ToString().Equals(subject));
            }

            if (sortByCreationDateDescending)
            {
                query = query.OrderByDescending(sg => sg.CreateDate);
            }
            else
            {
                query = query.OrderBy(sg => sg.CreateDate);
            }

            // Since this method is void, you may want to adjust it to return a list or modify the interface
            var result = query.ToList(); // This requires adjustment in the interface or usage of this method.
        }

        // Method to get study groups with users whose names start with 'M'
        public async Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithM()
        {
            return await _context.StudyGroups
                .Include(sg => sg.Users) // Ensure users are loaded
                .Where(sg => sg.Users.Any(u => u.Name.StartsWith("M")))
                .ToListAsync();
        }

        // Method to get study groups with users whose names start with 'M' in an in-memory database
        public async Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithMInMemoryDataBase()
        {
            // Raw SQL
            var query = @"
            SELECT DISTINCT sg.* 
            FROM StudyGroups sg
            JOIN Users u ON sg.StudyGroupId = u.StudyGroupId
            WHERE u.Name LIKE 'M%'
            ORDER BY sg.CreateDate";

            // Execute the raw SQL query
            var studyGroups = await _context.StudyGroups
                .FromSqlRaw(query)
                .Include(sg => sg.Users) // May need adjustment or removal depending on your provider support
                .ToListAsync();

            return studyGroups;
        }

    }
}
