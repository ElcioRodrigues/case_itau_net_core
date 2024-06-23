using CaseItau.Domain;

namespace CaseItau.API.Model
{
    public class FundoVM
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public decimal? Patrimonio { get; set; }
        public TipoFundoVM Tipo { get; set; }
    }
}
