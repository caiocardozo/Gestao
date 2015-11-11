using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GestaoDDD.Infra.Data.Contexto
{
   public interface IGestaoContext
    {
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
    }
}
