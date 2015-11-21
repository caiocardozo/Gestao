
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Services;
namespace GestaoDDD.Application.Services
{
    public class PrestadorAppService : AppServiceBase<Prestador>, IPrestadorAppService
    {
        private readonly IPrestadorService _prestadorService;
        public PrestadorAppService(IPrestadorService prestadorService)
            :base(prestadorService)
        {
            _prestadorService = prestadorService;
        }

    }
}
