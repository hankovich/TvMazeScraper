//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IShowsService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IShowsService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Logic.Interface
{
    using System.Threading.Tasks;

    using Scraper.Logic.Interface.Models;

    public interface IShowsService
    {
        Task<ShowsPage> ListShowsAsync(int pageNumber, int pageSize);
    }
}