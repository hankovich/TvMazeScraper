//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetShowsPageFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetShowsPageFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.InitialLoading
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Infrastructure;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class GetShowsPageFunction
    {
        [FunctionName(nameof(GetShowsPageFunction))]
        public static async Task<ShowsPage> Run(
            [ActivityTrigger] int pageNumber,
            [Inject(typeof(IDownloadService))] IDownloadService downloadService,
            [Inject(typeof(IConfiguration))] IConfiguration configuration)
        {
            string uri = $"{configuration.BaseTvMazeApiUri}/shows?page={pageNumber}";
            ServerResponse<ICollection<Show>> sr = await downloadService.GetAsync<ICollection<Show>>(uri);

            return new ShowsPage { Shows = sr.Content, IsLastPage = !sr.StatusCode.IsSuccessStatusCode() };
        }
    }
}