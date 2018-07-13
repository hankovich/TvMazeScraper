//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ServerResponse.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ServerResponse type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using System.Net;

    public class ServerResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Content { get; set; }
    }
}