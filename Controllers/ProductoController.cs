using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.Interfaces;
using System;
using System.Data;
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
        public async Task<ActionResult<Producto[]>> Create(string nombre, string Descripción, string Categoria, decimal Precio, string Cantidad)
        {
            try
            {
                // Llama a la función CreateProducto y espera su ejecución
                await _producto.CreateProducto(nombre, Descripción, Categoria, Precio, Cantidad);

                // Si la función CreateProducto no lanza una excepción, devuelve un OkResult
                return Ok("Producto Creado Correctamente");
            }
            catch (Exception ex)
            {
                // Si la función CreateProducto lanza una excepción, devuelve un StatusCode con un mensaje de error
                return StatusCode(500, $"Internal server error no se puedo crear el producto: {ex.Message}");
            }
        }

        [HttpPost("edit", Name = "EditProducto")]
        public async Task<ActionResult<Producto>> Edit(int id, string? nombre, string? descripcion, string? categoria, decimal? precio, string? cantidad)
        {
            try
            {
                // Obtén el producto que deseas editar
                var producto = await _producto.GetProductById(id);

                // Si el producto no se encuentra, devuelve un NotFoundResult
                if (producto == null)
                {
                    return NotFound();
                }

                // Actualiza las propiedades del producto con los nuevos valores, pero solo si no son nulos
                if (nombre != null) producto.Nombre = nombre;
                if (descripcion != null) producto.Descripcion = descripcion;
                if (categoria != null) producto.Categoria = categoria;
                if (precio != null) producto.Precio = precio.Value;
                if (cantidad != null) producto.Cantidad = cantidad;

                // Guarda los cambios en la base de datos
                await _producto.UpdateProducto(id, producto.Nombre, producto.Descripcion, producto.Categoria, producto.Precio, producto.Cantidad);

                // Devuelve el producto actualizado
                return Ok(producto);
            }
            catch (Exception ex)
            {
                // Si se produce un error, devuelve un StatusCode con un mensaje de error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("getall", Name = "getallProducto")]
        public async Task<ActionResult> get()
        {
            try
            {
                var Productos = await _producto.GetAllProducts();
                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyname", Name = "getaProductobyname")]
        public async Task<ActionResult> getprodctbyname(string nombre)
        {
            try
            {
                var Productos = await _producto.GetProductsbyname(nombre);
                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("delete", Name = "delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                var Productos = await _producto.Delete(id);
                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyprice", Name = "getaProductobyprice")]
        public async Task<ActionResult> getprodctbyprice(decimal minPrice, decimal maxPrice)
        {
            try
            {
                var Productos = await _producto.GetProductsbyprice(minPrice, maxPrice);
                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
