using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Ginasio.Core.Interfaces;
using Ginasio.Core;
using Ginasio.Core.Services;
using Ginasio.Infrastructure.Repositories;
using Ginasio.Infrastructure.Data;

namespace Ginasio.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Aqui configuramos os serviços
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers
            services.AddControllers();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ginasio API",
                    Version = "v1",
                    Description = "API para gerenciamento de Ginasios no sistema"
                });
            });

            services.AddDbContext<GinasioDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    o => o.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IGinasioHandler, GinasioHandler>();
            services.AddScoped<IGinasioRepository, GinasioRepository>();
            services.AddScoped<IGinasioService, GinasioService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ginasio API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}