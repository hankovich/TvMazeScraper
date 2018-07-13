//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EmbeddedShowInfo.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the EmbeddedShowInfo type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Models
{
    using Newtonsoft.Json;

    public class EmbeddedShowInfo<T> : Show
    {
        [JsonProperty("_embedded")]
        public T Embedded { get; set; }
    }
}
