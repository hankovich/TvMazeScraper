//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IPageParametersValidator.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IPageParametersValidator type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Validators.Abstract
{
    using Scraper.Api.Models;

    public interface IPageParametersValidator
    {
        void EnsureValid(PagingParameters pagingParameters);
    }
}
