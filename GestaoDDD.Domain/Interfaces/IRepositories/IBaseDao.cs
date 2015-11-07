using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.IRepositories
{
    public interface IBaseDao<TEntity> where TEntity : class
    {
        void SaveOrUpdate(TEntity obj);

        TEntity FindById(int id);
        
        IEnumerable<TEntity> FindAll();

        void Delete(TEntity obj);

        void Dispose();
    }
}
