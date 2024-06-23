using CaseItau.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Mapping
{
    public class TipoFundoMap
    {
        public TipoFundoMap(EntityTypeBuilder<TipoFundo> entityBuilder)
        {

            entityBuilder.HasKey(t => new { t.Codigo });

        }
    }
}
