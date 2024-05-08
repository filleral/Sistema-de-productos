using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_de_gestión_de_productos_.Entities;
using Microsoft.EntityFrameworkCore;
using Sistema_de_gestión_de_productos_.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Sistema_de_gestión_de_productos_.Services
{
    public class UserService :IUser
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> GetUserByUsername(string name)
        {
            try
            {
                // Filtra los productos por nombre
                var products = await _dbContext.User
                                                .Include(u => u.UserRoles)
                                                .Where(p => p.Email.Contains(name))
                                                .ToListAsync();
                if (products == null)
                {
                    return null;
                }
                // Aquí conviertes tus objetos Producto a ProductoDto si es necesario
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos por nombre: {ex.Message}");
                throw;
            }

        }
        public async Task<Roles> GetRolById(int id)
        {
            try
            {
                // Filtra los roles por su ID
                var role = await _dbContext.Roles
                                            .FirstOrDefaultAsync(r => r.Id == id);
                return role;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el rol por ID: {ex.Message}");
                throw;
            }
        }

        public bool VerifyPassword(User user, string password)
        {
            // Computa el hash de la contraseña proporcionada utilizando la salt del usuario
            using (var hmac = new HMACSHA512(user.Salt))
            {
                // Genera el hash de la contraseña proporcionada
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Compara el hash generado con el hash almacenado en el usuario
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                        return false;
                }

                // Si los hashes coinciden, la contraseña es válida
                return true;
            }
        }
        public async Task CreateUser(string username, string password, string email)
        {
            try
            {
                // Verifica si el nombre de usuario ya existe
                if (await _dbContext.User.AnyAsync(u => u.Username == username))
                {
                    throw new ArgumentException("El nombre de usuario ya está en uso.");
                }

                // Crea una nueva instancia de User
                var user = new User
                {
                    Username = username,
                    Email = email
                };

                // Establece la contraseña del usuario
                user.SetPassword(password);

                // Agrega el nuevo usuario al contexto de la base de datos
                _dbContext.User.Add(user);

                // Guarda los cambios en la base de datos
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el usuario.", ex);
            }
        }
    }
    public class TokenDetails
    {
        public List<KeyValuePair<string, string>> Claims { get; set; }
    }
}
