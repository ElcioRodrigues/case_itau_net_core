using CaseItau.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repository
{
    public class TipoFundoRepository : RepositoryGeneric<TipoFundo>, ITipoFundoRepository
    {
        public TipoFundoRepository(ApplicationDbContext context) : base(context) { }
    }
}
