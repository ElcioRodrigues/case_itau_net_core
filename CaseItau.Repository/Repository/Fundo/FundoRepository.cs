using CaseItau.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CaseItau.Infra.Data.Repository
{
    public class FundoRepository : RepositoryGeneric<Fundo>, IFundoRepository
    {
        public FundoRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<IList<Fundo>> GetAllAsync()
          => await _dbSet.Include(x => x.Tipo).OrderBy(x => x.Codigo).ToListAsync();

        public override async Task<Fundo> FindAsync(Expression<Func<Fundo, bool>> filter)
        {
            var fundo = await _dbSet
                .Include(x => x.Tipo)
                .Where(filter)
                .FirstOrDefaultAsync();

            return fundo;
        }
    }
}
