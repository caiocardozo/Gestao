using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ContatoConfiguration : EntityTypeConfiguration<Contato>
    {
        public ContatoConfiguration()
        {
            HasKey(c => c.contato_Id);

            Property(c => c.contato_Id).HasColumnName("Id");

            Property(c => c.ctt_email)
                .IsRequired().HasColumnName("email");

            Property(c => c.ctt_msg)
                .IsRequired()
                .HasMaxLength(500).HasColumnName("mensagem");

            Property(c => c.ctt_telefone)
                .IsRequired().HasColumnName("telefone");

            Property(c => c.ctt_nome)
                .IsRequired()
                .HasMaxLength(150).HasColumnName("nome");

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

        }
    }
}
