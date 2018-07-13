//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PageHateoasResponse.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the PageHateoasResponse type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Dto
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Scraper.Api.Models;
    using Scraper.Api.Resources;

    public class PageHateoasResponse : BaseHateoasResponse, IPageResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalPages { get; set; }

        [JsonIgnore]
        public int PageNumber { get; set; }

        [JsonIgnore]
        public int PageSize { get; set; }

        public virtual void GenerateLinks(
            PagingStringResources stringResources,
            ActionInfo actionInfo)
        {
            const int FirstPageNumber = 1;
            int totalPages = this.TotalPages ?? 0;

            this.Links = new List<LinkDto>();

            this.AddLink(FirstPageNumber, stringResources.FirstPage, actionInfo);

            if (this.PageNumber > FirstPageNumber && this.PageNumber <= totalPages)
            {
                this.AddLink(this.PageNumber - 1, stringResources.PreviousPage, actionInfo);
            }

            if (this.PageNumber >= FirstPageNumber && this.PageNumber <= totalPages)
            {
                this.AddLink(this.PageNumber, stringResources.CurrentPage, actionInfo);
            }

            if (this.PageNumber >= FirstPageNumber - 1 && this.PageNumber < totalPages)
            {
                this.AddLink(this.PageNumber + 1, stringResources.NextPage, actionInfo);
            }

            if (totalPages > 0)
            {
                this.AddLink(totalPages, stringResources.LastPage, actionInfo);
            }
        }

        private void AddLink(int number, string description, ActionInfo info)
        {
            this.Links.Add(
                new LinkDto
                {
                    Href = info.UrlHelper.Link(
                        info.RouteName,
                        new PagingParameters { PageNumber = number, PageSize = this.PageSize }),
                    Rel = description,
                    Method = info.HttpMethod
                });
        }
    }
}