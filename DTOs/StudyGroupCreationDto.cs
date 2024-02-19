using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGroupsManager.Models;

namespace StudyGroupsManager.DTOs
{
    public class StudyGroupCreationDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
    }

}
