using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class OrcamentoRepository : RepositoryBase<Orcamento>, IOrcamentoRepository
    {

        public OrcamentoRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
        }
    }
}
