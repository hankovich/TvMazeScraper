//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Extensions.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Extensions type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Infrastructure
{
    using System.Net;

    public static class Extensions
    {
        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
        {
            return (int)statusCode >= 200 && (int)statusCode <= 299;
        }
    }
}
