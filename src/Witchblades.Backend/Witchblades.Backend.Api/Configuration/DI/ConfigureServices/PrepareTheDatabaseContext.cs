using Serilog;
using Witchblades.Backend.Data;

namespace Witchblades.Backend.Api.Configuration.ServiceCollectionConfiguration
{
    public static class PrepareTheDatabaseContextConfiguration
    {
        /// <summary>
        /// Ensures the context is created <br></br>
        /// Fills the context with data (if implementation of the IDatabaseInitializer is available)
        /// </summary>
        public static void PrepareTheDatabaseContext(this IApplicationBuilder app)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<WitchbladesContext>();
                    context.Database.EnsureCreated();

                    var initializer = scope.ServiceProvider.GetService<IDatabaseInitializer>();
                    if (initializer != null)
                    {
                        initializer.SeedDatabase();
                    }
                }
            }
            catch
            {
                Log.Logger.Fatal("An error occurred while preparing the database context");
            }
        }
    }
}
