using Sistema_de_gestión_de_productos_.Entities;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IUser
    {
        public Task CreateUser(string username, string password, string email);
        public Task<List<UserDto>> GetUserByUsername(string name);
        public bool VerifyPassword(UserDto user, string password);
        public Task<RolesDto> GetRolById(int id);
        public Task CreateRol(int Rolid, int userid)
    }
}
