//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IDownloadService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IDownloadService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Abstract
{
    using System.Threading.Tasks;

    using Scraper.TvMazeLoader.Models;

    public interface IDownloadService
    {
        Task<ServerResponse<T>> GetAsync<T>(string uri);
    }
}