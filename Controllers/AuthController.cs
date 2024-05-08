using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.Interfaces;
using Sistema_de_gestión_de_productos_.Services;
using System;
using System.Threading.Tasks;

namespace Sistema_de_gestión_de_productos_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login", Name = "LoginUser")]
        public async Task<ActionResult<AuthenticationResult>> Login(string password, string email)
        {
            try
            {
                var model = new LoginRequest { 
                    Email = email,
                    Password = password
                };
                var result = await _authService.Login(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("token", Name = "token")]
        public ActionResult<AuthService> GetTokenDetails()
        {
            // Obtener el token de la cabecera de autorización
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token not provided");
            }

            // Obtener los detalles del token utilizando el servicio TokenService
            var tokenDetails = _authService.GetTokenDetails(token);

            if (tokenDetails == null)
            {
                return BadRequest("Invalid token");
            }

            return Ok(tokenDetails);
        }
    }
}
