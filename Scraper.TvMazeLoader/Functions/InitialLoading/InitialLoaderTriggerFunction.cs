//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InitialLoaderTriggerFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the InitialLoaderTriggerFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.InitialLoading
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public static class InitialLoaderTriggerFunction
    {
        [FunctionName(nameof(InitialLoaderTriggerFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [OrchestrationClient] DurableOrchestrationClient client,
            ILogger logger)
        {
            logger.LogInformation("{0} function processed a request.", nameof(InitialLoaderTriggerFunction));

            await TerminateRunningLoadersAsync(client);

            string orchestratorId = await client.StartNewAsync(nameof(InitialLoadingOrchestratorFunction), null);

            return new OkObjectResult(orchestratorId);
        }

        private static async Task TerminateRunningLoadersAsync(DurableOrchestrationClient client)
        {
            var statuses = await client.GetStatusAsync();

            await Task.WhenAll(statuses.Select(status => client.TerminateAsync(status.InstanceId, "New loader created")));
        }
    }
}
