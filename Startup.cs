using Sistema_de_gestión_de_productos_.Interfaces;
using Sistema_de_gestión_de_productos_.Entities;
using Microsoft.EntityFrameworkCore;
using Sistema_de_gestión_de_productos_.Services;

namespace Sistema_de_gestión_de_productos_
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Esta propiedad proporciona acceso a la configuración de la aplicación
        public IConfiguration Configuration { get; }

        // Este método se utiliza para agregar servicios al contenedor de inyección de dependencias
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("Context")));
            services.AddTransient<IProducto, ProductService>();
            services.AddControllersWithViews();
            services.AddSwaggerGen();
        }

        // Este método se utiliza para configurar el middleware de la aplicación HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuración de middleware para el entorno de desarrollo
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
