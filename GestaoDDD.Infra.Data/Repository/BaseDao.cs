using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GestaoDDD.Infra.Data.Repository
{
    public class BaseDao<TEntity> : IDisposable, IBaseDao<TEntity> where TEntity:class
    {
        protected Contexto _context = new Contexto();

        public void SaveOrUpdate(TEntity obj) 
        {
            if (_context.Entry(obj).State == EntityState.Modified)
            {
                _context.Entry(obj).State = EntityState.Modified;
            }
            else 
            {
                _context.Set<TEntity>().Add(obj);
            }
            _context.SaveChanges();
        }

        public TEntity FindById(int id) 
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> FindAll() 
        {
            return _context.Set<TEntity>();
        }

        public void Delete(TEntity obj) 
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            throw new NotImplementedException();
        }

    }
}
