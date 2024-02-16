using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudyGroupsManager.Models
{
    public class StudyGroup
    {
        private const int MinNameLength = 5;
        private const int MaxNameLength = 30;

        public StudyGroup(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                throw new ArgumentException($"O nome do grupo deve ter entre {MinNameLength} e {MaxNameLength} caracteres.");
            }

            // Verifique se o assunto é válido, se necessário
            if (!Enum.IsDefined(typeof(Subject), subject))
            {
                throw new ArgumentException("Assunto inválido.");
            }

            StudyGroupId = studyGroupId;
            Name = name;
            Subject = subject;
            CreateDate = createDate;
            Users = users ?? new List<User>(); // Adiciona um null-check para evitar NullReferenceException

        }
        //Some logic will be missing to validate values according to acceptance criteria, but imagine it is existing or do it yourself
        public int StudyGroupId { get; }

        public string Name { get; }

        public Subject Subject { get; }

        public DateTime CreateDate { get; }

        public List<User> Users { get; private set; }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
    }

    public enum Subject
    {
        Math,
        Chemistry,
        Physics
    }
}
