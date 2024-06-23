using CaseItau.Domain;
using CaseItau.Infra.Data.Repository;
using CaseItau.Infra.Data.UoW;
using CaseItau.Service.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Service
{
    public class TipoFundoService : BaseService<TipoFundo>, ITipoFundoService
    {
        private readonly IConfiguration _configuration;

        public TipoFundoService(ITipoFundoRepository tipoFundoRepository,
                    IUnitOfWork unitOfWork,
                    IConfiguration configuration)
                    : base(tipoFundoRepository, unitOfWork)
        {
            _configuration = configuration;
        }

        public override async Task<TipoFundo?> FindAsync(object keyValue)
        {
            var tipoFundo = await _repository.Get()
                .Where(b => b.Codigo == (int)keyValue)
                .FirstOrDefaultAsync();

            return tipoFundo;
        }

        public async Task<IList<TipoFundo>> GetAllAsync()
         => await _repository.Get().OrderBy(x => x.Codigo).ToListAsync();
    }
}
