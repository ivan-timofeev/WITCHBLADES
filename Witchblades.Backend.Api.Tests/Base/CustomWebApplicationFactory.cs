using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Witchblades.Backend.Api.Configuration;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Api.Tests.Base
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove real database context
                {
                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<WitchbladesContext>));

                    services.Remove(descriptor);
                }

                // Remove database initializer
                {
                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(IDatabaseInitializer));

                    services.Remove(descriptor);
                }


                services.AddDbContext<WitchbladesContext>(options =>
                {
                    options.UseInMemoryDatabase($"WitchbladesContext.Tests.InMemory");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<WitchbladesContext>();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
