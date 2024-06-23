using CaseItau.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Mapping
{
    public class FundoMap
    {
        public FundoMap(EntityTypeBuilder<Fundo> entityBuilder) {

            entityBuilder.HasKey(t => new { t.Codigo, t.CodigoTipo });
            entityBuilder.HasOne(t => t.Tipo).WithMany(t => t.Fundos).HasForeignKey(t => t.CodigoTipo);

        }
    }
}
