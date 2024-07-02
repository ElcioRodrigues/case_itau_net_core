using CaseItau.Domain;
using CaseItau.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }

        public DbSet<TipoFundo> TiposDeFundo { get; set; }
        public DbSet<Fundo> Fundos { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = new TipoFundoMap(modelBuilder.Entity<TipoFundo>());
            _ = new FundoMap(modelBuilder.Entity<Fundo>());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Pode ser implementado o log das tabelas
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
