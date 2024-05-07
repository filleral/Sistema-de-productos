using Sistema_de_gesti�n_de_productos_.Interfaces;
using Sistema_de_gesti�n_de_productos_.Entities;
using Microsoft.EntityFrameworkCore;
using Sistema_de_gesti�n_de_productos_.Services;

namespace Sistema_de_gesti�n_de_productos_
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Esta propiedad proporciona acceso a la configuraci�n de la aplicaci�n
        public IConfiguration Configuration { get; }

        // Este m�todo se utiliza para agregar servicios al contenedor de inyecci�n de dependencias
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("Context")));
            services.AddTransient<IProducto, ProductService>();
            services.AddControllersWithViews();
            services.AddSwaggerGen();
        }

        // Este m�todo se utiliza para configurar el middleware de la aplicaci�n HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuraci�n de middleware para el entorno de desarrollo
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
