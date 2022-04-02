﻿using Microsoft.AspNetCore.Mvc;

namespace Witchblades.Backend.Api.Configuration
{
    public static class VersioningConfiguration
    {
        public static void AddApiVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            ApiVersion? apiVersion = null;

            try
            {
                var version = configuration["DefaultApiVersion"];

                int major = int.Parse(version.Split('.')[0]);
                int minor = int.Parse(version.Split('.')[1]);

                apiVersion = new ApiVersion(major, minor);
            }
            catch
            {
                throw new InvalidDataException("Invalid default api version\n" +
                    "Use {Major:int}.{Minor:int} (example: 1.0)");
            }

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = apiVersion;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
