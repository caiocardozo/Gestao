using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class ServicoAppService : AppServiceBase<Servico>, IServicoAppService
    {
        private readonly IServicoService _servicoService;

        public ServicoAppService(IServicoService servicoBase)
            : base(servicoBase)
        {
            _servicoService = servicoBase;
        }
    }
}
