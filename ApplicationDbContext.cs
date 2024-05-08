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
        public DbSet<User> User { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> Useroles { get; set; }


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
            modelBuilder.Entity<User>(entidad =>
            {
                entidad.ToTable("User");
                entidad.HasKey(p => p.Id);

                entidad.Property(p => p.Username)
                    .IsRequired();

                entidad.Property(p => p.PasswordHash)
                    .IsRequired();

                entidad.Property(p => p.Salt)
                    .IsRequired();

                entidad.Property(p => p.Email)
                    .IsRequired();
            });

            modelBuilder.Entity<Roles>(entidad =>
            {
                entidad.ToTable("roles");
                entidad.HasKey(p => p.Id);

                entidad.Property(p => p.rolname)
                    .IsRequired();

            });
            modelBuilder.Entity<UserRoles>(entidad =>
            {
                entidad.ToTable("UserRoles");
                entidad.HasKey(p => p.Id); // Definir una clave primaria compuesta

                entidad.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.userId); // Definir la clave externa a User

                entidad.HasOne(ur => ur.Roles)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId); // Definir la clave externa a Roles
            });

        }
    }
}
