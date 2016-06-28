using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
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
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado)
        {
            return _orcamentoRepository.RetornaOrcamentos(servico, cidade, estado);
        }


        public IEnumerable<Orcamento> RetornaOrcamentosAbertos()
        {
            return _orcamentoRepository.RetornaOrcamentosAbertos();
        }

        public IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId)
        {
            return _orcamentoRepository.RetornaOrcamentosPagos(servico, cidade, estado, usuarioId);
        }

        public IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId)
        {
            return _orcamentoRepository.GetOrcamentoPagosPeloPrestador(usuarioId);
        }
    }
}
