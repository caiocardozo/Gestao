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
           
            Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(256);

            Property(s => s.Nome_Profissional)
                .IsRequired()
                .HasMaxLength(256);

            Property(s => s.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            Property(s => s.Estado);

            Property(s => s.Email_Solicitante)
                .IsRequired()
                .HasMaxLength(128);

            Property(s => s.Email_Empresa)
                .HasMaxLength(128);

            Property(s => s.Cidade)
                .IsRequired()
                .HasMaxLength(128);

            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);

        }
    }
}
