//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ContainerConfig.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ContainerConfig type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.DependencyInjection
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Autofac;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Scraper.Orm;
    using Scraper.Repositories;
    using Scraper.Repositories.Interface;
    using Scraper.TvMazeLoader.Models;

    using ActorDal = Scraper.Orm.Models.Actor;
    using ShowDal = Scraper.Orm.Models.Show;

    public static class ContainerConfig
    {
        private const string ConnectionStringName = "DefaultConnection";

        public static IContainer BuildContainer(ILoggerFactory factory)
        {
            var builder = new ContainerBuilder();

            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetInterfaces().Any()).ToArray();

            builder.RegisterTypes(assemblyTypes).AsImplementedInterfaces();

            var contextOptionsBuilder = new DbContextOptionsBuilder<ScraperContext>();
            var connectionString = Environment.GetEnvironmentVariable(ConnectionStringName);
            DbContextOptions<ScraperContext> dbContextOptions = contextOptionsBuilder.UseSqlServer(connectionString).Options;

            var scraperContext = new ScraperContext(dbContextOptions);
            builder.RegisterInstance(scraperContext).As<DbContext>();

            builder.RegisterType<WriteOnlyRepository<ShowDal>>().As<IWriteOnlyRepository<ShowDal>>();
            builder.RegisterType<ReadOnlyRepository<ShowDal>>().As<IReadOnlyRepository<ShowDal>>();

            var logger = factory.CreateLogger(nameof(ContainerConfig));
            builder.RegisterInstance(logger).As<ILogger>();

            builder.Register(c => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Actor, ActorDal>().ForMember(actor => actor.DateOfBirth, config => config.MapFrom(src => src.Birthday));
                    cfg.CreateMap<ActorDal, Actor>().ForMember(actor => actor.Birthday, config => config.MapFrom(src => src.DateOfBirth));

                    cfg.CreateMap<ShowWithCast, ShowDal>().ForMember(show => show.Timestamp, config => config.MapFrom(src => src.Updated));
                    cfg.CreateMap<ShowDal, ShowWithCast>().ForMember(show => show.Updated, config => config.MapFrom(src => src.Timestamp));

                    cfg.CreateMap<Show, ShowDal>().ReverseMap();
                })).AsImplementedInterfaces().SingleInstance();

            builder.Register(c => c.Resolve<IConfigurationProvider>().CreateMapper())
                .As<IMapper>();

            return builder.Build();
        }
    }
}