using GestaoDDD.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ClaimsConfiguration : EntityTypeConfiguration<Claims>
    {
        public ClaimsConfiguration()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired();

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(128);

            ToTable("AspNetClaims");
        }
    }
}
