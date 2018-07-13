//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IShowUpdatesService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IShowUpdatesService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Abstract
{
    using System.Threading.Tasks;

    public interface IShowUpdatesService
    {
        Task<int> GetLastUpdateTimestampAsync();
    }
}