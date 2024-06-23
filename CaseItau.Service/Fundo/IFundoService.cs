using CaseItau.Domain;
using CaseItau.Domain.Common;
using CaseItau.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Service
{
    public interface IFundoService : IBaseService<Fundo>
    {
        Task<Result> MoveAssetsAsync(object keyValue, decimal value);
        Task<IList<Fundo>> GetAllAsync();
    }
}
