using Sistema_de_gestión_de_productos_.Entities;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IUser
    {
        public Task CreateUser(string username, string password, string email);
        public Task<List<User>> GetUserByUsername(string name);
        public bool VerifyPassword(User user, string password);
        public Task<Roles> GetRolById(int id);
    }
}
