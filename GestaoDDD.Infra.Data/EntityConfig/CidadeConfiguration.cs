using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    class CidadeConfiguration : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnOrder(1);
            Property(c => c.Codigo).HasColumnName("codigo").HasColumnOrder(2);
            Property(c => c.data_inclusao).HasColumnName("data_inclusao").HasColumnOrder(3);
            Property(c => c.data_alteracao).HasColumnName("data_alteracao").HasColumnOrder(4);
            Property(c => c.Situacao).HasColumnName("situacao").HasColumnOrder(5);
            Property(c => c.CodigoCidade).HasColumnName("codigo_cidade").HasColumnOrder(6);
            Property(c => c.NomeCidade).HasColumnName("nome_cidade").HasColumnOrder(7).HasMaxLength(200);
            Property(c => c.CodigoUf).HasColumnName("codigo_uf").HasColumnOrder(8);
            Property(c => c.UF).HasColumnName("uf").HasColumnOrder(9);
            Property(c => c.CodigoPais).HasColumnName("codigo_pais").HasColumnOrder(10);
            Property(c => c.Pais).HasColumnName("pais").HasColumnOrder(11);
            


        }
    }
}
