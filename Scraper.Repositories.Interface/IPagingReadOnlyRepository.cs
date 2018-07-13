//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IPagingReadOnlyRepository.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IPagingReadOnlyRepository type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Repositories.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Scraper.Orm.Models;

    public interface IPagingReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize);

        Task<IEnumerable<TResult>> GetPageAsync<TResult>(int pageNumber, int pageSize, Expression<Func<TEntity, TResult>> converter);
    }
}