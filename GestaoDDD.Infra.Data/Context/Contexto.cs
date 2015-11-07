using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using GestaoDDD.Infra.Data.EntitiesConfig;

namespace GestaoDDD.Infra.Data.Context
{
    public class Contexto : DbContext
    {
        public Contexto()
            :base("Connection")
        {

        }
        public DbSet<Categoria> Categoria { get; set; }

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

            modelBuilder.Configurations.Add(new CategoriaConfig());


        }
    }
}
