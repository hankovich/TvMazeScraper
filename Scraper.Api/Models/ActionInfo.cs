//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ActionInfo.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ActionInfo type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Models
{
    using Microsoft.AspNetCore.Mvc;

    public class ActionInfo
    {
        public IUrlHelper UrlHelper { get; set; }

        public string RouteName { get; set; }

        public string HttpMethod { get; set; }
    }
}