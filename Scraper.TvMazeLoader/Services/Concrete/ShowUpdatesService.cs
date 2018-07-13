//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowUpdatesService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowUpdatesService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Concrete
{
    using System;
    using System.Threading.Tasks;

    using Scraper.Repositories.Interface;
    using Scraper.TvMazeLoader.Services.Abstract;

    using ShowDal = Scraper.Orm.Models.Show;

    public class ShowUpdatesService : IShowUpdatesService
    {
        private readonly IReadOnlyRepository<ShowDal> repository;

        public ShowUpdatesService(IReadOnlyRepository<ShowDal> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<int> GetLastUpdateTimestampAsync()
        {
            return this.repository.GetMaxAsync(show => show.Timestamp);
        }
    }
}