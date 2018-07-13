//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowWithCast.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowWithCast type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using System.Collections.Generic;

    public class ShowWithCast : Show
    {
        public IEnumerable<Actor> Cast { get; set; }
    }
}