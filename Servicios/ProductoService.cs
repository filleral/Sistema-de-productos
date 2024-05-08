using Sistema_de_gestión_de_productos_.Interfaces;
using Microsoft.EntityFrameworkCore; 
using Sistema_de_gestión_de_productos_.Entities;
using Sistema_de_gestión_de_productos_.dto.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Sistema_de_gestión_de_productos_.Services
{
    public class ProductService : IProducto
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Producto>> GetProductsbyprice(decimal minPrice, decimal maxPrice)
        {
            try
            {
                // Filtra los productos por rango de precio
                var products = await _dbContext.Productos
                                                .Where(p => p.Precio >= minPrice && p.Precio <= maxPrice)
                                                .ToListAsync();
                // Aquí conviertes tus objetos Producto a ProductoDto si es necesario
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos por rango de precio: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Producto>> GetAllProducts()
        {
            try
            {
                var products = await _dbContext.Productos.ToListAsync();
                // Aquí conviertes tus objetos Producto a ProductoDto si es necesario
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Producto>> GetProductsbyname(string nombre)
        {
            try
            {
                // Filtra los productos por nombre
                var products = await _dbContext.Productos
                                                .Where(p => p.Nombre.Contains(nombre))
                                                .ToListAsync();
                if (products == null)
                {
                    return null;
                }
                // Aquí conviertes tus objetos Producto a ProductoDto si es necesario
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener productos por nombre: {ex.Message}");
                throw;
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // Busca el producto por su ID en la base de datos
                var product = await _dbContext.Productos.FindAsync(id);

                // Si el producto no se encuentra, devuelve un NotFoundResult
                if (product == null)
                {
                    return null;
                }

                // Elimina el producto de la base de datos
                _dbContext.Productos.Remove(product);
                await _dbContext.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar el producto: {ex.Message}");
                throw;
            }
        }

        public async Task CreateProducto(string nombre, string descripcion, string categoria, decimal precio, string cantidad)
        {
            try
            {
                var nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Categoria = categoria,
                    Precio = precio,
                    Cantidad = cantidad
                };

                _dbContext.Productos.Add(nuevoProducto);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Producto> GetProductById(int id)
        {
            try
            {
                // Busca el producto por su ID en la base de datos
                var product = await _dbContext.Productos.FindAsync(id);

                // Si el producto no se encuentra, devuelve null
                if (product == null)
                {
                    return null;
                }

                // Devuelve el producto encontrado
                return product;
            }
            catch (Exception ex)
            {
                // Maneja el error adecuadamente, como registrar el error o lanzarlo nuevamente
                Console.WriteLine($"Error al obtener el producto por ID: {ex.Message}");
                throw;
            }
        }
        public async Task<ActionResult<Producto>> UpdateProducto(int id, string nombre, string descripcion, string categoria, decimal precio, string cantidad)
        {
            try
            {
                // Busca el producto por su ID en la base de datos
                var product = await _dbContext.Productos.FindAsync(id);

                // Si el producto no se encuentra, devuelve un mensaje de error
                if (product == null)
                {
                    return new NotFoundObjectResult($"Producto con ID {id} no encontrado");
                }

                // Actualiza las propiedades del producto
                product.Nombre = nombre;
                product.Descripcion = descripcion;
                product.Categoria = categoria;
                product.Precio = precio;
                product.Cantidad = cantidad;

                // Guarda los cambios en la base de datos
                await _dbContext.SaveChangesAsync();

                // Devuelve el producto actualizado
                return product;
            }
            catch (Exception ex)
            {
                // Maneja el error adecuadamente, como registrar el error o lanzarlo nuevamente
                Console.WriteLine($"Error al actualizar el producto: {ex.Message}");
                throw;
            }
        }


    }
}
