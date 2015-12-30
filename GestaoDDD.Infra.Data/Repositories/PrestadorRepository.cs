using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class PrestadorRepository : RepositoryBase<Prestador>, IPrestadorRepository
    {
        public PrestadorRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
        }
    }
}
