using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyGroupsManager.Models;

namespace StudyGroupsManager.DTOs
{
    public class StudyGroupJoinDto
    {
        public int UserId { get; set; }
        public Subject Subject { get; set; }
    }
}
