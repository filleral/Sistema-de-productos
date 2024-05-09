using Microsoft.AspNetCore.Mvc;
using Sistema_de_gestión_de_productos_.Entities;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IProducto
    {
        public Task<ActionResult<Producto>> UpdateProducto(int id, string nombre, string descripcion, string categoria, decimal precio, string cantidad);
        public Task<Producto> GetProductById(int id);
        public Task<List<Producto>> GetAllProducts();
        public Task CreateProducto(string nombre, string descripcion, string categoria, decimal precio, string cantidad);
        public Task<List<Producto>> GetProductsbyname(string nombre);
        public Task<List<Producto>> GetProductsbyprice(decimal minPrice, decimal maxPrice);
        public Task<ActionResult> Delete(int id);
    }
}
