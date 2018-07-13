//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Show.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Show type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Orm.Models
{
    using System.Collections.Generic;

    public class Show : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Timestamp { get; set; }

        public IEnumerable<Actor> Cast { get; set; }
    }
}