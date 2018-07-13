//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Show.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Show type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Autotests.Models
{
    using System.Collections.Generic;

    public class Show
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Actor> Cast { get; set; }
    }
}