//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CastInfo.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the CastInfo type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using System.Collections.Generic;

    public class CastInfo
    {
        public int ShowId { get; set; }

        public ICollection<Actor> Cast { get; set; }
    }
}
