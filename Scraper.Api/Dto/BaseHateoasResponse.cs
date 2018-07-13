//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BaseHateoasResponse.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the BaseHateoasResponse type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class BaseHateoasResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<string> Errors { get; set; }

        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<LinkDto> Links { get; set; }
    }
}