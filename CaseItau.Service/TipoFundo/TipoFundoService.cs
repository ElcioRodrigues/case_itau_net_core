using CaseItau.Domain;
using CaseItau.Infra.Data.Repository;
using CaseItau.Infra.Data.UoW;
using CaseItau.Service.Generic;
using Microsoft.Extensions.Configuration;

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

        
    }
}
