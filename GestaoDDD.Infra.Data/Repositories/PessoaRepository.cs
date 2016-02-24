using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        private readonly GestaoContext _gestaoContext;
        public PessoaRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _gestaoContext = new GestaoContext();
        }
    }
}
