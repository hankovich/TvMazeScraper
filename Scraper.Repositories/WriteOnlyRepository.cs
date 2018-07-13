//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WriteOnlyRepository.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the WriteOnlyRepository type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Scraper.Orm.Models;
    using Scraper.Repositories.Interface;

    public class WriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity>
        where TEntity : class, IEntity 
    {
        private readonly DbContext context;

        public WriteOnlyRepository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            this.context.Set<TEntity>().AddRange(entities);

            return this.context.SaveChangesAsync();
        }

        public Task DeleteByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            IQueryable<TEntity> entitiesToDelete = this.context.Set<TEntity>().Where(entity => ids.Contains(entity.Id));
            this.context.Set<TEntity>().RemoveRange(entitiesToDelete);

            return this.context.SaveChangesAsync();
        }
    }
}
