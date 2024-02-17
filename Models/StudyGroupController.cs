using Microsoft.AspNetCore.Mvc;

namespace StudyGroupsManager.Models
{
    public class StudyGroupController
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        public async Task<IActionResult> CreateStudyGroup(StudyGroup studyGroup)
        {
            try
            {
                await _studyGroupRepository.CreateStudyGroup(studyGroup);
                return new OkResult();
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }


        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        public async Task<IActionResult> GetFilteredAndSortedStudyGroups(string subject, bool sortByCreationDateDescending)
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();

            if (!string.IsNullOrEmpty(subject))
            {
                studyGroups = studyGroups.Where(sg => sg.Subject.ToString().Equals(subject, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (sortByCreationDateDescending)
            {
                studyGroups = studyGroups.OrderByDescending(sg => sg.CreateDate).ToList();
            }
            else
            {
                studyGroups = studyGroups.OrderBy(sg => sg.CreateDate).ToList();
            }

            return new OkObjectResult(studyGroups);
        }
    }
}