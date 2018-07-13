//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PagingParameters.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the PagingParameters type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Models
{
    public class PagingParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 25;
    }
}
