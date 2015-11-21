using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
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
    }
}
