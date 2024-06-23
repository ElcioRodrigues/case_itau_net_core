using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain
{
    [Table("TIPO_FUNDO")]
    public class TipoFundo
    {
        [Column("CODIGO"), Required]
        public int Codigo { get; set; }

        [Column("NOME"), MaxLength(20), Required]
        public required string Nome { get; set; }

        public virtual ICollection<Fundo> Fundos { get; set; }
    }
}
