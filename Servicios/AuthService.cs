using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Sistema_de_gestión_de_productos_;
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.Interfaces;
using Sistema_de_gestión_de_productos_.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class AuthService : IAuth
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Sistema_de_gestión_de_productos_.Interfaces.IUser _userService;

    public AuthService(ApplicationDbContext dbContext, Sistema_de_gestión_de_productos_.Interfaces.IUser userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }
    public static string GenerateJwtSecretKey()
    {
        const int keySizeInBits = 256;
        byte[] keyBytes = new byte[keySizeInBits / 8];

        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(keyBytes);
        }

        // Convierte los bytes de la clave en una cadena base64 para su almacenamiento
        return Convert.ToBase64String(keyBytes);
    }
    public TokenDetails GetTokenDetails(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claims = jwtToken.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)).ToList();

        return new TokenDetails
        {
            Claims = claims
        };
    }

    public async Task<AuthenticationResult> Login(LoginRequest model)
    {
        // Verificar si el usuario existe en la base de datos
        var users = await _userService.GetUserByUsername(model.Email);
        var user = users.FirstOrDefault(); // Obtener el primer usuario de la lista
        var rolid = user.UserRoles.Select(ur => ur.RoleId).FirstOrDefault();

        if (user == null)
        {
            return new AuthenticationResult(false, null, "Usuario no encontrado.");
        }

        // Verificar si la contraseña es correcta para el primer usuario encontrado
        var isPasswordValid = user.VerifyPassword(model.Password);
        if (!isPasswordValid)
        {
            return new AuthenticationResult(false, null, "CONTRASEÑA INCORRECTA");
        }

        // Generar el token JWT
        var _jwtSecret = GenerateJwtSecretKey();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        var rolname = await _userService.GetRolById(rolid);
        if (user != null && rolname != null)
        {
            claims.Add(new Claim(ClaimTypes.Role, rolname.rolname));
        }

        var identity = new ClaimsIdentity(claims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);


        // Devolver el token JWT en el resultado de autenticación
        return new AuthenticationResult(true, tokenString, null);
    }
}
