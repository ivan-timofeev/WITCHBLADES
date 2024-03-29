﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Serilog;

namespace Witchblades.Backend.Api.Configuration.ServiceCollectionConfiguration
{
    public static class UseDevelopmentTimeFeaturesConfiguration
    {
        /// <summary>
        /// Adds the swagger UI
        /// </summary>
        public static void UseDevelopmentTimeFeatures(this IApplicationBuilder app)
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

                // Redirect from / to /swagger/index.html
                var option = new RewriteOptions();
                option.AddRedirect("^$", "swagger");
                app.UseRewriter(option);

                // Use extended serilog
                app.UseSerilogRequestLogging(opts =>
                    opts.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms");
            }
        }
    }
}
