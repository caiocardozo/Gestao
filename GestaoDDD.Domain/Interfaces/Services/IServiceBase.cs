using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        void SaveOrUpdate(TEntity obj);

        TEntity FindById(int id);

        IEnumerable<TEntity> FindAll();

        void Delete(TEntity obj);

        void Dispose();
    }
}
