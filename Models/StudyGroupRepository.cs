using StudyGroupsManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateStudyGroup(StudyGroup studyGroup)
        {
            _context.StudyGroups.Add(studyGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudyGroup>> GetStudyGroups()
        {
            return await _context.StudyGroups.ToListAsync();
        }

        public async Task<IEnumerable<StudyGroup>> SearchStudyGroups(string subject)
        {
            return await _context.StudyGroups
                .Where(sg => sg.Subject.ToString().Equals(subject))
                .ToListAsync();
        }

        public async Task JoinStudyGroup(int studyGroupId, int userId)
        {
            // Esta é uma implementação de exemplo. Você precisará ajustar conforme a lógica do seu aplicativo.
            var user = await _context.Users.FindAsync(userId);
            var studyGroup = await _context.StudyGroups.FindAsync(studyGroupId);
            if (user != null && studyGroup != null)
            {
                // Supondo que você tenha uma lista de usuários em StudyGroup
                studyGroup.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task LeaveStudyGroup(int studyGroupId, int userId)
        {
            // Implementação similar ao JoinStudyGroup, mas removendo o usuário do grupo
            var user = await _context.Users.FindAsync(userId);
            var studyGroup = await _context.StudyGroups.FindAsync(studyGroupId);
            if (user != null && studyGroup != null && studyGroup.Users.Contains(user))
            {
                studyGroup.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public void GetStudyGroupsFilteredAndSorted(string subject, bool sortByCreationDateDescending)
        {
            // Implementação de exemplo. Ajuste conforme necessário.
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

            // Como este método é void, você pode querer ajustá-lo para retornar uma lista ou modificar a interface
            var result = query.ToList(); // Isso requer ajuste na interface ou no uso deste método.
        }

        public async Task<IEnumerable<StudyGroup>> GetStudyGroupsWithUserStartingWithM()
        {
            return await _context.StudyGroups
                .Include(sg => sg.Users) // Garante que os usuários sejam carregados
                .Where(sg => sg.Users.Any(u => u.Name.StartsWith("M")))
                .ToListAsync();
        }
    }
}
