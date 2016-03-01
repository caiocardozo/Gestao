using System.Data.Entity.ModelConfiguration;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class PessoaConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfiguration()
        {
            HasKey(p => p.usu_id);

            Property(p => p.usu_id)
                .HasMaxLength(128);

            Property(p => p.pes_nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(p => p.pes_cpf)
                .IsRequired()
                .HasMaxLength(11);

            Property(p => p.pes_rg)
                .HasMaxLength(9);

            Property(p => p.pes_endereco)
                .HasMaxLength(200);

            Property(p => p.pes_numero);

            Property(p => p.pes_bairro)
                .HasMaxLength(100);

            Property(p => p.pes_cidade)
                .HasMaxLength(100);

            Property(p => p.pes_cep)
                .HasMaxLength(9);

        }
    }
}
