using GestaoDDD.Domain.Entities;
using System.Collections.Generic;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class CidadeService : ServiceBase<Cidade>, ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        public CidadeService(ICidadeRepository cidadeRepository)
            : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

       //retorna todos as cidades a partir de um estado
        public List<CidadeIdNome> RetornaCidadePeloEstado(int estado)
        {
            return _cidadeRepository.RetornaCidadePeloEstado(estado);
        }
    }
}
