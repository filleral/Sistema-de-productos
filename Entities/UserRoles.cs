using System.ComponentModel.DataAnnotations;

namespace Sistema_de_gestión_de_productos_.Entities
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public Roles Roles { get; set; }
    }
}
