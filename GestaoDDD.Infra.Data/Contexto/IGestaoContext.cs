using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GestaoDDD.Domain.Entities;

namespace GestaoDDD.Infra.Data.Contexto
{
    public interface IGestaoContext
    {

        DbEntityEntry Entry(object entity);
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;

        DbSet<Categoria> Categoria { get; set; }
        DbSet<Prestador> Prestador { get; set; }
        DbSet<Usuario> Usuario { get; set; }
        DbSet<Orcamento> Orcamento { get; set; }
        DbSet<Servico> Servico { get; set; }
        DbSet<ServicoPrestador> ServicoPrestador { get; set; }
        DbSet<Pessoa> Pessoa { get; set; }
        DbSet<IndiqueProfissional> IndiqueProfissional { get; set; }
        DbSet<Contato> Contato { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<Cidade> Cidade { get; set; }
        DbSet<Log> Log { get; set; }

    }
}
