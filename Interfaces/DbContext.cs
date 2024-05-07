using System.Collections.Generic;
using System.Data.Entity;

namespace Sistema_de_gestión_de_productos_.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Producto> Producto { get; set; }
    }
}