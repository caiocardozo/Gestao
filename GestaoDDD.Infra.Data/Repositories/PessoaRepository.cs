using System.Linq;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        private readonly GestaoContext _db;

        public PessoaRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _db = new GestaoContext();
        }

        //retorna a pessoa atraves do id
        public Pessoa RPessoaPorId(string id)
        {
            return _db.Pessoa.FirstOrDefault(p => p.usu_id.Equals(id));
        }
    }
}
