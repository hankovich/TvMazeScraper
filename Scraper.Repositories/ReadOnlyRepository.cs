//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ReadOnlyRepository.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ReadOnlyRepository type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Scraper.Orm.Models;
    using Scraper.Repositories.Interface;

    public class ReadOnlyRepository<TEntity> : IPagingReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext context;

        public ReadOnlyRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            IEnumerable<TEntity> entities = await this.context.Set<TEntity>().ToListAsync();
            return entities;
        }

        public Task<TEntity> GetMinAsync()
        {
            return this.context.Set<TEntity>().MinAsync();
        }

        public Task<TEntity> GetMaxAsync()
        {
            return this.context.Set<TEntity>().MaxAsync();
        }

        public Task<TResult> GetMinAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return this.context.Set<TEntity>().MinAsync(selector);
        }

        public Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return this.context.Set<TEntity>().MaxAsync(selector);
        }

        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return this.context.Set<TEntity>().FirstAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            List<TEntity> page = await this.context.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return page;
        }

        public async Task<IEnumerable<TResult>> GetPageAsync<TResult>(
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, TResult>> converter)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber));
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IEnumerable<TResult> page = await this.context.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(converter).ToListAsync();

            return page;
        }

        public Task<int> GetTotalCountAsync()
        {
            return this.context.Set<TEntity>().CountAsync();
        }
    }
}