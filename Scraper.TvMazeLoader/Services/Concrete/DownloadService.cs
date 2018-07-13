//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DownloadService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the DownloadService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Concrete
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using Polly;
    using Polly.Retry;

    using Scraper.TvMazeLoader.Infrastructure;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    public class DownloadService : IDownloadService
    {
        private readonly IConfiguration configuration;

        private readonly ILogger logger;

        public DownloadService(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServerResponse<T>> GetAsync<T>(string uri)
        {
            ServerResponse<string> responseString = await this.GetServerResponseWithRetriesAsync(uri);

            T content = default;

            if (responseString.StatusCode.IsSuccessStatusCode())
            {
                content = JsonConvert.DeserializeObject<T>(responseString.Content);
            }

            return new ServerResponse<T>
            {
                Content = content,
                StatusCode = responseString.StatusCode
            };
        }

        private async Task<ServerResponse<string>> GetServerResponseWithRetriesAsync(string uri)
        {
            this.logger.LogInformation($"Starting downloader for {uri}");

            var client = new HttpClient();

            RetryPolicy<ServerResponse<string>> policy = Policy
                .HandleResult<ServerResponse<string>>(response => response.StatusCode == (HttpStatusCode)429)
                .WaitAndRetryForeverAsync(i => this.configuration.WaitAfterServerLimitTimeSpan);

            ServerResponse<string> result = await policy.ExecuteAsync(() => this.GetServerResponseOnceAsync(client, uri));

            this.logger.LogInformation($"Stopping downloader for {uri}");

            return result;
        }

        private async Task<ServerResponse<string>> GetServerResponseOnceAsync(HttpClient client, string uri)
        {
            using (var responseMessage = await client.GetAsync(uri))
            {
                this.logger.LogInformation($"Executed request to {uri} with {responseMessage.StatusCode}");

                return new ServerResponse<string>
                {
                    Content = await responseMessage.Content.ReadAsStringAsync(),
                    StatusCode = responseMessage.StatusCode
                };
            }
        }
    }
}