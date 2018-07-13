//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="UpdatesScrapingOrchestratorFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the UpdatesScrapingOrchestratorFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.Functions.SharedFunctions;
    using Scraper.TvMazeLoader.Models;

    public static class UpdatesScrapingOrchestratorFunction
    {
        private const int PageSize = 250;

        [FunctionName(nameof(UpdatesScrapingOrchestratorFunction))]
        public static async Task Run(
            [OrchestrationTrigger] DurableOrchestrationContext orchestrationContext,
            ILogger logger)
        {
            logger.LogInformation("{0} function started to process a request. (InstanceId = {1})", nameof(UpdatesScrapingOrchestratorFunction), orchestrationContext.InstanceId);

            Task<IEnumerable<ShowUpdate>> updatesTask =
                orchestrationContext.CallActivityAsync<IEnumerable<ShowUpdate>>(nameof(GetShowUpdatesFunction), null);
            Task<int> lastUpdateTimestampTask = orchestrationContext.CallActivityAsync<int>(
                nameof(GetLastShowUpdateTimestampFunction),
                null);

            IEnumerable<ShowUpdate> updates = await updatesTask;
            int lastUpdateTimestamp = await lastUpdateTimestampTask;

            var updatedShows = updates.Where(update => update.Timespamp > lastUpdateTimestamp);

            int pageNumber = 1;

            while (true)
            {
                IEnumerable<Task<ShowWithCast>> showsTasks = updatedShows.Skip((pageNumber - 1) * PageSize).Take(PageSize).Select(
                    update => orchestrationContext.CallActivityAsync<ShowWithCast>(
                        nameof(GetShowWithCastFunction),
                        update.ShowId));

                ShowWithCast[] showsPage = await Task.WhenAll(showsTasks);

                if (!showsPage.Any())
                {
                    break;
                }

                await orchestrationContext.CallActivityAsync(nameof(DeleteShowsFunction), showsPage.Select(show => show.Id));

                await orchestrationContext.CallActivityAsync(nameof(InsertShowsFunction), showsPage);

                pageNumber++;
            }

            logger.LogInformation("{0} function ended to process a request. (InstanceId = {1})", nameof(UpdatesScrapingOrchestratorFunction), orchestrationContext.InstanceId);
        }
    }
}