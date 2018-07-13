//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ValidationStringResources.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ValidationStringResources type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api.Resources
{
    using System;

    public class ValidationStringResources
    {
        public string ParameterMustBePositiveTemplate { get; set; }

        public string ParameterIsNullTemplate { get; set; }

        public string GetParameterMustBePositiveString(string parameterName)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            return string.Format(this.ParameterMustBePositiveTemplate, parameterName);
        }

        public string GetParameterIsNullString(string parameterName)
        {
            if (parameterName == null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            return string.Format(this.ParameterIsNullTemplate, parameterName);
        }
    }
}