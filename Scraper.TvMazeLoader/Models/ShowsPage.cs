//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsPage.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsPage type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using System.Collections.Generic;

    public class ShowsPage
    {
        public ICollection<Show> Shows { get; set; }

        public bool IsLastPage { get; set; }
    }
}