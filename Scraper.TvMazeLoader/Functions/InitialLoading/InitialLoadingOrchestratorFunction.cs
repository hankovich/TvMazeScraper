//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InitialLoadingOrchestratorFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the InitialLoadingOrchestratorFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.InitialLoading
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;

    using Scraper.TvMazeLoader.Functions.SharedFunctions;
    using Scraper.TvMazeLoader.Models;

    public static class InitialLoadingOrchestratorFunction
    {
        [FunctionName(nameof(InitialLoadingOrchestratorFunction))]
        public static async Task Run(
            [OrchestrationTrigger] DurableOrchestrationContext orchestrationContext,
            ILogger logger)
        {
            logger.LogInformation("{0} function processed a request.", nameof(InitialLoadingOrchestratorFunction));
            
            int pageNumber = 1;

            while (true)
            {
                logger.LogInformation("Processing {0} page", pageNumber);

                ShowsPage page = await orchestrationContext.CallActivityAsync<ShowsPage>(nameof(GetShowsPageFunction), pageNumber);

                if (page.IsLastPage)
                {
                    break;
                }

                IEnumerable<Task<CastInfo>> castInfosTasks = page.Shows.Select(
                    show => orchestrationContext.CallActivityAsync<CastInfo>(nameof(GetShowCastFunction), show.Id));

                var castInfos = await Task.WhenAll(castInfosTasks);

                var showsWithCast = new List<ShowWithCast>(page.Shows.Count);

                foreach (var castInfo in castInfos)
                {
                    var showToEnrich = page.Shows.First(show => show.Id == castInfo.ShowId);
                    showsWithCast.Add(
                        new ShowWithCast
                        {
                            Id = showToEnrich.Id,
                            Name = showToEnrich.Name,
                            Updated = showToEnrich.Updated,
                            Cast = castInfo.Cast
                        });
                }

                await orchestrationContext.CallActivityAsync(nameof(InsertShowsFunction), showsWithCast);
                pageNumber++;
            }
        }
    }
}