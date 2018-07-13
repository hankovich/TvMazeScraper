//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ListShowsContext.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ListShowsContext type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Autotests.Models
{
    using System.Collections.Generic;
    using System.Net.Http;

    public class ListShowsContext
    {
        public Dictionary<string, string> QueryParameters { get; } = new Dictionary<string, string>();

        public HttpResponseMessage ServerResponseMessage { get; set; }

        public ShowsPage ResponsePage { get; set; }
    }
}
