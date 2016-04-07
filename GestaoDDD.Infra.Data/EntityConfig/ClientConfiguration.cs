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

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

            ToTable("AspNetClients");

        }
    }
}
