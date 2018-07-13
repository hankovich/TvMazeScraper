//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringResources.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the StringResources type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Resources
{
    public class StringResources
    {
        public PagingStringResources PageStringResources { get; set; }

        public ValidationStringResources ValidationStringResources { get; set; }

        public string SomethingWentWrong { get; set; }
    }
}