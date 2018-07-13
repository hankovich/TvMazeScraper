//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.Services.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Scraper.Repositories.Interface;
    using Scraper.TvMazeLoader.Models;
    using Scraper.TvMazeLoader.Services.Abstract;

    using ShowDal = Scraper.Orm.Models.Show;

    public class ShowsService : IShowsService
    {
        private readonly IWriteOnlyRepository<ShowDal> repository;

        private readonly IMapper mapper;

        public ShowsService(IWriteOnlyRepository<ShowDal> repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task InsertPageAsync(IEnumerable<ShowWithCast> shows)
        {
            var repositoriesShow = this.mapper.Map<IEnumerable<ShowDal>>(shows);
            return this.repository.InsertRangeAsync(repositoriesShow);
        }

        public Task DeleteShowsAsync(IEnumerable<int> showIds)
        {
            return this.repository.DeleteByIdsAsync(showIds);
        }
    }
}