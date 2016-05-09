using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class CategoriaConfiguration : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfiguration()
        {
            HasKey(c => c.cat_Id);
            Property(c => c.cat_Id).HasColumnName("Id").HasColumnOrder(1);
                
            Property(c => c.cat_Nome)
                .IsRequired()
                .HasMaxLength(200).HasColumnName("nome").HasColumnOrder(4);

            Property(c => c.data_alteracao).HasColumnName("data_alteracao").HasColumnOrder(3);

            Property(c => c.data_inclusao).HasColumnName("data_inclusao").HasColumnOrder(2);

        }
    }
}
