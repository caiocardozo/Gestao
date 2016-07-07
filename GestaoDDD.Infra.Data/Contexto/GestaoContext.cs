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
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        #region objetos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Prestador> Prestador { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<ServicoPrestador> ServicoPrestador { get; set; }
        public DbSet<ComoFunciona> ComoFunciona { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<IndiqueProfissional> IndiqueProfissional { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Log> Log { get; set; }

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
            modelBuilder.Configurations.Add(new PessoaConfiguration());
            modelBuilder.Configurations.Add(new ComoFuncionaConfiguration());
            modelBuilder.Configurations.Add(new IndiqueProfissionalConfiguration());
            modelBuilder.Configurations.Add(new ContatoConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new CidadeConfiguration());

            #endregion
        }
    }
}
