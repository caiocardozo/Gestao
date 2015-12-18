using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        //chamadas genericas repositori base faz crud padrao

        //adiciona ou atualiza um objeto no banco de dados
        void SaveOrUpdate(TEntity obj);
        //adiciona no banco de dados o obj
        void Add(TEntity obj);
        //seleciona por ID
        TEntity GetById(int id);
        //retorna um Ienumerable
        IEnumerable<TEntity> GetAll();
        //realiza update recebendo objeto
        void Update(TEntity obj);
        //realiza delete recebendo objeto
        void Remove(TEntity obj);
        //dispose força a implementar 
        void Dispose();
    }
}
