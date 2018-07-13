//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsDto.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsDto type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ShowsDto : PageHateoasResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ShowDto> Shows { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalShows { get; set; }
    }
}
