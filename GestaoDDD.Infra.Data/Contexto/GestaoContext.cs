using System.Data.Entity;
using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using GestaoDDD.Infra.Data.EntityConfig;

namespace GestaoDDD.Infra.Data.Contexto
{
    public class GestaoContext : DbContext, IGestaoContext
    {
        public GestaoContext()
            : base("ConnectionLocalCaio")
        {
        }

        #region objetos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Prestador> Prestador { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<ServicoPrestador> ServicoPrestador { get; set; }

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
            modelBuilder.Configurations.Add(new OrcamentoConfiguration());
            modelBuilder.Configurations.Add(new PrestadorConfiguration());
            modelBuilder.Configurations.Add(new ServicoConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new ServicoPrestadorConfiguration());
            #endregion
        }
    }
}
