using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_gestión_de_productos_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _producto;

        public ProductoController(IProducto Producto)
        {
            _producto = Producto;
        }

        [HttpPost(Name = "CreateProducto")]
        public async Task<ActionResult<Producto[]>> Create()
        {
            try
            {
                var CreateProducto = 
                return Ok(CreateProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
