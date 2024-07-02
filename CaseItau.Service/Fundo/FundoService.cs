using CaseItau.Domain;
using CaseItau.Domain.Common;
using CaseItau.Infra.Data.Repository;
using CaseItau.Infra.Data.UoW;
using CaseItau.Service.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CaseItau.Service
{
    public class FundoService : BaseService<Fundo>, IFundoService
    {
        private readonly IConfiguration _configuration;
        private readonly IFundoRepository _fundoRepository;
        private readonly ITipoFundoService _tipoFundoService;

        public FundoService(IFundoRepository fundoRepository,
            ITipoFundoService tipoFundoService,
                    IUnitOfWork unitOfWork,
                    IConfiguration configuration)
                    : base(fundoRepository, unitOfWork)
        {
            _configuration = configuration;
            _fundoRepository = fundoRepository;
            _tipoFundoService = tipoFundoService;
        }

        public async Task<IList<Fundo>> GetAllAsync()
          => await _fundoRepository.GetAllAsync();

        public async Task<Result> MoveAssetsAsync(object keyValue, decimal value)
        {
            var result = new Result();
            var fundo = await _fundoRepository.FindAsync(x => x.Codigo == (string)keyValue);
            if (fundo == null)
            {
                result.Messages.Add("Registro não existe!");
                return result;
            }

            if (!fundo.Patrimonio.HasValue)
            {
                fundo.Patrimonio = decimal.Zero;
            }

            fundo.Patrimonio = value;
            await _repository.UpdateAsync(fundo);
            result.SuccessMessage = "Patrimonio alterado com sucesso!";
            return result;
        }

        public override async Task<Result> InsertAsync(Fundo entity)
        {
            var result = await ValidateEntity(entity);
            if (!result.Success)
                return result;

            var fundo = await _fundoRepository.FindAsync(x => x.Codigo == entity.Codigo);
            if (fundo != null)
            {
                result.Messages.Add("Já existe um fundo de investimento com esse código!");
                return result;
            }

            await _fundoRepository.InsertAsync(entity);
            result.SuccessMessage = "Fundo cadastrado com sucesso!";
            return result;
        }

        private async Task<Result> ValidateEntity(Fundo entity)
        {
            var result = new Result();
            if (string.IsNullOrWhiteSpace(entity.Codigo))
            {
                result.Messages.Add("O campo <Codigo> é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(entity.Nome))
            {
                result.Messages.Add("O campo <Nome> é obrigatório!");
            }
            if (string.IsNullOrWhiteSpace(entity.Cnpj))
            {
                result.Messages.Add("O campo <Cnpj> é obrigatório!");
            }
            var fundoTipo = await _tipoFundoService.FindAsync(x => x.Codigo == entity.CodigoTipo);
            if (fundoTipo == null)
            {
                result.Messages.Add("O campo <CodigoTipo> informado é inválido!");
            }

            var fundo = await _fundoRepository.Get()
               .Where(b => b.Cnpj == entity.Cnpj)
               .FirstOrDefaultAsync();

            if (fundo != null && fundo.Codigo != entity.Codigo)
            {
                result.Messages.Add("O Cpnj informado pertence à outro cadastro!");
            }

            return result;
        }

        public override async Task<Result> UpdateAsync(Fundo entity)
        {
            var result = await ValidateEntity(entity);
            if (!result.Success)
                return result;

            var fundo = await _fundoRepository.FindAsync(x => x.Codigo == entity.Codigo);
            if (fundo == null)
            {
                result.Messages.Add("Registro não existe!");
                return result;
            }

            fundo.Cnpj = entity.Cnpj;
            fundo.Patrimonio = entity.Patrimonio;
            fundo.Nome = entity.Nome;
            fundo.CodigoTipo = entity.CodigoTipo;

            await _fundoRepository.UpdateAsync(fundo);
            result.SuccessMessage = "Fundo cadastrado com sucesso!";
            return result;
        }

        public override async Task<Result> DeleteAsync(object keyValue)
        {
            var result = new Result();
            var fundo = await _fundoRepository.FindAsync(x => x.Codigo == (string)keyValue);
            if (fundo == null)
            {
                result.Messages.Add("Registro não existe!");
                return result;
            }
            await _fundoRepository.DeleteAsync(fundo);
            return result;
        }

    }
}
