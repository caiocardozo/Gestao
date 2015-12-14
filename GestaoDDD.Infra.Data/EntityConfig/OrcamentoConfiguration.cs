using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class OrcamentoConfiguration : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoConfiguration()
        {
            HasKey(c => c.orc_Id);

            Property(c => c.orc_Descricao)
                .HasMaxLength(200)
                .IsRequired();

            Property(c => c.orc_Dias_Prazo);

            Property(c => c.orc_Endereco_Servico)
                .HasMaxLength(200);

            Property(c => c.orc_Frequencia_Prazo);

            Property(c => c.data_Alteracao);

            Property(c => c.data_Inclusao);
        }
    }
}
