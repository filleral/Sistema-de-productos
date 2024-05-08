using System.ComponentModel.DataAnnotations;

namespace Sistema_de_gestión_de_productos_.Entities
{
    public class Roles
    {
        public int Id { get; set; }

        [Required]
        public string rolname { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
}
