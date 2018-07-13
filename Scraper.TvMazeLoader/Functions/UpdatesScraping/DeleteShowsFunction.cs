//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DeleteShowsFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the DeleteShowsFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class DeleteShowsFunction
    {
        [FunctionName(nameof(DeleteShowsFunction))]
        public static Task Run(
            [ActivityTrigger] IEnumerable<int> showIds,
            [Inject(typeof(IShowsService))] IShowsService showsService,
            ILogger logger)
        {
            logger.LogInformation("Deleting shows");
            return showsService.DeleteShowsAsync(showIds);
        }
    }
}