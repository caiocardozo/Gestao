using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ComoFuncionaConfiguration : EntityTypeConfiguration<ComoFunciona>
    {
        public ComoFuncionaConfiguration()
        {
            HasKey(c => c.cf_Id);

            Property(c => c.cf_Informacao)
                .HasMaxLength(500)
                .IsRequired();

            Property(c => c.cf_Ordem)
                .IsRequired();
        }
    }
}
