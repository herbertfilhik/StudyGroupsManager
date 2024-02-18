
using System.ComponentModel.DataAnnotations;

namespace StudyGroupsManager.Models
{
    public class StudyGroup
    {
        private const int MinNameLength = 5;
        private const int MaxNameLength = 30;

        // Construtor sem parâmetros necessário para o Entity Framework Core
        public StudyGroup()
        {
            Users = new List<User>();
        }

        public StudyGroup(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                throw new ArgumentException($"O nome do grupo deve ter entre {MinNameLength} e {MaxNameLength} caracteres.");
            }
                        
            // Verify if the subject is valid
            if (!Enum.IsDefined(typeof(Subject), subject))
            {
                throw new ArgumentException("Assunto inválido.");
            }

            StudyGroupId = studyGroupId;
            Name = name;
            Subject = subject;
            CreateDate = createDate;
            Users = users ?? new List<User>();
        }

        //Some logic will be missing to validate values according to acceptance criteria, but imagine it is existing or do it yourself

        [Key] // Define esta propriedade como chave primária
        public int StudyGroupId { get; set; }

        [StringLength(MaxNameLength, MinimumLength = MinNameLength, ErrorMessage = "O nome do grupo deve ter entre {2} e {1} caracteres.")]
        public string? Name { get; private set; }

        public Subject Subject { get; }

        public DateTime CreateDate { get; set; }

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
