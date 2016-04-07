using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class CategoriaConfiguration : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfiguration()
        {
            HasKey(c => c.cat_Id);
            Property(c => c.cat_Id).HasColumnName("Id");
                
            Property(c => c.cat_Nome)
                .IsRequired()
                .HasMaxLength(200).HasColumnName("nome");

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);
        }
    }
}
