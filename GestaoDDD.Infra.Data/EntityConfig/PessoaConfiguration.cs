using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Services;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class PessoaConfiguration : EntityTypeConfiguration<Pessoa>
    {
        public PessoaConfiguration()
        {
            HasKey(p => p.usu_id);

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

            //HasRequired(p => p.Pessoa)
            //   .WithRequiredPrincipal(p => p.Usuario);

            //HasRequired(p => p.Usuario).WithRequiredPrincipal(s => s.Pessoa); Wagner 
            

            
        }
    }
}
