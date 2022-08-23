﻿using MediatR;

namespace CaseItau.API.Service.Fundo.Commands.Create
{
    public class FundoCreateCommandRequest : IRequest<FundoCreateCommandResponse>
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public decimal? Patrimonio { get; set; }
    }
}