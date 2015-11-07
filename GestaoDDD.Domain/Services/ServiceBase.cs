using GestaoDDD.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Interfaces;
using GestaoDDD.Domain.Interfaces.IRepositories;


namespace GestaoDDD.Domain.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IBaseDao<TEntity> _repository;

        public ServiceBase(IBaseDao<TEntity> repository)
        {
            _repository = repository;
        }

        public void SaveOrUpdate(TEntity obj) 
        {
            _repository.SaveOrUpdate(obj);
        }

        public TEntity FindById(int id) 
        {
            return _repository.FindById(id);
        }

        public IEnumerable<TEntity> FindAll() 
        {
            return _repository.FindAll();
        }

        public void Delete(TEntity obj) 
        {
            _repository.Delete(obj);
        }

        public void Dispose() 
        {
            _repository.Dispose();

        }

    }
}
