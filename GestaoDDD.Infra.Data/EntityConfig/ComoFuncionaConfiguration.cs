using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

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

            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);

        }
    }
}
