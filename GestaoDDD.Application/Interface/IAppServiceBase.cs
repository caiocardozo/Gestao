using System.Collections.Generic;

namespace GestaoDDD.Application.Interface
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        void SaveOrUpdate(TEntity obj);

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Remove(TEntity obj);

        void Dispose();
    }
}
