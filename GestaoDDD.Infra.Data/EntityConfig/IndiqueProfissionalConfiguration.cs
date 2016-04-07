using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class IndiqueProfissionalConfiguration : EntityTypeConfiguration<IndiqueProfissional>
    {
        public IndiqueProfissionalConfiguration()
        {
            HasKey(s => s.Id);
            Property(c => c.Id).HasColumnName("Id");
           
            Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(256).HasColumnName("nome_solicitante");

            Property(s => s.Nome_Profissional)
                .IsRequired()
                .HasMaxLength(256).HasColumnName("nome_profissional");

            Property(s => s.Telefone)
                .IsRequired()
                .HasMaxLength(20).HasColumnName("telefone");

            Property(s => s.Estado).HasColumnName("estado");

            Property(s => s.Email_Solicitante)
                .IsRequired()
                .HasMaxLength(128).HasColumnName("email_solicitante");

            Property(s => s.Email_Empresa)
                .HasMaxLength(128).HasColumnName("email_empresa");

            Property(s => s.Cidade)
                .IsRequired()
                .HasMaxLength(128).HasColumnName("cidade");

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

        }
    }
}
