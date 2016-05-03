using System.Linq;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        //aqui rabalha com objetos genericos
        protected GestaoContext _db;

        public RepositoryBase(GestaoContext gestaoContext)
        {
            _db = gestaoContext;
        }

        public void Add(TEntity obj)
        {
            _db.Set<TEntity>().Add(obj);
            _db.SaveChanges();
        }

        public void SaveOrUpdate(TEntity obj)
        {
            _db.Set<TEntity>().Add(obj);
            _db.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            //retornando um objeto
            return _db.Set<TEntity>().Find(id);//find busca pela chave primaria pelo Id ali passado
        }

        public IEnumerable<TEntity> GetAll()
        {
            //retornando varios objetos
            return _db.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            //realiza o update seta o objeto como modificado
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            //remove o objeto
            _db.Set<TEntity>().Remove(obj);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
