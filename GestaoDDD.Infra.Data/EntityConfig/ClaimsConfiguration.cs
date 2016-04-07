using GestaoDDD.Domain.Entities.Identity;
using System.Data.Entity.ModelConfiguration;

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

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

            ToTable("Claims");
        }
    }
}
