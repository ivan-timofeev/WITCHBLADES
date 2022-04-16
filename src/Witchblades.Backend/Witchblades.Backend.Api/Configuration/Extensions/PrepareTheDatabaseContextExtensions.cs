using Witchblades.Backend.Data;

namespace Witchblades.Backend.Api.Configuration.ServiceCollectionConfiguration
{
    public static class PrepareTheDatabaseContextExtensions
    {
        /// <summary>
        /// Ensures the context is created <br></br>
        /// Fills the context with data (if implementation of the IDatabaseInitializer is available)
        /// </summary>
        public static void PrepareTheDatabaseContext(this IApplicationBuilder app)
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
    }
}
