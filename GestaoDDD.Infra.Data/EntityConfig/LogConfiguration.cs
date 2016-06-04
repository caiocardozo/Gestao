using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");

            Property(c => c.Mensagem).HasColumnName("Mensagem");
            Property(c => c.data_alteracao).HasColumnName("data_alteracao");
            Property(c => c.data_inclusao).HasColumnName("data_inclusao");
        }
    }
}
