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
    }
}
