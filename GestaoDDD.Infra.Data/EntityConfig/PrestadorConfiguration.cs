using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class PrestadorConfiguration : EntityTypeConfiguration<Prestador>
    {
        public PrestadorConfiguration()
        {
            HasKey(c => c.pres_Id);

            Property(u => u.pres_Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasMaxLength(128);
            
            Property(c => c.pres_Nome)
                .IsRequired()
                .HasMaxLength(120).HasColumnName("nome");

            Property(c => c.pres_Email)
                .HasMaxLength(100).HasColumnName("email");

            Property(c => c.pres_Endereco)
                .HasMaxLength(200).HasColumnName("endereco");

            Property(c => c.pres_Raio_Recebimento).HasColumnName("raio");

            Property(c => c.pres_Cpf_Cnpj)
                .HasMaxLength(14)
                .IsRequired().HasColumnName("cpf_cnpj");

            Property(c => c.pres_Telefone_Celular)
                .HasMaxLength(14).HasColumnName("celular");

            Property(c => c.pres_Telefone_Residencial)
               .HasMaxLength(14).HasColumnName("telefone_fixo");

            Property(c => c.status).HasColumnName("status");

           // Property(c => c.UsuarioId).HasColumnName("usuario_id");
        }
    }
}
