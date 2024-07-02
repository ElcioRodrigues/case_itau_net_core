using CaseItau.Domain;

namespace CaseItau.Infra.Data.Repository
{
    public interface IFundoRepository : IRepositoryGeneric<Fundo>
    {
        Task<IList<Fundo>> GetAllAsync();
    }
}
