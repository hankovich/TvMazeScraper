//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ActorDto.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ActorDto type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    public class ActorDto
    {
        public int Id { get; set; }

        public string Birthday { get; set; }
    
        public string Name { get; set; }
    }
}