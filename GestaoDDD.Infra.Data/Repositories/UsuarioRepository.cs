using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.IRepositories;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class USuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {

        public USuarioRepository(IGestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
        }
    }
}
