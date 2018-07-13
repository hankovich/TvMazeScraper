//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsPage.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsPage type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Logic.Interface.Models
{
    using System.Collections.Generic;

    public class ShowsPage
    {
        public IEnumerable<Show> Shows { get; set; }

        public int Count { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalShows { get; set; }
    }
}