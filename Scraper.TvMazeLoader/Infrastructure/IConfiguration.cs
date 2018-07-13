//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IConfiguration.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IConfiguration type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Infrastructure
{
    using System;

    public interface IConfiguration
    {
        TimeSpan WaitAfterServerLimitTimeSpan { get; }

        string BaseTvMazeApiUri { get; }
    }
}