//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ScrapeUpdatesTriggerFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ScrapeUpdatesTriggerFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    public static class ScrapeUpdatesTriggerFunction
    {
        [FunctionName(nameof(ScrapeUpdatesTriggerFunction))]
        public static async Task Run(
            [TimerTrigger("%UpdatesScrapingCronExpression%", RunOnStartup = true)] TimerInfo timerInfo,
            [OrchestrationClient] DurableOrchestrationClient client,
            ILogger logger)
        {
            logger.LogInformation("{0} function processed a request.", nameof(ScrapeUpdatesTriggerFunction));

            if (timerInfo.IsPastDue)
            {
                logger.LogInformation("Timer invocation is due to a missed schedule occurrence. Skipping invocation...");
                return;
            }

            logger.LogInformation("{0} function processed a request.", nameof(ScrapeUpdatesTriggerFunction));

            await TerminateRunningUpdatersAsync(client);

            var instanceId = await client.StartNewAsync(nameof(UpdatesScrapingOrchestratorFunction), null);

            logger.LogInformation("{0} function started (InstanceId = {1})", nameof(UpdatesScrapingOrchestratorFunction), instanceId);
        }

        private static async Task TerminateRunningUpdatersAsync(DurableOrchestrationClient client)
        {
            var statuses = await client.GetStatusAsync();

            await Task.WhenAll(statuses.Select(status => client.TerminateAsync(status.InstanceId, "New updater created")));
        }
    }
}
