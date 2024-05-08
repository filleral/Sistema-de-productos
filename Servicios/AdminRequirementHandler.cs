using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sistema_de_gestión_de_productos_.Services;
using TuProyecto.Authorization;
using YamlDotNet.Core.Tokens;

public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminRequirementHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Fail(); // No se proporcionó un token en el encabezado de autorización
            return Task.CompletedTask;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

        // Verificar si el usuario tiene el rol de administrador en los claims del token
        if (claims.ContainsKey("role") && claims["role"] == "Admin")
        {
            context.Succeed(requirement); // El usuario tiene el rol de administrador
            return Task.CompletedTask;
        }

        context.Fail(); // El usuario no tiene el rol de administrador
        return Task.CompletedTask;
    }
}
