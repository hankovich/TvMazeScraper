//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ShowsController.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ShowsController type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    using Scraper.Api.Dto;
    using Scraper.Api.Infrastructure.Configuration;
    using Scraper.Api.Infrastructure.Validators.Abstract;
    using Scraper.Api.Models;
    using Scraper.Api.Resources;
    using Scraper.Logic.Interface;
    using Scraper.Logic.Interface.Models;

    using Swashbuckle.AspNetCore.SwaggerGen;

    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IPageParametersValidator pageParametersValidator;

        private readonly IMapper mapper;

        private readonly IShowsService showsService;

        private readonly PagingStringResources pageStringResources;

        private readonly IUrlHelper urlHelper;

        public ShowsController(
            IShowsService showsService,
            IPageParametersValidator pageParametersValidator,
            IMapper mapper,
            PagingStringResources pageStringResources,
            IUrlHelper urlHelper)
        {
            this.showsService = showsService ?? throw new ArgumentNullException(nameof(showsService));
            this.pageParametersValidator = pageParametersValidator ?? throw new ArgumentNullException(nameof(pageParametersValidator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.pageStringResources = pageStringResources ?? throw new ArgumentNullException(nameof(pageStringResources));
            this.urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        [HttpGet, Route(nameof(List), Name = nameof(List))]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, VaryByHeader = HeaderNames.AcceptEncoding, CacheProfileName = CacheProfiles.Default)]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ShowsDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(BaseHateoasResponse))]
        public async Task<IActionResult> List([FromQuery] PagingParameters parameters)
        {
            this.pageParametersValidator.EnsureValid(parameters);

            ShowsPage showsPage = await this.showsService.ListShowsAsync(parameters.PageNumber, parameters.PageSize);

            var showsDto = this.mapper.Map<ShowsDto>(showsPage);

            var actionInfo = new ActionInfo
            {
                UrlHelper = this.urlHelper,
                RouteName = nameof(this.List),
                HttpMethod = HttpMethod.Get.Method
            };

            showsDto.GenerateLinks(this.pageStringResources, actionInfo);

            return this.Ok(showsDto);
        }
    }
}
