using Sistema_de_gestión_de_productos_.Entities;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IProducto
    {
        public Task<List<Producto>> GetAllProducts();
        public Task CreateProducto(string nombre, string descripcion, string categoria, string precio, string cantidad);
    }
}
