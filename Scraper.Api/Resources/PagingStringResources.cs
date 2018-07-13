//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PagingStringResources.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the PagingStringResources type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Resources
{
    public class PagingStringResources
    {
        public string FirstPage { get; set; }

        public string PreviousPage { get; set; }

        public string CurrentPage { get; set; }

        public string NextPage { get; set; }

        public string LastPage { get; set; }
    }
}
