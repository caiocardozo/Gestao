using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;
using System.Collections.Generic;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class ServicoRepository : RepositoryBase<Servico>, IServicoRepository
    {
        private readonly GestaoContext _db;
        public ServicoRepository(GestaoContext gestaoContexto)
            : base(gestaoContexto)
        {
            _db = new GestaoContext();
        }
    }
}
