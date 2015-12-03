using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class PrestadorConfiguration : EntityTypeConfiguration<Prestador>
    {
        public PrestadorConfiguration()
        {
            HasKey(c => c.pres_Id);
        }
    }
}
