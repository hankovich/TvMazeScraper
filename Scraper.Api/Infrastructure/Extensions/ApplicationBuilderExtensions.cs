//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ApplicationBuilderExtensions type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;

    using Scraper.Api.Infrastructure.Configuration;

    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder app, ApiInfoConfiguration apiInfoConfiguration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{apiInfoConfiguration.ApiVersion}/swagger.json", $"{apiInfoConfiguration.ApiName} {apiInfoConfiguration.ApiVersion}");
            });
        }
    }
}