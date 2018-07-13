//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidationException.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ValidationException type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ValidationException : Exception
    {
        public ValidationException(IList<string> validationMessages)
            : base(string.Join(Environment.NewLine, validationMessages))
        {
            this.ValidationMessages = validationMessages;
        }

        public ValidationException(IList<string> validationMessages, Exception innerException)
            : base(string.Join(Environment.NewLine, validationMessages), innerException)
        {
        }

        public IList<string> ValidationMessages { get; }
    }
}
