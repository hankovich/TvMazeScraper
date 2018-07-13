//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetShowUpdatesFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetShowUpdatesFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Infrastructure;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class GetShowUpdatesFunction
    {
        [FunctionName(nameof(GetShowUpdatesFunction))]
        public static async Task<IEnumerable<ShowUpdate>> Run(
            [ActivityTrigger] DurableActivityContext context,
            [Inject(typeof(IDownloadService))] IDownloadService downloadService,
            [Inject(typeof(IConfiguration))] IConfiguration configuration,
            ILogger logger)
        {
            logger.LogInformation("Checking updates");

            string uri = $"{configuration.BaseTvMazeApiUri}/updates/shows";
            ServerResponse<Dictionary<int, int>> sr = await downloadService.GetAsync<Dictionary<int, int>>(uri);

            return sr.Content.Select(pair => new ShowUpdate { ShowId = pair.Key, Timespamp = pair.Value });
        }
    }
}