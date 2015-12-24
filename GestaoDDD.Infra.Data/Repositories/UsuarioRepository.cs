using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System;
using System.Collections.Generic;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly GestaoContext _gestaoContext;
        public UsuarioRepository(IGestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _gestaoContext = new GestaoContext();
        }

        public Usuario ObterPorId(string id)
        {
            return _gestaoContext.Usuario.Find(id);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _gestaoContext.Usuario;
        }
        public void DesativarLock(string id)
        {
            _gestaoContext.Usuario.Find(id).LockoutEnabled = false;
            _gestaoContext.SaveChanges();
        }

        public void Dispose()
        {
            _gestaoContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
