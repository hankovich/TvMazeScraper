//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CastServerModel.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the CastServerModel type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using System.Collections.Generic;

    public class CastServerModel
    {
        public ICollection<Role> Cast { get; set; }
    }
}