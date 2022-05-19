using System.Text.Json.Serialization;
using Witchblades.Backend.Api.Configuration.ServiceCollectionConfiguration;
using Witchblades.Backend.Api.Utils;
using Witchblades.Backend.Api.Utils.Middlewares;
using Witchblades.Backend.Data;
using Witchblades.Logic.Interfaces;

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

            services.AddDatabaseContext(_configuration);
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            services.AddAutoMapper(typeof(AutoMappingProfile));
            services.AddSingleton<IPagedModelFactory, PagedModelFactory>();
        }

        // Middlewares
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Configurating swagger
            //if (env.IsDevelopment())
            //{
                app.UseDevelopmentTimeFeatures();
            //}

            // Database preparing
            app.PrepareTheDatabaseContext();

            app.UseCors(t => t.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());

            app.UseStaticFiles();
            // app.UseHttpsRedirection();
            // app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
