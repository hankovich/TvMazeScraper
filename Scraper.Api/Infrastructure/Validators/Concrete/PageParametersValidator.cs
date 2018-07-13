//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PageParametersValidator.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the PageParametersValidator type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Validators.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Scraper.Api.Infrastructure.Exceptions;
    using Scraper.Api.Infrastructure.Validators.Abstract;
    using Scraper.Api.Models;
    using Scraper.Api.Resources;

    public class PageParametersValidator : IPageParametersValidator
    {
        private readonly ValidationStringResources validationStringResources;

        public PageParametersValidator(ValidationStringResources validationStringResources)
        {
            this.validationStringResources = validationStringResources ?? throw new ArgumentNullException(nameof(validationStringResources));
        }

        public void EnsureValid(PagingParameters parameters)
        {
            if (parameters == null)
            {
                var validationMessage = this.validationStringResources.GetParameterIsNullString(nameof(parameters));

                throw new ValidationException(new List<string> { validationMessage });
            }

            IList<string> validationMessages = new List<string>();

            if (parameters.PageNumber <= 0)
            {
                validationMessages.Add(this.validationStringResources.GetParameterMustBePositiveString(nameof(parameters.PageNumber)));
            }

            if (parameters.PageSize <= 0)
            {
                validationMessages.Add(this.validationStringResources.GetParameterMustBePositiveString(nameof(parameters.PageSize)));
            }

            if (validationMessages.Any())
            {
                throw new PagingValidationException(validationMessages);
            }
        }
    }
}
