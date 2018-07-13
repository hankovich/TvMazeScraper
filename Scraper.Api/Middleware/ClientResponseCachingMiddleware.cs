//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ClientResponseCachingMiddleware.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ClientResponseCachingMiddleware type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Middleware
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    using Scraper.Api.Infrastructure.Configuration;

    public class ClientResponseCachingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly CacheConfiguration CacheConfiguration;

        public ClientResponseCachingMiddleware(RequestDelegate next, CacheConfiguration cacheConfiguration)
        {
            this.next = next;
            this.CacheConfiguration = cacheConfiguration;
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.Response.GetTypedHeaders().CacheControl =
                new CacheControlHeaderValue { Public = true, MaxAge = TimeSpan.FromSeconds(this.CacheConfiguration.DefaultMaxAgeSeconds) };

            context.Response.Headers[HeaderNames.Vary] = new[] { HeaderNames.AcceptEncoding };

            return this.next(context);
        }
    }
}