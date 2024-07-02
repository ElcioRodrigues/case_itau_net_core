using CaseItau.Domain.Common;
using System.Linq.Expressions;

namespace CaseItau.Service.Generic
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity?> FindAsync(object keyValue);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        Task<Result> InsertAsync(TEntity entity);

        Task<Result> UpdateAsync(TEntity entity);

        Task<Result> DeleteAsync(object keyValue);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage);

        Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order);

        Task<IEnumerable<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);

        
    }
}
