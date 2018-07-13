//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InsertShowsFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the InsertShowsFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.SharedFunctions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class InsertShowsFunction
    {
        [FunctionName(nameof(InsertShowsFunction))]
        public static Task Run(
            [ActivityTrigger] IEnumerable<ShowWithCast> page,
            [Inject(typeof(IShowsService))] IShowsService pageSaverService,
            ILogger logger)
        {
            logger.LogInformation($"Inserting page");

            return pageSaverService.InsertPageAsync(page);
        }
    }
}