//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowDto.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowDto type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    using System.Collections.Generic;

    public class ShowDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ActorDto> Cast { get; set; }
    }
}