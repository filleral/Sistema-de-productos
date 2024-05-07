using Sistema_de_gestión_de_productos_.Interfaces;
using Microsoft.EntityFrameworkCore; 
using Sistema_de_gestión_de_productos_.Entities;

namespace Sistema_de_gestión_de_productos_.Services
{
    public class ProductService : IProducto
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProducto(string nombre, string descripcion, string categoria, string precio, string cantidad)
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
        public async Task<List<Producto>> GetAllProducts()
        {
            try
            {
                var products = await _dbContext.Productos.ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
