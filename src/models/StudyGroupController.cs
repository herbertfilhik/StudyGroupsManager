﻿using Microsoft.AspNetCore.Mvc;
using StudyGroupsManager.src.DTOs;
using StudyGroupsManager.src.Repositories;

namespace StudyGroupsManager.src.Models
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
            // Length name validation
            if (string.IsNullOrWhiteSpace(studyGroupDto.Name) || studyGroupDto.Name.Length < 5 || studyGroupDto.Name.Length > 30)
            {
                return BadRequest("The name of the group must be between 5 and 30 characters.");
            }

            // Subject validation
            if (!Enum.IsDefined(typeof(Subject), studyGroupDto.Subject))
            {
                return BadRequest("Invalid Subject");
            }

            try
            {
                // Verify if User already has Group for subject
                bool userHasGroupForSubject = await _studyGroupRepository.UserAlreadyHasGroupForSubject(studyGroupDto.UserId, studyGroupDto.Subject);
                if (userHasGroupForSubject)
                {
                    return BadRequest("The user already has a study group for this subject.");
                }

                // Create new StudyGroup object from DTO
                var newStudyGroup = new StudyGroup
                {

                    Name = studyGroupDto.Name,
                    Subject = studyGroupDto.Subject,
                    CreateDate = DateTime.UtcNow,
                    // Initialize the user list as empty, or add the user if applicable
                    Users = new List<User>()
                };

                // Try create StudyGroup
                await _studyGroupRepository.CreateStudyGroup(newStudyGroup);
                return Ok(); // Retorna sucesso
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            var studyGroup = await _studyGroupRepository.GetStudyGroupById(studyGroupId);
            if (studyGroup == null)
            {
                return NotFound("Study group not found.");
            }

            var userAlreadyMember = await _studyGroupRepository.UserIsMemberOfStudyGroupForSubject(userId, studyGroup.Subject);
            if (userAlreadyMember)
            {
                return BadRequest("User is already a member of a study group for this subject.");
            }

            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return Ok();
        }


        // Action method to leave a study group
        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            //  First, check if the user is a member of the study group
            bool isMember = await _studyGroupRepository.IsUserMemberOfStudyGroup(userId, studyGroupId);
            if (!isMember)
            {
                // If not member, return BadRequest
                return BadRequest($"The user with ID {userId} is not a member of the study group with ID {studyGroupId}.");
            }

            // If member, procede witj logic to remove user from Study Group
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return Ok();
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

        // Methody to obtain Study Groups ordering by creation date
        public async Task<IActionResult> GetStudyGroupsSortedByCreationDate(bool descending)
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroupsSortedByCreationDate(descending);
            return Ok(studyGroups);
        }
    }
}
