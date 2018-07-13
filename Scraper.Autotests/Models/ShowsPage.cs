//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsPage.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsPage type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Autotests.Models
{
    using System.Collections.Generic;

    public class ShowsPage
    {
        public IEnumerable<Show> Shows { get; set; }

        public int? Count { get; set; }

        public int? TotalPages { get; set; }

        public int? TotalShows { get; set; }

        public IList<string> Errors { get; set; }
    }
}
