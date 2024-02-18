using System.ComponentModel.DataAnnotations;

namespace StudyGroupsManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Adicione o atributo StudyGroupId para associar usuários a grupos de estudo
        public int StudyGroupId { get; set; }

        // Se necessário, você também pode adicionar uma propriedade de navegação para representar o relacionamento com o grupo de estudo
        // public StudyGroup StudyGroup { get; set; }
    }
}
