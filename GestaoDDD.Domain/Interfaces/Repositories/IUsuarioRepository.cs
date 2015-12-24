using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterPorId(string id);
        IEnumerable<Usuario> ObterTodos();
        void DesativarLock(string id);
    }
}
