using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Witchblades.Backend.Data;
using Witchblades.Backend.Api.Configuration;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Witchblades.Backend.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureSqlDbContext(builder.Configuration);
// Json settings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Add api versioning
builder.Services.AddApiVersioning(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
builder.Services.AddScoped<IPagedModelFactory, PagedModelFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.

// Configurating swagger
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
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
using (var scope = app.Services.CreateScope())
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
app.UseAuthorization();
app.MapControllers();

app.Run();
