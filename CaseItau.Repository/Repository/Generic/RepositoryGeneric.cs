using CaseItau.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repository
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryGeneric(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(params object[] keyValues) => await FindAsync(keyValues);


        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = _dbSet.AsQueryable();

            query = query.Where(filter);

            var count = await query.CountAsync();
            if (count > 1)
                throw new ApplicationException("Mais de uma entidade foi encontrada com o filtro informado");

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<PagedList<TEntity>> GetAsync<TKey>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> order,
            int page,
            int itemsPerPage)
        {
            var skip = (page - 1) * itemsPerPage;
            var query = _dbSet.AsQueryable();

            query = query.Where(filter);
            var total = await query.CountAsync();
            var result = await query
                .OrderBy(order)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = total,
                Items = result
            };
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TKey>(
            Expression<Func<TEntity, TKey>> order)
        {
            var query = _dbSet.AsQueryable();

            return  await query
                .OrderBy(order)
                .ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage)
            => await GetAsync(x => true, order, page, itemsPerPage);

        public async Task<PagedList<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order)
            => await GetAsync(filter, order);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.CountAsync(filter);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter) => await CountAsync(filter) > 0;

        public IQueryable<TEntity> Get() => _dbSet;

        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                Exception db = HandleDbUpdateException(ex);
                throw db;
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbUpdate)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes.");
            try
            {
                builder.AppendLine($"Message: {dbUpdate.InnerException.Message}");
                foreach (var entries in dbUpdate.Entries)
                    builder.AppendLine($"Entity of type {entries.Entity.GetType().Name} in state {entries.State} could not be updated");
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }
            string message = builder.ToString();
            return new Exception(message, dbUpdate);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(object keyValue)
        {
            var entity = await FindAsync(keyValue);
            if (entity == null)
                throw new KeyNotFoundException("Não existe esse registro");
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
