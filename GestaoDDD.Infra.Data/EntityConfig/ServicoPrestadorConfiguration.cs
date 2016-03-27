using System.Data.Entity.ModelConfiguration;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ServicoPrestadorConfiguration : EntityTypeConfiguration<ServicoPrestador>
    {
        public ServicoPrestadorConfiguration()
        {
            HasKey(p => p.serv_Pres_Id);

            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);

        }
    }
}
