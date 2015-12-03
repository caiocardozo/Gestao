using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class OrcamentoConfiguration : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoConfiguration()
        {
            HasKey(c => c.orc_Id);
        }
    }
}
