//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="LinkDto.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the LinkDto type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    public class LinkDto
    {
        public string Href { get; set; }

        public string Method { get; set; }

        public string Rel { get; set; }
    }
}
