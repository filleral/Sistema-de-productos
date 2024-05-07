using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Entities;
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
        [HttpPost("create", Name = "CreateProducto")]
        public async Task<ActionResult<Producto[]>> Create(string nombre, string Descripción, string Categoria, string Precio, string Cantidad)
        {
            try
            {
                var CreateProducto = _producto.CreateProducto(nombre, Descripción, Categoria, Precio, Cantidad);
                return Ok(CreateProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("edit", Name = "EditProducto")]
        public async Task<ActionResult<Producto[]>> Edit(string nombre, string Descripción, string Categoria, string Precio, string Cantidad)
        {
            try
            {
                var CreateProducto = _producto.CreateProducto(nombre, Descripción, Categoria, Precio, Cantidad);
                return Ok(CreateProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getall", Name = "getallProducto")]
        public async Task<ActionResult<Producto[]>> get()
        {
            try
            {
                var CreateProducto = _producto.GetAllProducts();
                return Ok(CreateProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
