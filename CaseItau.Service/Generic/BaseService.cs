using CaseItau.Domain.Common;
using CaseItau.Infra.Data.Repository;
using CaseItau.Infra.Data.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Service.Generic
{
    public class BaseService <TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IRepositoryGeneric<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IRepositoryGeneric<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter) => await _repository.AnyAsync(filter);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await _repository.CountAsync(filter);

        public virtual async Task<TEntity?> FindAsync(object keyValue) => await _repository.FindAsync(keyValue);


        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.FindAsync(filter);

        public virtual async Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage)
            => await _repository.GetAsync(filter, order, page, itemsPerPage);

        public async Task<PagedList<TEntity>> FormatPagedListAsync(IQueryable<TEntity> query, int page, int itemsPerPage)
        {
            var total = await query.CountAsync();
            var skip = (page - 1) * itemsPerPage;
            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = total,
                Items = await query.Skip(skip).Take(itemsPerPage).ToListAsync()
            };
        }

        public async Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage)
          => await _repository.GetAsync(order, page, itemsPerPage);

        public async Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order)
        => await _repository.GetAsync(filter, order);

        public async Task<IEnumerable<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order)
        => await _repository.GetAsync(order);

        public virtual async Task<Result> InsertAsync(TEntity entity)
        {
            await _repository.InsertAsync(entity);

            return new Result();
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);

            return new Result();
        }

        public virtual async Task<Result> DeleteAsync(object keyValue)
        {
            await _repository.DeleteAsync(keyValue);
            return new Result();
        }
    }
}
