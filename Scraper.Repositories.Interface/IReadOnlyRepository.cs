//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IReadOnlyRepository.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IReadOnlyRepository type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Repositories.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Scraper.Orm.Models;

    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetMinAsync();

        Task<TEntity> GetMaxAsync();

        Task<TResult> GetMinAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetTotalCountAsync();
    }
}