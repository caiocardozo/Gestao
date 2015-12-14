using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ServicoConfiguration : EntityTypeConfiguration<Servico>
    {
        public ServicoConfiguration()
        {
            HasKey(c => c.serv_Id);

            Property(c => c.serv_Nome)
                .HasMaxLength(100)
                .IsRequired();


            Property(c => c.data_Alteracao);

            Property(c => c.data_Inclusao);

           }
    }
}
