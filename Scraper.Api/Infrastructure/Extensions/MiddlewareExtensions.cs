//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MiddlewareExtensions.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the MiddlewareExtensions type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;

    using Scraper.Api.Middleware;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseClientResponseCaching(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientResponseCachingMiddleware>();
        }
    }
}