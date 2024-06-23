using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context) => _context = context;

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        public async Task CommitAsync() => await _context.Database.CommitTransactionAsync();
        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();
        public async ValueTask DisposeAsync()
        {
            if (_context?.Database?.CurrentTransaction != null)
                await _context.Database.CurrentTransaction.RollbackAsync();
        }
    }
}
