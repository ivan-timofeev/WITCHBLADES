using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json.Serialization;
using Witchblades.Backend.Api.Utils;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Api.Configuration
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        // Services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            services.AddApiVersioning(_configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.ConfigureSqlDbContext(_configuration);
            services.AddAutoMapper(typeof(AutoMappingProfile));
            services.AddScoped<IPagedModelFactory, PagedModelFactory>();
        }

        // Middlewares
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.

            // Configurating swagger
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var apiProvider = scope.ServiceProvider.GetService<IApiVersionDescriptionProvider>();

                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        foreach (var description in apiProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
                }
            }

            // Seeding the database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<WitchbladesContext>();

                context.Database.EnsureCreated();
                new DatabaseInitializer().SeedDatabase(context);
            }

            app.UseCors(t => t.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            // app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
