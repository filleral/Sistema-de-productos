using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Identity.Client;
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.Services;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IAuth
    {
        public Task<AuthenticationResult> Login(LoginRequest model);
        public TokenDetails GetTokenDetails(string token);
    }
}
