using System.ComponentModel.DataAnnotations;

namespace Sistema_de_gestión_de_productos_.Entities
{
    public class UserRolesDto
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int userId { get; set; }
        public UserDto User { get; set; }
        public RolesDto Roles { get; set; }
    }
}
