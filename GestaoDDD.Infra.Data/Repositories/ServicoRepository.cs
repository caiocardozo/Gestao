using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ServicoRepository : RepositoryBase<Servico>, IServicoRepository
    {
        public ServicoRepository(IGestaoContext gestaoContexto)
            : base(gestaoContexto)
        {

        }

    }
}
