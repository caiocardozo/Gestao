using System.Data.Entity.ModelConfiguration;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ServicoPrestadorConfiguration : EntityTypeConfiguration<ServicoPrestador>
    {
        public ServicoPrestadorConfiguration()
        {
            HasKey(p => p.serv_Pres_Id);

            Property(c => c.serv_Id)
                .IsRequired();

            Property(u => u.pres_Id)
               .IsRequired()
               .HasMaxLength(128);

            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

            //mapeia o relacionamento 1 to N Serviço
            HasRequired(t => t.Servico)//determina que Categoria é obrigatorio em serviço
           .WithMany(t => t.ServicoPrestador)
           .HasForeignKey(t => t.serv_Id);//chave estangeira de servico   

            //mapeia o relacionamento 1 to N Prestador
            HasRequired(t => t.Prestador)//determina que Prestador é obrigatorio em serviço
           .WithMany(t => t.ServicoPrestador)
           .HasForeignKey(t => t.pres_Id);//chave estangeira de prestado   
        }
    }
}
