//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MappingProfile.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the MappingProfile type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Configuration
{
    using AutoMapper;

    using Scraper.Api.Dto;
    using Scraper.Logic.Interface.Models;

    using ActorDal = Scraper.Orm.Models.Actor;
    using ShowDal = Scraper.Orm.Models.Show;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Actor, ActorDto>().ReverseMap();
            this.CreateMap<Show, ShowDto>().ReverseMap();

            this.CreateMap<ShowsPage, ShowsDto>().ReverseMap();
            this.CreateMap<Actor, Actor>().ReverseMap();

            this.CreateMap<Show, Show>().ReverseMap();

            this.CreateMap<Actor, ActorDal>().ForMember(actor => actor.DateOfBirth, options => options.MapFrom(src => src.Birthday));
            this.CreateMap<ActorDal, Actor>().ForMember(actor => actor.Birthday, options => options.MapFrom(src => src.DateOfBirth));

            this.CreateMap<Show, ShowDal>().ReverseMap();
        }
    }
}
