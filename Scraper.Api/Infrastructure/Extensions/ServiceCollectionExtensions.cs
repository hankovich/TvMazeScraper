//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ServiceCollectionExtensions type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Scraper.Api.Infrastructure.Configuration;
    using Scraper.Api.Infrastructure.Validators.Abstract;
    using Scraper.Api.Infrastructure.Validators.Concrete;
    using Scraper.Api.Middleware;
    using Scraper.Logic;
    using Scraper.Logic.Interface;
    using Scraper.Orm;
    using Scraper.Orm.Models;
    using Scraper.Repositories;
    using Scraper.Repositories.Interface;

    using Swashbuckle.AspNetCore.Swagger;

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureContainer(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ScraperContext>();

            services.AddScoped<IShowsService, ShowsService>();
            services.AddScoped<IPagingReadOnlyRepository<Show>, ReadOnlyRepository<Show>>();

            services.AddSingleton<IPageParametersValidator, PageParametersValidator>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(
                provider =>
                {
                    var actionContext = provider.GetRequiredService<IActionContextAccessor>().ActionContext;
                    var factory = provider.GetRequiredService<IUrlHelperFactory>();
                    return factory.GetUrlHelper(actionContext);
                });
        }

        public static void AddSwagger(this IServiceCollection services, ApiInfoConfiguration apiInfoConfiguration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiInfoConfiguration.ApiVersion, new Info { Title = apiInfoConfiguration.ApiName, Version = apiInfoConfiguration.ApiVersion });
            });
        }

        public static void AddMvc(this IServiceCollection services, CacheConfiguration cacheConfiguration)
        {
            services.AddMvc(
                options =>
                {
                    options.CacheProfiles.Add(
                        CacheProfiles.Default,
                        new CacheProfile { Duration = (int)cacheConfiguration.DefaultMaxAgeSeconds });

                    options.CacheProfiles.Add(
                        CacheProfiles.Never,
                        new CacheProfile { Location = ResponseCacheLocation.None, NoStore = true });

                    options.Filters.Add<ScraperHateoasExceptionFilterAttribute>();
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}