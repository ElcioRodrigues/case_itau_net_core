namespace CaseItau.Web.Models
{
    public class FundoVM
    {
        public required string Codigo { get; set; }
        public required string Nome { get; set; }
        public required string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public decimal? Patrimonio { get; set; }
        public TipoFundoVM Tipo { get; set; }
    }
}
