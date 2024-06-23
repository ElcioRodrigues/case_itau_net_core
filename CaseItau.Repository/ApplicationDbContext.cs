using CaseItau.Domain;
using CaseItau.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

            new TipoFundoMap(modelBuilder.Entity<TipoFundo>());
            new FundoMap(modelBuilder.Entity<Fundo>());

            //O Contexto procura pelas classes que implementam IEntityTypeConfiguration adicionando o mapeamento de forma automática.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Pode ser implementado o log das tabelas
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
