using System.Collections.Generic;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class OrcamentoAppService : AppServiceBase<Orcamento>, IOrcamentoAppService
    {
        private readonly IOrcamentoService _orcamentoService;
        public OrcamentoAppService(IOrcamentoService orcamentoService)
            : base(orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        //retorna os orcamentos 
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumClasses.EnumEstados estado)
        {
            return _orcamentoService.RetornaOrcamentos(servico, cidade, estado);
        }
    }
}
