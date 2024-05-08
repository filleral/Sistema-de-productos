using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.Interfaces;

namespace Sistema_de_gestión_de_productos_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser User)
        {
            _user = User;
        }
        [HttpPost("create", Name = "CreateUser")]
        public async Task<ActionResult<Producto[]>> Create(string username, string password, string email)
        {
            try
            {
                // Llama a la función CreateProducto y espera su ejecución
                await _user.CreateUser(username, password, email);

                // Si la función CreateProducto no lanza una excepción, devuelve un OkResult
                return Ok("Usuario Creado Correctamente");
            }
            catch (Exception ex)
            {
                // Si la función CreateProducto lanza una excepción, devuelve un StatusCode con un mensaje de error
                return StatusCode(500, $"Internal server error no se puedo crear el producto: {ex.Message}");
            }
        }
        [HttpPost("Createrol", Name = "Createrol")]
        public async Task<ActionResult<UserRolesDto[]>> Createrol(int Rolid, int userid)
        {
            try
            {
                // Llama a la función CreateProducto y espera su ejecución
                await _user.CreateRol(Rolid, userid);

                // Si la función CreateProducto no lanza una excepción, devuelve un OkResult
                return Ok("rol Creado Correctamente");
            }
            catch (Exception ex)
            {
                // Si la función CreateProducto lanza una excepción, devuelve un StatusCode con un mensaje de error
                return StatusCode(500, $"Internal server error no se puedo crear el producto: {ex.Message}");
            }
        }


    }
}
