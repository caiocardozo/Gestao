using System.Collections.Generic;

namespace GestaoDDD.Application.Interface
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        void SaveOrUpdate(TEntity obj);

        void Add(TEntity obj);

        TEntity GetById(int id);

        void Update(TEntity obj);

        IEnumerable<TEntity> GetAll();

        void Remove(TEntity obj);

        void Dispose();
    }
}
