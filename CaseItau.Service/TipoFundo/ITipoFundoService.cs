using CaseItau.Domain;
using CaseItau.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Service
{
    public interface ITipoFundoService : IBaseService<TipoFundo>
    {
        Task<IList<TipoFundo>> GetAllAsync();
    }
}
