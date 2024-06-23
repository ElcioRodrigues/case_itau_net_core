using CaseItau.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repository
{
    public class FundoRepository : RepositoryGeneric<Fundo>, IFundoRepository
    {
        public FundoRepository(ApplicationDbContext context) : base(context) { }
    }
}
