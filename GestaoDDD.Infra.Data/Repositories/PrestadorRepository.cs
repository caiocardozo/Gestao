using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System.Linq;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class PrestadorRepository : RepositoryBase<Prestador>, IPrestadorRepository
    {
        private readonly GestaoContext _db;

        public PrestadorRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _db = new GestaoContext();
        }

        public Prestador GetPorCpf(string cpf)
        {
            return _db.Prestador.FirstOrDefault(s => s.pres_Cpf_Cnpj.Equals(cpf));
        }
    }
}
