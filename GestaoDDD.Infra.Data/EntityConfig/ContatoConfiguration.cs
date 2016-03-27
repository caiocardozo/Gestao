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

            Property(c => c.ctt_email)
                .IsRequired();

            Property(c => c.ctt_msg)
                .IsRequired()
                .HasMaxLength(500);

            Property(c => c.ctt_telefone)
                .IsRequired();

            Property(c => c.ctt_nome)
                .IsRequired()
                .HasMaxLength(150);


            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);


        }
    }
}
