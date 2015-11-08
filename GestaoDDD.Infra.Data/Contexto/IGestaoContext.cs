using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GestaoDDD.Infra.Data.Context
{
   public interface IGestaoContexto
    {
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
    }
}
