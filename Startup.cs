using Sistema_de_gestión_de_productos_.Interfaces;
using Sistema_de_gestión_de_productos_.Entities;
using Microsoft.EntityFrameworkCore;
using Sistema_de_gestión_de_productos_.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag;
using Swashbuckle.AspNetCore.SwaggerGen;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authorization;
using TuProyecto.Authorization;


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
            try
            {

                // En el método ConfigureServices de Startup.cs
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminPolicy", policy =>
                    {
                        policy.Requirements.Add(new AdminRequirement());
                    });
                });
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();


                // Otros servicios
                services.AddControllers();
                services.AddScoped<IUser, UserService>();
                services.AddScoped<IAuth, AuthService>();
                services.AddScoped<ApplicationDbContext>();

                services.AddAuthorization();

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Context")));

                services.AddTransient<IProducto, ProductService>();
                services.AddControllersWithViews();
                services.AddSwaggerGen();
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente, como registrarla o lanzarla nuevamente
                // Dependiendo de cómo desees manejarla en tu aplicación.
                Console.WriteLine($"Error al configurar DbContext: {ex.Message}");
                throw;
            }
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
            app.UseAuthentication();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Agrega el middleware de autorización aquí, después de UseRouting() y antes de UseEndpoints()
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
