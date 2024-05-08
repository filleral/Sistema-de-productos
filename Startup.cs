using Sistema_de_gesti�n_de_productos_.Interfaces;
using Sistema_de_gesti�n_de_productos_.Entities;
using Microsoft.EntityFrameworkCore;
using Sistema_de_gesti�n_de_productos_.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag;
using Swashbuckle.AspNetCore.SwaggerGen;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authorization;
using TuProyecto.Authorization;


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
            try
            {

                // En el m�todo ConfigureServices de Startup.cs
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
                // Manejar la excepci�n adecuadamente, como registrarla o lanzarla nuevamente
                // Dependiendo de c�mo desees manejarla en tu aplicaci�n.
                Console.WriteLine($"Error al configurar DbContext: {ex.Message}");
                throw;
            }
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
            app.UseAuthentication();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Agrega el middleware de autorizaci�n aqu�, despu�s de UseRouting() y antes de UseEndpoints()
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
