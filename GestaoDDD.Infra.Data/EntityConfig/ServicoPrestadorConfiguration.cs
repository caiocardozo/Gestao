using System.Data.Entity.ModelConfiguration;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ServicoPrestadorConfiguration : EntityTypeConfiguration<ServicoPrestador>
    {
        public ServicoPrestadorConfiguration()
        {
            HasKey(p => p.serv_Pres_Id);
            Property(c => c.serv_Pres_Id).HasColumnName("Id");

           
            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

        }
    }
}
