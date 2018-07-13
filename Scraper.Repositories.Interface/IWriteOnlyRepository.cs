//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IWriteOnlyRepository.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the IWriteOnlyRepository type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Repositories.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Scraper.Orm.Models;

    public interface IWriteOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteByIdsAsync(IEnumerable<int> ids);
    }
}