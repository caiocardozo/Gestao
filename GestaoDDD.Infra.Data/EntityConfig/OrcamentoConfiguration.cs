using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class OrcamentoConfiguration : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoConfiguration()
        {
            HasKey(c => c.orc_Id);

            Property(c => c.orc_descricao)
                .IsRequired()
                .HasMaxLength(500);

            Property(c => c.orc_endereco)
                .IsRequired();

            Property(c => c.orc_prazo)
                .IsRequired();

            Property(c => c.orc_nome_solicitante)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.orc_email_solicitante)
                .HasMaxLength(200)
                .IsRequired();

            Property(c => c.orc_telefone_solicitante)
                .IsRequired();

            Property(c => c.orc_endereco_solicitante)
                .HasMaxLength(200);

            HasRequired(c => c.categoria_id)
                .WithRequiredPrincipal(c => c.Orcamento);

            HasRequired(c => c.servico_id)
                .WithRequiredDependent(c => c.Orcamento);

            Property(c => c.orc_latitude);

            Property(c => c.orc_longitude);

            Property(c => c.data_Alteracao);

            Property(c => c.data_Inclusao);


        }
    }
}
