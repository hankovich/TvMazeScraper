//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IShowsService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IShowsService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Scraper.TvMazeLoader.Models;

    public interface IShowsService
    {
        Task InsertPageAsync(IEnumerable<ShowWithCast> shows);

        Task DeleteShowsAsync(IEnumerable<int> showIds);
    }
}