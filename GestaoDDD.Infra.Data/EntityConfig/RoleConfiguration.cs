using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id").HasColumnOrder(1);

            Property(c => c.Name).HasColumnName("Name").HasColumnOrder(4);

            Property(c => c.data_alteracao).HasColumnName("data_alteracao").HasColumnOrder(3);
            Property(c => c.data_inclusao).HasColumnName("data_inclusao").HasColumnOrder(2);

            ToTable("AspNetRoles");
        }
    }
}
