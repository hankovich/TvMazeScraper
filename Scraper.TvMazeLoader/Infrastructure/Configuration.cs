//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Configuration.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Configuration type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Infrastructure
{
    using System;

    public class Configuration : IConfiguration
    {
        public TimeSpan WaitAfterServerLimitTimeSpan { get; } = TimeSpan.Parse(
            Environment.GetEnvironmentVariable(nameof(WaitAfterServerLimitTimeSpan)));

        public string BaseTvMazeApiUri { get; } = Environment.GetEnvironmentVariable(nameof(BaseTvMazeApiUri));
    }
}