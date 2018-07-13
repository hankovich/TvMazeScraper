//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Extensions.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Extensions type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Extensions
{
    using Microsoft.Extensions.Configuration;

    public static class Extensions
    {
        public static T GetSectionAs<T>(this IConfiguration configuration, string sectionName = null) where T : new()
        {
            var instance = new T();
            configuration.GetSection(sectionName ?? typeof(T).Name).Bind(instance);

            return instance;
        }
    }
}
