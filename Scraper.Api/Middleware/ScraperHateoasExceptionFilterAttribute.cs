//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ScraperHateoasExceptionFilterAttribute.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ScraperHateoasExceptionFilterAttribute type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Scraper.Api.Dto;
    using Scraper.Api.Infrastructure.Exceptions;
    using Scraper.Api.Models;
    using Scraper.Api.Resources;

    public class ScraperHateoasExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly StringResources stringResources;

        public ScraperHateoasExceptionFilterAttribute(StringResources stringResources)
        {
            this.stringResources = stringResources ?? throw new ArgumentNullException(nameof(stringResources));
        }

        public override void OnException(ExceptionContext context)
        {
            var urlHelper =
                (IUrlHelper)context.HttpContext.RequestServices.GetService(typeof(IUrlHelper));

            switch (context.Exception)
            {
                case PagingValidationException pagingValidationException:
                    PageHateoasResponse pagingErrorResponse = ProcessPagingValidationException(pagingValidationException, context, this.stringResources.PageStringResources, urlHelper);

                    context.Result = new BadRequestObjectResult(pagingErrorResponse);
                    break;
                case ValidationException validationException:
                    BaseHateoasResponse validationErrorResponse = ProcessValidationException(validationException);

                    context.Result = new BadRequestObjectResult(validationErrorResponse);
                    break;
                case SqlException _:
                    BaseHateoasResponse sqlErrorResponse = ProcessSqlException(stringResources);

                    context.Result = new BadRequestObjectResult(sqlErrorResponse);
                    break;
                case Exception exception:
                    BaseHateoasResponse errorResponse = ProcessException(exception);

                    context.Result = new BadRequestObjectResult(errorResponse);
                    break;
            }

            context.ExceptionHandled = true;
        }

        private static PageHateoasResponse ProcessPagingValidationException(PagingValidationException pagingValidationException, ExceptionContext context, PagingStringResources pageStringResources, IUrlHelper urlHelper)
        {
            const int DefaultPageNumber = 1;
            const int DefaultPageSize = 25;

            var errorResponse = new PageHateoasResponse
            {
                Errors = pagingValidationException.ValidationMessages,
                PageNumber = DefaultPageNumber,
                PageSize = DefaultPageSize
            };

            var actionInfo = new ActionInfo
            {
                UrlHelper = urlHelper,
                RouteName = context.ActionDescriptor.AttributeRouteInfo.Name,
                HttpMethod = context.HttpContext.Request.Method
            };

            errorResponse.GenerateLinks(pageStringResources, actionInfo);
            return errorResponse;
        }

        private static BaseHateoasResponse ProcessValidationException(ValidationException pagingValidationException)
        {
            var errorResponse = new BaseHateoasResponse
            {
                Errors = pagingValidationException.ValidationMessages,
            };

            return errorResponse;
        }

        private static BaseHateoasResponse ProcessSqlException(StringResources stringResources)
        {
            return new BaseHateoasResponse { Errors = new List<string> { stringResources.SomethingWentWrong }, };
        }

        private static BaseHateoasResponse ProcessException(Exception exception)
        {
            return new BaseHateoasResponse { Errors = new List<string> { exception.Message }, };
        }
    }
}
