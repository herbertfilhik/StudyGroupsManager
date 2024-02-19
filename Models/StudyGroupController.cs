using Microsoft.AspNetCore.Mvc;

namespace StudyGroupsManager.Models
{
    // Controller responsible for managing study groups
    public class StudyGroupController : ControllerBase
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        // Constructor injection for study group repository
        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        // Action method to create a study group
        public async Task<IActionResult> CreateStudyGroup(StudyGroupCreationDto studyGroupDto)
        {
            try
            {
                // Verifica se o usuário já possui um grupo para o assunto
                bool userHasGroupForSubject = await _studyGroupRepository.UserAlreadyHasGroupForSubject(studyGroupDto.UserId, studyGroupDto.Subject);
                if (userHasGroupForSubject)
                {
                    return BadRequest("O usuário já possui um grupo de estudo para esse assunto.");
                }

                // Cria um novo objeto StudyGroup a partir do DTO
                var newStudyGroup = new StudyGroupCreationDto
                {
                    // Atribua os valores do DTO para o novo objeto StudyGroup aqui
                    Name = studyGroupDto.Name,
                    Subject = studyGroupDto.Subject,
                    // Defina os outros campos conforme necessário
                };

                // Tenta criar o grupo de estudo
                await _studyGroupRepository.CreateStudyGroup(newStudyGroup);
                return Ok(); // Retorna sucesso
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Retorna erro se a criação falhar
            }
        }    

        // Action method to get all study groups
        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }

        // Action method to search for study groups by subject
        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }

        // Action method to join a study group
        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        // Action method to leave a study group
        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        // Action method to get filtered and sorted study groups
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

        // Action method to get study groups with users whose names start with 'M'
        public async Task<IActionResult> GetStudyGroupsWithUserStartingWithM()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroupsWithUserStartingWithM();
            return Ok(studyGroups);
        }

        // Action method to get study groups with users whose names start with 'M' from an in-memory database
        public async Task<IActionResult> GetStudyGroupsWithUserStartingWithMInMemoryDataBase()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroupsWithUserStartingWithMInMemoryDataBase();
            return Ok(studyGroups);
        }
    }
}
