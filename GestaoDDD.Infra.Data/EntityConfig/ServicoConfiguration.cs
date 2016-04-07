using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GestaoDDD.Infra.Data.EntityConfig
{
    public class ServicoConfiguration : EntityTypeConfiguration<Servico>
    {
        public ServicoConfiguration()
        {
            HasKey(c => c.serv_Id);
            Property(c => c.serv_Id).HasColumnName("Id");

            Property(c => c.serv_Nome)
                .HasMaxLength(100)
                .IsRequired().HasColumnName("nome");


            Property(c => c.data_alteracao);

            Property(c => c.data_inclusao);

            //mapeia o relacionamento 1 to N Categoria
            HasRequired(t => t.Categoria)//determina que Categoria é obrigatorio em serviço
           .WithMany(t => t.Servico)//uma categoria tem varios serviços
           .HasForeignKey(t => t.cat_Id);//chave estangeira de categoria   

            Property(c => c.cat_Id).HasColumnName("categoria_id");


        }
    }
}
