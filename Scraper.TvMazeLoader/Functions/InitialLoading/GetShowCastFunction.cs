//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetShowCastFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetShowCastFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.InitialLoading
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Infrastructure;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public static class GetShowCastFunction
    {
        [FunctionName(nameof(GetShowCastFunction))]
        public static async Task<CastInfo> Run(
            [ActivityTrigger] int showId,
            [Inject(typeof(IDownloadService))] IDownloadService downloadService,
            [Inject(typeof(IConfiguration))] IConfiguration configuration)
        {
            string uri = $"{configuration.BaseTvMazeApiUri}/shows/{showId}/cast";
            ServerResponse<ICollection<Role>> sr = await downloadService.GetAsync<ICollection<Role>>(uri);

            return new CastInfo { ShowId = showId, Cast = sr.Content?.Select(role => role.Person).ToList() };
        }
    }
}