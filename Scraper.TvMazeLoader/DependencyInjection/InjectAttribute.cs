//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InjectAttribute.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the InjectAttribute type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.DependencyInjection
{
    using System;

    using Microsoft.Azure.WebJobs.Description;

    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}