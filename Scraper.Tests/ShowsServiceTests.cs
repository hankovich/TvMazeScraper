//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsServiceTests.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsServiceTests type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;

    using Moq;

    using NUnit.Framework;

    using Scraper.Logic;
    using Scraper.Logic.Interface.Models;
    using Scraper.Repositories.Interface;

    using ActorDal = Scraper.Orm.Models.Actor;
    using ActorLogic = Scraper.Logic.Interface.Models.Actor;
    using ShowDal = Scraper.Orm.Models.Show;
    using ShowLogic = Scraper.Logic.Interface.Models.Show;

    [TestFixture]
    public class ShowsServiceTests
    {
        public ShowsService Service { get; set; }

        public IList<ShowDal> Shows { get; set; }

        [SetUp]
        public void Init()
        {
            this.Shows = GetShows();

            var repositoryMock = new Mock<IPagingReadOnlyRepository<ShowDal>>();

            repositoryMock
                .Setup(
                    rep => rep.GetPageAsync(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsNotNull<Expression<Func<ShowDal, ShowDal>>>()))
                .Returns(Task.FromResult<IEnumerable<ShowDal>>(this.Shows));
            repositoryMock.Setup(rep => rep.GetTotalCountAsync()).Returns(Task.FromResult(this.Shows.Count()));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(map => map.Map<ICollection<Show>>(It.IsNotNull<IEnumerable<Show>>())).Returns(
                this.Shows.Select(
                    show => new ShowLogic
                    {
                        Id = show.Id,
                        Name = show.Name,
                        Cast = show.Cast.Select(
                            actor => new ActorLogic { Id = actor.Id, Name = actor.Name, Birthday = actor.DateOfBirth })
                    }).ToList());

            this.Service = new ShowsService(repositoryMock.Object, mapperMock.Object);
        }

        [TestCase]
        public void ListShowsAsyncTest_InvalidPageNumber_ShouldThrowsException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await this.Service.ListShowsAsync(0, 25));
        }

        [TestCase]
        public void ListShowsAsyncTest_InvalidPageSize_ShouldThrowsException()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await this.Service.ListShowsAsync(1, int.MinValue));
        }

        [TestCase]
        public async Task ListShowsAsyncTest_ValidCase_ShouldReturnPage()
        {
            int requestedPageSize = 25;
            var page = await this.Service.ListShowsAsync(1, requestedPageSize);

            Assert.AreEqual(page.TotalShows, this.Shows.Count());
            Assert.LessOrEqual(page.Count, requestedPageSize);

            Show[] arrayShows = page.Shows.ToArray();
            for (var index = 0; index < arrayShows.Length; index++)
            {
                var show = arrayShows[index];
                var originalShow = this.Shows[index];

                Assert.AreEqual(show.Id, originalShow.Id);
                Assert.AreEqual(show.Name, originalShow.Name);

                var castArray = show.Cast.ToArray();
                var originalCastArray = originalShow.Cast.ToArray();

                for (int i = 0; i < castArray.Length; i++)
                {
                    var actor = castArray[i];
                    var originalActor = originalCastArray[i];

                    Assert.AreEqual(actor.Id, originalActor.Id);
                    Assert.AreEqual(actor.Name, originalActor.Name);
                    Assert.AreEqual(actor.Birthday, originalActor.DateOfBirth);
                }
            }
        }

        private static IList<ShowDal> GetShows()
        {
            var showFirst = new ShowDal
            {
                Id = 3,
                Name = "Test show",
                Timestamp = new Random().Next(),
                Cast = new List<ActorDal>
                {
                    new ActorDal { Id = 2, DateOfBirth = DateTime.Now.AddYears(-40).ToString("u"), Name = "John" },
                    new ActorDal { Id = 12, DateOfBirth = DateTime.Now.AddYears(-53).ToString("u"), Name = "Bob" }
                }
            };

            var showSecond = new ShowDal
            {
                Id = 5,
                Name = "Test show 2",
                Timestamp = new Random().Next(),
                Cast = new List<ActorDal>
                {
                    new ActorDal { Id = 1, DateOfBirth = DateTime.Now.AddYears(-17).ToString("u"), Name = "Mike" },
                    new ActorDal { Id = 15, DateOfBirth = DateTime.Now.AddYears(-42).ToString("u"), Name = "Alice" }
                }
            };

            return new List<ShowDal> { showFirst, showSecond };
        }
    }
}
