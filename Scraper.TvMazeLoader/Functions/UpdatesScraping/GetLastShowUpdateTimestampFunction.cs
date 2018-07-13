//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetLastShowUpdateTimestampFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetLastShowUpdateTimestampFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class GetLastShowUpdateTimestampFunction
    {
        [FunctionName(nameof(GetLastShowUpdateTimestampFunction))]
        public static Task<int> Run(
            [ActivityTrigger] DurableActivityContext context,
            [Inject(typeof(IShowUpdatesService))] IShowUpdatesService showUpdatesService,
            ILogger logger)
        {
            logger.LogInformation("Getting shows' last update time");
            return showUpdatesService.GetLastUpdateTimestampAsync();
        }
    }
}