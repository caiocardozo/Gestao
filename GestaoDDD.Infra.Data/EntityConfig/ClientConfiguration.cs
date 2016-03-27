using GestaoDDD.Domain.Entities.Identity;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired();

            Property(u => u.ClientKey);

            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);

            ToTable("AspNetClients");

        }
    }
}
