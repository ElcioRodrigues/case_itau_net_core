using CaseItau.Domain;
using CaseItau.Domain.Common;
using CaseItau.Service.Generic;

namespace CaseItau.Service
{
    public interface IFundoService : IBaseService<Fundo>
    {
        Task<Result> MoveAssetsAsync(object keyValue, decimal value);
        Task<IList<Fundo>> GetAllAsync();
    }
}
