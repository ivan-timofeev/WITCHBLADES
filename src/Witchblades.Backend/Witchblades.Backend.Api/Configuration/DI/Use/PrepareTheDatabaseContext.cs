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
                    var context = scope.ServiceProvider.GetService<WitchbladesContext>();
                    context.Database.EnsureCreated();

                    var initializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
                    if (initializer != null)
                    {
                        initializer.SeedDatabase(context);
                    }
                }
            }
            catch
            {
                Log.Logger.Fatal("Application can't connect to the database (auto-restart in 10s)");
                Thread.Sleep(10000);
                Environment.Exit(-1);
            }
        }
    }
}
