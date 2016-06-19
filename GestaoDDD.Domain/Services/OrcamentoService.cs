using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Domain.Services
{
    public class OrcamentoService : ServiceBase<Orcamento>, IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository)
            :base(orcamentoRepository)
        {
            _orcamentoRepository = orcamentoRepository;
        }

        //retorna os orcamentos 
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade)
        {
            return _orcamentoRepository.RetornaOrcamentos(servico, cidade);
        }
    }
}
