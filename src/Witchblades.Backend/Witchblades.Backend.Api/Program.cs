using Serilog;
using Witchblades.Backend.Api.Configuration;

namespace Witchblades
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Read Configuration from appsettings_serilog
            var config = GetConfiguration();

            // Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            try
            {
                Log.ForContext<Program>().Information("The Application runs");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());

        private static IConfigurationRoot GetConfiguration()
        {
            bool isDevelopment = string.Equals(
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "development",
                StringComparison.InvariantCultureIgnoreCase);

            if (isDevelopment)
            {
                return new ConfigurationBuilder()
                    .AddJsonFile("appsettings_serilog.Development.json")
                    .Build();
            }
            else
            {
                return new ConfigurationBuilder()
                    .AddJsonFile("appsettings_serilog.json")
                    .Build();
            }
        }
    }
}