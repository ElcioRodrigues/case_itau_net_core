using CaseItau.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repository
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(object keyValue);

        Task DeleteAsync(TEntity entity);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order);

        Task<IEnumerable<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Get the DbSet as a IQueryable
        /// </summary>
        IQueryable<TEntity> Get();


    }
}
