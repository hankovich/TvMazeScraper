//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IPageResponse.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IPageResponse type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    public interface IPageResponse
    {
        int? TotalPages { get; set; }

        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}