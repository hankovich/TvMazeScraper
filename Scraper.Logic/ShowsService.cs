//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsService.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsService type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Scraper.Logic.Interface;
    using Scraper.Logic.Interface.Models;
    using Scraper.Repositories.Interface;

    using ShowDal = Scraper.Orm.Models.Show;

    public class ShowsService : IShowsService
    {
        private readonly IMapper mapper;

        private readonly IPagingReadOnlyRepository<ShowDal> showRepository;

        public ShowsService(IPagingReadOnlyRepository<ShowDal> showRepository, IMapper mapper)
        {
            this.showRepository = showRepository ?? throw new ArgumentNullException(nameof(showRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShowsPage> ListShowsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var pageTask = this.showRepository.GetPageAsync(
                pageNumber,
                pageSize,
                show => new ShowDal
                {
                    Id = show.Id,
                    Name = show.Name,
                    Cast = show.Cast.OrderByDescending(actor => actor.DateOfBirth)
                });

            var countTask = this.showRepository.GetTotalCountAsync();

            int count = await countTask;
            IEnumerable<ShowDal> dalPage = await pageTask;

            var page = this.mapper.Map<ICollection<Show>>(dalPage);

            var showsPage = new ShowsPage
            {
                Count = page.Count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)count / pageSize),
                TotalShows = count,
                Shows = page
            };

            return showsPage;
        }
    }
}