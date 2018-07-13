//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="GetShowWithCastFunction.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the GetShowWithCastFunction type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Functions.UpdatesScraping
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Azure.WebJobs;

    using Scraper.TvMazeLoader.DependencyInjection;
    using Scraper.TvMazeLoader.Infrastructure;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public class GetShowWithCastFunction
    {
        [FunctionName(nameof(GetShowWithCastFunction))]
        public static async Task<ShowWithCast> Run(
            [ActivityTrigger] int showId,
            [Inject(typeof(IDownloadService))] IDownloadService downloadService,
            [Inject(typeof(IConfiguration))] IConfiguration configuration)
        {
            string uri = $"{configuration.BaseTvMazeApiUri}/shows/{showId}?embed=cast";
            ServerResponse<EmbeddedShowInfo<CastServerModel>> sr = await downloadService.GetAsync<EmbeddedShowInfo<CastServerModel>>(uri);

            return new ShowWithCast
            {
                Id = sr.Content.Id,
                Name = sr.Content.Name,
                Updated = sr.Content.Updated,
                Cast = sr.Content.Embedded.Cast.Select(role => role.Person)
            };
        }
    }
}
