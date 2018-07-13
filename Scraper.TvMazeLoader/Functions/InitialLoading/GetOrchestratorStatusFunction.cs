//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetOrchestratorStatusFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetOrchestratorStatusFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.InitialLoading
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class GetOrchestratorStatusFunction
    {
        private static readonly JsonSerializerSettings SerializerSettings =
            new JsonSerializerSettings { Converters = new List<JsonConverter> { new StringEnumConverter() } };

        [FunctionName(nameof(GetOrchestratorStatusFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, Route = "GetStatusFunction/{instanceId}")] HttpRequest req,
            string instanceId,
            [OrchestrationClient] DurableOrchestrationClient client,
            ILogger logger)
        {
            logger.LogInformation(
                "{0} function processed a request (instanceId = {1}).",
                nameof(GetOrchestratorStatusFunction),
                instanceId);

            DurableOrchestrationStatus status = await client.GetStatusAsync(instanceId);

            if (status == null)
            {
                return new NotFoundResult();
            }

            return new JsonResult(status, SerializerSettings);
        }
    }
}