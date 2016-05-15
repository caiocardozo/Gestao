using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class ContatoAppService : AppServiceBase<Contato>, IContatoAppService
    {
        private readonly IContatoService _cttService;
        public ContatoAppService(IContatoService cttService)
            :base(cttService)
        {
            _cttService = cttService;
        }
    }
}
