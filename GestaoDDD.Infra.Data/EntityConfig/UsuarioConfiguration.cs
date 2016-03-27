using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.Data_Alteracao);

            Property(c => c.Data_Inclusao);

            ToTable("AspNetUsers");


            // MAPEAMENTO DE UM PARA UM
            HasRequired(p => p.Pessoa)
                .WithRequiredPrincipal(p => p.Usuario);
        }
    }
}
