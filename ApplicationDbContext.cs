using Microsoft.EntityFrameworkCore;

namespace Sistema_de_gestión_de_productos_.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entidad =>
            {
                entidad.ToTable("Productos");
                entidad.HasKey(p => p.Id);

                entidad.Property(p => p.Nombre)
                    .IsRequired();

                entidad.Property(p => p.Descripcion)
                    .IsRequired();

                entidad.Property(p => p.Cantidad)
                    .IsRequired();

                entidad.Property(p => p.Categoria)
                    .IsRequired();

                entidad.Property(p => p.Precio)
                    .IsRequired();
            });
        }
    }
}
