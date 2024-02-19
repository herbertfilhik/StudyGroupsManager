using StudyGroupsManager.Models; // Namespace for study group models
using Microsoft.EntityFrameworkCore; // Namespace for Entity Framework Core
using StudyGroupsManager.Context;
using StudyGroupsManager.DTOs;

namespace StudyGroupsManager.Repositories
{
    public class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly AppDbContext _context; // Application DbContext

        public StudyGroupRepository(AppDbContext context)
        {
            _context = context; // Initializing DbContext
        }

        // Method to create a study group
        public async Task<bool> CreateStudyGroup(StudyGroupCreationDto studyGroupDto)
        {
            // Create a new instance of StudyGroup based on data received from the DTO
            var newStudyGroup = new StudyGroup
            {
                Name = studyGroupDto.Name,
                Subject = studyGroupDto.Subject, // Directly use the value of the enum, which is already guaranteed to be valid
                CreateDate = DateTime.Now, // Set the creation date to now
                Users = new List<User>() // Initialize the list of users, if applicable
            };

            // Add the new study group to the Entity Framework context
            _context.StudyGroups.Add(newStudyGroup);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return true; // Return true to indicate success
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

        // Method to verify if user already has a group for subject
        public async Task<bool> UserAlreadyHasGroupForSubject(int userId, Subject subject)
        {
            return await _context.StudyGroups.AnyAsync(sg => sg.Users.Any(u => u.Id == userId) && sg.Subject == subject);
        }

        public async Task<IEnumerable<StudyGroup>> GetStudyGroupsSortedByCreationDate(bool descending)
        {
            if (descending)
            {
                return await _context.StudyGroups.OrderByDescending(sg => sg.CreateDate).ToListAsync();
            }
            else
            {
                return await _context.StudyGroups.OrderBy(sg => sg.CreateDate).ToListAsync();
            }
        }

        public async Task<bool> UserIsMemberOfStudyGroupForSubject(int userId, Subject subject)
        {
            return await _context.StudyGroups
                .AnyAsync(sg => sg.Users.Any(u => u.Id == userId) && sg.Subject == subject);
        }

        public async Task<StudyGroup> GetStudyGroupById(int studyGroupId)
        {
            return await _context.StudyGroups
                                 .Include(sg => sg.Users) // Optional, depending on whether you want to load the group's users
                                 .FirstOrDefaultAsync(sg => sg.StudyGroupId == studyGroupId);
        }

        public async Task<User> GetUserById(int userId)
        {
            // Implement logic to fetch a user by ID.
            // Example:
            return await _context.Users.FindAsync(userId);
        }

        // Implementation of method as defined in interface
        public async Task CreateStudyGroup(StudyGroup studyGroup)
        {
            _context.StudyGroups.Add(studyGroup);
            await _context.SaveChangesAsync();
        }

        // Implementation of newly added method
        public async Task<bool> IsUserMemberOfStudyGroup(int userId, int studyGroupId)
        {
            // Check if there exists any study group with the specified ID that contains a user with the specified ID
            return await _context.StudyGroups
                .AnyAsync(sg => sg.StudyGroupId == studyGroupId && sg.Users.Any(u => u.Id == userId));
        }
    }
}
