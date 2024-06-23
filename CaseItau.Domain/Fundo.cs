using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain
{
    [Table("FUNDO")]
    public class Fundo
    {
        [Column("CODIGO"), MaxLength(20), Required]
        public required string Codigo { get; set; }
        [Column("NOME"), MaxLength(100), Required]
        public required string Nome { get; set; }
        [Column("CNPJ"), MaxLength(14), Required]
        public required string Cnpj { get; set; }
        [Column("CODIGO_TIPO")]
        public int CodigoTipo { get; set; }
        [Column("PATRIMONIO")]
        public decimal? Patrimonio { get; set; }

        public TipoFundo Tipo { get; set; }
    }
}
