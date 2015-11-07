using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntitiesConfig
{
    public class CategoriaConfig : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfig()
        {
            HasKey(c => c.Id_pk);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
