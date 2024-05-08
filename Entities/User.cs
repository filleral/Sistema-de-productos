using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Sistema_de_gestión_de_productos_.Entities
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        // La contraseña se almacenará en forma de hash
        public byte[] PasswordHash { get; set; }

        // La salt se utiliza para aumentar la seguridad del hash
        public byte[] Salt { get; set; }

        [Required]
        public string Email { get; set; }
        public List<UserRolesDto> UserRoles { get; set; }

        // Método para establecer la contraseña
        public void SetPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                Salt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // Método para verificar la contraseña
        public bool VerifyPassword(string password)
        {
            using (var hmac = new HMACSHA512(Salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PasswordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}
