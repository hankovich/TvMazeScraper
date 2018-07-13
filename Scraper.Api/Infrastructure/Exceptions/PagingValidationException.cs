//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PagingValidationException.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the PagingValidationException type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class PagingValidationException : ValidationException
    {
        public PagingValidationException(IList<string> validationMessages)
            : base(validationMessages)
        {
        }

        public PagingValidationException(IList<string> validationMessages, Exception innerException)
            : base(validationMessages, innerException)
        {
        }
    }
}