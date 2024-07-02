using CaseItau.Domain;

namespace CaseItau.Infra.Data.Repository
{
    public class TipoFundoRepository : RepositoryGeneric<TipoFundo>, ITipoFundoRepository
    {
        public TipoFundoRepository(ApplicationDbContext context) : base(context) { }

    }
}
