using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class PrestadorConfiguration : EntityTypeConfiguration<Prestador>
    {
        public PrestadorConfiguration()
        {
            HasKey(c => c.pres_Id);

            Property(c => c.pres_Nome)
                .IsRequired()
                .HasMaxLength(120);

            Property(c => c.pres_Email)
                .HasMaxLength(100);

            Property(c => c.pres_Endereco)
                .HasMaxLength(200);

            Property(c => c.pres_Raio_Recebimento);

            Property(c => c.pres_Cpf_Cnpj)
                .HasMaxLength(14)
                .IsRequired();

            Property(c => c.pres_Telefone_Celular)
                .HasMaxLength(14);

            Property(c => c.pres_Telefone_Residencial)
               .HasMaxLength(14);

            Property(c => c.status);




        }
    }
}
