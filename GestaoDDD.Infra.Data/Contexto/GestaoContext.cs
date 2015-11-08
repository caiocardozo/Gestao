using System.Data.Entity;
using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using GestaoDDD.Infra.Data.EntityConfig;

namespace GestaoDDD.Infra.Data.Contexto
{
    public class GestaoContext : DbContext
    {
        public GestaoContext()
            :base("Connection")
        {

        }

        #region categoria
        public DbSet<Categoria> Categoria { get; set; }
        #endregion
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(s => s.Name == s.ReflectedType.Name + "Id"
                || s.Name == s.ReflectedType.Name + "ID")
                .Configure(s => s.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(500));


            #region ArquivoConfiguracao
            modelBuilder.Configurations.Add(new CategoriaConfiguration());

            #endregion
        }
    }
}
